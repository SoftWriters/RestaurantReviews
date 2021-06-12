using RestaurantReviews.Core.DataTypes;
using RestaurantReviews.Core.Interfaces;
using RestaurantReviews.Database.Sqlite.Entities;
using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestaurantReviews.Database.Sqlite
{
    /// <summary>
    /// Controller for the restaurant review database
    /// </summary>
    public class SqliteRestaurantReviewDatabase : IRestaurantReviewDatabase
    {
        private SQLiteConnection _sqliteConnection;

        public SqliteRestaurantReviewDatabase(ISQLitePlatform sqlitePlatform, string filePath)
        {
            _sqliteConnection = InitializeConnection(sqlitePlatform, filePath);
        }

        #region Mutable functions

        public void AddRestaurant(IRestaurant restaurant)
        {
            if (restaurant == null)
                throw new ArgumentNullException(nameof(restaurant));

            //Check if it already exists
            if ((restaurant is SqliteRestaurant dbRestaurant && dbRestaurant.Id != 0) ||
                EntityExists(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), restaurant.UniqueId))
            {
                throw new DuplicateEntityException(nameof(IRestaurant), restaurant.UniqueId);
            }

            //If the address doesn't already exist, add it 
            SqliteAddress dbAddress = FindEntityByUniqueId<SqliteAddress>(_sqliteConnection, SqliteAddress.TableName, nameof(SqliteAddress.UniqueId), restaurant.Address.UniqueId);
            if (dbAddress == null)
            {
                dbAddress = new SqliteAddress(restaurant.Address);
                dbAddress.Save(_sqliteConnection);
            } //Otherwise, the address is ignored (e.g. another restaurant was there previously, or multiple restaurants are in the same building)

            //Create and save
            dbRestaurant = new SqliteRestaurant(restaurant, dbAddress);
            dbRestaurant.Save(_sqliteConnection);
        }

        public void DeleteRestaurant(Guid restaurantId)
        {
            //Get the restaurant This will be more convenient to work with.
            var dbRestaurant = (SqliteRestaurant)GetRestaurant(restaurantId);
            if (dbRestaurant == null)
                throw new EntityNotFoundException(nameof(IRestaurant), restaurantId);

            //First delete all associated reviews
            string deleteReviewsQuery = $"DELETE FROM {SqliteRestaurantReview.TableName}" +
                $" WHERE {nameof(SqliteRestaurantReview.RestaurantUniqueId)} = \"{restaurantId}\"";
            
            _sqliteConnection.Execute(deleteReviewsQuery);
            
            //Delete it
            dbRestaurant.Remove(_sqliteConnection);

            //Now check if the address should also be deleted (no more restaurants are referencing it)
            string addressUseCountQuery = $"SELECT COUNT(*) FROM {SqliteRestaurant.TableName}" +
                $" WHERE {SqliteRestaurant.TableName}.{nameof(SqliteRestaurant.AddressId)} = {dbRestaurant.AddressId}";

            int numRestaurantsUsingAddress = _sqliteConnection.ExecuteScalar<int>(addressUseCountQuery);
            if (numRestaurantsUsingAddress == 0)
            {
                string deleteAddressQuery = $"DELETE FROM {SqliteAddress.TableName}" +
                    $" WHERE {nameof(SqliteAddress.Id)} = \"{dbRestaurant.AddressId}\"";
                _sqliteConnection.Execute(deleteAddressQuery);
            }
        }

        public void AddReview(IRestaurantReview review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            //Check if it already exists
            if (EntityExists(_sqliteConnection, SqliteRestaurantReview.TableName, nameof(SqliteRestaurantReview.UniqueId), review.UniqueId))
                throw new DuplicateEntityException(nameof(IRestaurantReview), review.UniqueId);

            //Make sure the restaurant exists
            if (!EntityExists(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), review.RestaurantUniqueId))
                throw new EntityNotFoundException(nameof(IRestaurant), review.RestaurantUniqueId);

            //If the user doesn't already exist, add it 
            SqliteUser dbUser = FindEntityByUniqueId<SqliteUser>(_sqliteConnection, SqliteUser.TableName, nameof(SqliteUser.UniqueId), review.Reviewer.UniqueId);
            if (dbUser == null)
            {
                dbUser = new SqliteUser(review.Reviewer);
                dbUser.Save(_sqliteConnection);
            } //Otherwise, the user is ignored 

            var dbReview = new SqliteRestaurantReview(review, dbUser);
            dbReview.Save(_sqliteConnection);
        }

        public void DeleteReview(Guid reviewId)
        {
            //Get the review. This will be more convenient to work with.
            var dbReview = (SqliteRestaurantReview)GetReview(reviewId);
            if (dbReview == null)
                throw new EntityNotFoundException(nameof(IRestaurantReview), reviewId);

            //Delete it
            dbReview.Remove(_sqliteConnection);

            //Now check if the reviewer should also be deleted (no more reviews are referencing it)
            string reviewerUseCountQuery = $"SELECT COUNT(*) FROM {SqliteRestaurantReview.TableName}" +
                $" WHERE {SqliteRestaurantReview.TableName}.{nameof(SqliteRestaurantReview.ReviewerId)} = {dbReview.ReviewerId}";

            int numReviewsUsingReviewer = _sqliteConnection.ExecuteScalar<int>(reviewerUseCountQuery);
            if (numReviewsUsingReviewer == 0)
            {
                string deleteUserQuery = $"DELETE FROM {SqliteUser.TableName}" +
                    $" WHERE {nameof(SqliteUser.Id)} = \"{dbReview.ReviewerId}\"";
                _sqliteConnection.Execute(deleteUserQuery);
            }
        }

        #endregion

        #region Query functions

        public IRestaurant GetRestaurant(Guid restaurantId)
        {
            var restaurant = FindEntityByUniqueId<SqliteRestaurant>(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), restaurantId);

            if (restaurant != null)
                ConnectAddresses(_sqliteConnection, new[] { restaurant });

            return restaurant;
        }

        public IRestaurantReview GetReview(Guid reviewId)
        {
            var review = FindEntityByUniqueId<SqliteRestaurantReview>(_sqliteConnection, SqliteRestaurantReview.TableName, nameof(SqliteRestaurantReview.UniqueId), reviewId);
            
            if (review != null)
                ConnectUsers(_sqliteConnection, new[] { review });

            return review;
        }

        public IReadOnlyList<IRestaurant> FindRestaurants(string name = null, string city = null, string stateOrProvince = null, string postalCode = null)
        {
            //Build the SQL query from the optional parameters
            string query = $"SELECT {SqliteRestaurant.FullyQualifiedTableProperties}" +
                $" FROM {SqliteRestaurant.TableName} INNER JOIN {SqliteAddress.TableName}" +
                $" ON {SqliteRestaurant.TableName}.{nameof(SqliteRestaurant.AddressId)} = {SqliteAddress.TableName}.{nameof(SqliteAddress.Id)}" +
                $" WHERE 1 = 1"; //1=1 allows us to easily add the "AND" clauses dynamically

            //Since this is user input strings, use a command with parameter bindings to prevent against injection
            var statement = _sqliteConnection.CreateCommand(query);
            
            if (!string.IsNullOrEmpty(name))
            {
                statement.CommandText += $" AND {SqliteRestaurant.TableName}.{nameof(SqliteRestaurant.Name)} LIKE @name";
                statement.Bind("@name", $"%{name}%");
            }
            if (!string.IsNullOrEmpty(city))
            {
                statement.CommandText += $" AND {SqliteAddress.TableName}.{nameof(SqliteAddress.City)} LIKE @city";
                statement.Bind("@city", $"%{city}%");
            }
            if (!string.IsNullOrEmpty(stateOrProvince))
            {
                statement.CommandText += $" AND {SqliteAddress.TableName}.{nameof(SqliteAddress.StateOrProvince)} LIKE @state";
                statement.Bind("@state", $"%{stateOrProvince}%");
            }
            if (!string.IsNullOrEmpty(postalCode))
            {
                statement.CommandText += $" AND {SqliteAddress.TableName}.{nameof(SqliteAddress.PostalCode)} LIKE @postal";
                statement.Bind("@postal", $"%{postalCode}%");
            }

            var restaurants = statement.ExecuteQuery<SqliteRestaurant>();

            //Link up the foreign key objects
            ConnectAddresses(_sqliteConnection, restaurants);
            
            return restaurants.ToList();
        }

        public IReadOnlyList<IRestaurantReview> GetReviewsForRestaurant(Guid restaurantId)
        {
            string query = $"SELECT {SqliteRestaurantReview.FullyQualifiedTableProperties} FROM {SqliteRestaurantReview.TableName} " +
                $"WHERE {SqliteRestaurantReview.TableName}.{nameof(SqliteRestaurantReview.RestaurantUniqueId)} = \"{restaurantId}\"";

            var reviews = _sqliteConnection.Query<SqliteRestaurantReview>(query);

            //Link up the foreign key objects
            ConnectUsers(_sqliteConnection, reviews);

            return reviews;
        }

        public IReadOnlyList<IRestaurantReview> GetReviewsForReviewer(Guid reviewerId)
        {
            string query = $"SELECT {SqliteRestaurantReview.FullyQualifiedTableProperties} FROM {SqliteRestaurantReview.TableName}" +
                $" INNER JOIN {SqliteUser.TableName} ON {SqliteUser.TableName}.{nameof(SqliteUser.Id)} = {SqliteRestaurantReview.TableName}.{nameof(SqliteRestaurantReview.ReviewerId)}" +
                $" WHERE {SqliteUser.TableName}.{nameof(SqliteUser.UniqueId)} = \"{reviewerId}\"";

            var reviews = _sqliteConnection.Query<SqliteRestaurantReview>(query);

            //Link up the foreign key objects
            ConnectUsers(_sqliteConnection, reviews);

            return reviews;
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sqliteConnection?.Dispose();
                _sqliteConnection = null;
            }
        }

        #endregion

        #region Common Queries

        private static bool EntityExists(SQLiteConnection sqliteConnection, string tableName, string uniqueIdPropertyName, Guid id)
        {
            return sqliteConnection.ExecuteScalar<int>($"SELECT Count(*) FROM {tableName} WHERE {uniqueIdPropertyName} = \"{id}\" LIMIT 1") > 0;
        }

        private static T FindEntityByUniqueId<T>(SQLiteConnection sqliteConnection, string tableName, string uniqueIdPropertyName, Guid id) where T : class
        {
            return sqliteConnection.Query<T>($"SELECT * FROM {tableName} WHERE {uniqueIdPropertyName} = \"{id}\" LIMIT 1").FirstOrDefault();
        }

        private static void ConnectAddresses(SQLiteConnection sqliteConnection, IEnumerable<SqliteRestaurant> restaurants)
        {
            var addressesById = new Dictionary<int, SqliteAddress>();
            foreach (SqliteRestaurant restaurant in restaurants)
            {
                if (!addressesById.TryGetValue(restaurant.AddressId, out SqliteAddress address))
                {
                    string addressQuery = $"SELECT * FROM {SqliteAddress.TableName} WHERE {nameof(SqliteAddress.Id)} = {restaurant.AddressId} LIMIT 1";
                    address = sqliteConnection.Query<SqliteAddress>(addressQuery).FirstOrDefault();
                    addressesById[restaurant.AddressId] = address;
                }

                restaurant.Address = address;
            }
        }

        private static void ConnectUsers(SQLiteConnection sqliteConnection, IEnumerable<SqliteRestaurantReview> reviews)
        {
            var usersById = new Dictionary<int, SqliteUser>();
            foreach (SqliteRestaurantReview review in reviews)
            {
                if (!usersById.TryGetValue(review.ReviewerId, out SqliteUser user))
                {
                    string userQuery = $"SELECT * FROM {SqliteUser.TableName} WHERE {nameof(SqliteUser.Id)} = {review.ReviewerId} LIMIT 1";
                    user = sqliteConnection.Query<SqliteUser>(userQuery).FirstOrDefault();
                    usersById[review.ReviewerId] = user;
                }

                review.Reviewer = user;
            }
        }

        #endregion

        private static SQLiteConnection InitializeConnection(ISQLitePlatform sqlitePlatform, string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            bool createDb = !fileInfo.Exists || fileInfo.Length == 0;
            var connection = new SQLiteConnection(sqlitePlatform, filePath);

            if (createDb)
            {
                connection.CreateTable<SqliteAddress>();
                connection.CreateTable<SqliteRestaurant>();
                connection.CreateTable<SqliteRestaurantReview>();
                connection.CreateTable<SqliteUser>();
            }
            return connection;
        }
    }
}
