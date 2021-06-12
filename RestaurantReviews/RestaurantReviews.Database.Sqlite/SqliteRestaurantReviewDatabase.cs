using RestaurantReviews.Core;
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
            //First delete all associated reviews
            string deleteReviewsQuery = $"DELETE FROM {SqliteRestaurantReview.TableName}" +
                $" WHERE {nameof(SqliteRestaurantReview.RestaurantUniqueId)} = \"{restaurantId}\"";
            
            _sqliteConnection.Execute(deleteReviewsQuery);

            //Get the restaurant This will be more convenient to work with.
            SqliteRestaurant dbRestaurant = FindEntityByUniqueId<SqliteRestaurant>(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), restaurantId);
            
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
            SqliteRestaurantReview dbReview = FindEntityByUniqueId<SqliteRestaurantReview>(_sqliteConnection, SqliteRestaurantReview.TableName, nameof(SqliteRestaurantReview.UniqueId), reviewId);

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

        public IReadOnlyList<IRestaurant> FindRestaurants(string name = null, string city = null, string stateOrProvince = null, string postalCode = null)
        {
            //Build the SQL query from the optional parameters
            string query = $"SELECT {SqliteRestaurant.FullyQualifiedTableProperties}" +
                $" FROM {SqliteRestaurant.TableName} INNER JOIN {SqliteAddress.TableName}" +
                $" ON {SqliteRestaurant.TableName}.{nameof(SqliteRestaurant.AddressId)} = {SqliteAddress.TableName}.{nameof(SqliteAddress.Id)}" +
                $" WHERE 1 = 1"; //1=1 allows us to easily add the "AND" clauses dynamically

            //TODO: Prevent sql injection and all that good stuff
            if (!string.IsNullOrEmpty(name))
            {
                query += $" AND {SqliteRestaurant.TableName}.{nameof(SqliteRestaurant.Name)} LIKE \"%{name}%\"";
            }
            if (!string.IsNullOrEmpty(city))
            {
                query += $" AND {SqliteAddress.TableName}.{nameof(SqliteAddress.City)} LIKE \"%{city}%\"";
            }
            if (!string.IsNullOrEmpty(stateOrProvince))
            {
                query += $" AND {SqliteAddress.TableName}.{nameof(SqliteAddress.StateOrProvince)} LIKE \"%{stateOrProvince}%\"";
            }
            if (!string.IsNullOrEmpty(postalCode))
            {
                query += $" AND {SqliteAddress.TableName}.{nameof(SqliteAddress.PostalCode)} LIKE \"%{postalCode}%\"";
            }

            var restaurants = _sqliteConnection.Query<SqliteRestaurant>(query);
            //Link up the foreign key objects
            //TODO: Already have the address information from the query. Would be more efficient to use that
            foreach (var restaurant in restaurants)
            {
                string addressQuery = $"SELECT * FROM {SqliteAddress.TableName} WHERE {nameof(SqliteAddress.Id)} = {restaurant.AddressId} LIMIT 1";

                restaurant.Address = _sqliteConnection.Query<SqliteAddress>(addressQuery).FirstOrDefault();
            }
            
            return restaurants.ToList();
        }

        public IReadOnlyList<IRestaurantReview> GetReviewsForRestaurant(Guid restaurantId)
        {
            string query = $"SELECT {SqliteRestaurantReview.FullyQualifiedTableProperties} FROM {SqliteRestaurantReview.TableName} " +
                $"WHERE {SqliteRestaurantReview.TableName}.{nameof(SqliteRestaurantReview.RestaurantUniqueId)} = \"{restaurantId}\"";

            var reviews = _sqliteConnection.Query<SqliteRestaurantReview>(query);

            //Link up the foreign key objects
            //TODO: Move this somewhere common
            var usersById = new Dictionary<int, SqliteUser>();
            foreach (var review in reviews)
            {
                if (!usersById.TryGetValue(review.ReviewerId, out SqliteUser reviewer))
                {
                    string userQuery = $"SELECT * FROM {SqliteUser.TableName} WHERE {nameof(SqliteUser.Id)} = {review.ReviewerId} LIMIT 1";
                    reviewer = _sqliteConnection.Query<SqliteUser>(userQuery).FirstOrDefault();
                    usersById[reviewer.Id] = reviewer;
                }

                review.Reviewer = reviewer;
            }

            return reviews;
        }

        public IReadOnlyList<IRestaurantReview> GetReviewsForReviewer(Guid reviewerId)
        {
            string query = $"SELECT {SqliteRestaurantReview.FullyQualifiedTableProperties} FROM {SqliteRestaurantReview.TableName}" +
                $" INNER JOIN {SqliteUser.TableName} ON {SqliteUser.TableName}.{nameof(SqliteUser.Id)} = {SqliteRestaurantReview.TableName}.{nameof(SqliteRestaurantReview.ReviewerId)}" +
                $" WHERE {SqliteUser.TableName}.{nameof(SqliteUser.UniqueId)} = \"{reviewerId}\"";

            var reviews = _sqliteConnection.Query<SqliteRestaurantReview>(query);

            //Link up the foreign key objects
            var usersById = new Dictionary<int, SqliteUser>();
            foreach (var review in reviews)
            {
                if (!usersById.TryGetValue(review.ReviewerId, out SqliteUser reviewer))
                {
                    string userQuery = $"SELECT * FROM {SqliteUser.TableName} WHERE {nameof(SqliteUser.Id)} = {review.ReviewerId} LIMIT 1";
                    reviewer = _sqliteConnection.Query<SqliteUser>(userQuery).FirstOrDefault();
                    usersById[reviewer.Id] = reviewer;
                }

                review.Reviewer = reviewer;
            }

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

        private static T FindEntityById<T>(SQLiteConnection sqliteConnection, string tableName, string idPropertyName, int id) where T : class
        {
            return sqliteConnection.Query<T>($"SELECT * FROM {tableName} WHERE {idPropertyName} = {id} LIMIT 1").FirstOrDefault();
        }

        private static T FindEntityByUniqueId<T>(SQLiteConnection sqliteConnection, string tableName, string uniqueIdPropertyName, Guid id) where T : class
        {
            return sqliteConnection.Query<T>($"SELECT * FROM {tableName} WHERE {uniqueIdPropertyName} = \"{id}\" LIMIT 1").FirstOrDefault();
        }

        private static void DeleteEntityByUniqueId(SQLiteConnection sqliteConnection, string tableName, string uniqueIdPropertyName, Guid id)
        {
            //I would add LIMIT 1 to this but it's a compile-time option I don't have right now
            sqliteConnection.ExecuteScalar<int>($"DELETE FROM {tableName} WHERE {uniqueIdPropertyName} = \"{id}\"");
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
