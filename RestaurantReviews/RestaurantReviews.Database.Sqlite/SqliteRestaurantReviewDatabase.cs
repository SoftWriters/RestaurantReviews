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
    public class SqliteRestaurantReviewDatabase : IRestaurantReviewDatabase, IDisposable
    {
        private SQLiteConnection _sqliteConnection;

        public SqliteRestaurantReviewDatabase(ISQLitePlatform sqlitePlatform, string filePath)
        {
            _sqliteConnection = InitializeConnection(sqlitePlatform, filePath);
        }

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

        public void UpdateRestaurant(IRestaurant restaurant)
        {
            if (restaurant == null)
                throw new ArgumentNullException(nameof(restaurant));

            //Find the existing restaurant
            if (!(restaurant is SqliteRestaurant dbRestaurant) || dbRestaurant.Id == 0)
            {
                dbRestaurant = FindEntityByUniqueId<SqliteRestaurant>(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), restaurant.UniqueId);
                if (dbRestaurant == null)
                    throw new EntityNotFoundException(nameof(IRestaurant), restaurant.UniqueId);
            }

            //Find the existing address or add a new one
            if (!(restaurant.Address is SqliteAddress dbAddress) || dbAddress.Id == 0)
            {
                dbAddress = FindEntityByUniqueId<SqliteAddress>(_sqliteConnection, SqliteAddress.TableName, nameof(SqliteAddress.UniqueId), restaurant.Address.UniqueId);
                if (dbAddress == null)
                {
                    //Add the new address
                    dbAddress = new SqliteAddress(restaurant.Address);
                }
                else
                {
                    //Update the existing address
                    dbAddress.UpdateProperties(restaurant.Address);
                }

                dbAddress.Save(_sqliteConnection);
            }

            //Update and save
            dbRestaurant.UpdateProperties(restaurant, dbAddress);
            dbRestaurant.Save(_sqliteConnection);
        }

        public void DeleteRestaurant(Guid restaurantId)
        {
            //First delete all associated reviews
            string deleteReviewsQuery = $"DELETE FROM {SqliteRestaurantReview.TableName}" +
                $" WHERE {nameof(SqliteRestaurantReview.RestaurantId)} IN" +
                $" (SELECT {nameof(SqliteRestaurant.Id)} FROM {SqliteRestaurant.TableName} WHERE {nameof(SqliteRestaurant.UniqueId)} = \"{restaurantId}\")";
            
            _sqliteConnection.Execute(deleteReviewsQuery);

            //Count how many restaurants are sharing the same address
            string addressUseCountQuery = $"SELECT COUNT(*) FROM {SqliteRestaurant.TableName} WHERE {nameof(SqliteRestaurant.AddressId)}"
            //TODO: Get the address, and delete it if there are no other restaurants referencing it
            DeleteEntityByUniqueId(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), restaurantId);
        }

        public void AddReview(IRestaurantReview review)
        {
            //TODO: Maybe just find the Ids we care about?
            if (review.Restaurant == null || review.Reviewer == null)
                return; //TODO: Throw?

            //Find the user and restaurant in the db.
            //TODO: Add them if they don't already exist?
            if (!(review.Reviewer is SqliteUser dbUser))
            {
                dbUser = FindEntityByUniqueId<SqliteUser>(_sqliteConnection, SqliteUser.TableName, nameof(SqliteUser.UniqueId), review.Reviewer.UniqueId);
                if (dbUser == null)
                    return; //TODO: Throw?
            }

            //Make sure the user and restaurant are saved
            if (!(review.Restaurant is SqliteRestaurant dbRestaurant))
            {
                dbRestaurant = FindEntityByUniqueId<SqliteRestaurant>(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), review.Restaurant.UniqueId);
                if (dbRestaurant == null)
                    return; //TODO: Throw?
            }

            var dbReview = new SqliteRestaurantReview(review, dbRestaurant, dbUser);
            dbReview.Save(_sqliteConnection);
        }

        public bool DeleteReview(Guid reviewId)
        {
            //TODO: Maybe just delete it in SQL instead?
            SqliteRestaurantReview dbReview = FindEntityByUniqueId<SqliteRestaurantReview>(_sqliteConnection, SqliteRestaurantReview.TableName, nameof(SqliteRestaurantReview.UniqueId), reviewId);
            return dbReview?.Remove(_sqliteConnection) ?? false;
        }

        public void AddUser(IUser user)
        {
            if ((user is SqliteUser dbUser) && dbUser.Id != 0)
            {
                throw new ArgumentException("User already exists", nameof(user));
            }
            //TODO: Check if the unique Id already exists. Add Unique constraints on UniqueId fields
            dbUser = new SqliteUser(user);
            dbUser.Save(_sqliteConnection);
        }

        public bool DeleteUser(Guid userId)
        {
            //TODO: Maybe just delete it in SQL instead?
            SqliteUser dbUser = FindEntityByUniqueId<SqliteUser>(_sqliteConnection, SqliteUser.TableName, nameof(SqliteUser.UniqueId), userId);
            return dbUser?.Remove(_sqliteConnection) ?? false;
        }

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

        public IReadOnlyList<IRestaurantReview> FindReviews(IRestaurant restaurant)
        {
            string query = $"SELECT {SqliteRestaurantReview.FullyQualifiedTableProperties}" +
                $" FROM {SqliteRestaurantReview.TableName} INNER JOIN {SqliteRestaurant.TableName}" +
                $" ON {SqliteRestaurantReview.TableName}.{nameof(SqliteRestaurantReview.RestaurantId)} = {SqliteRestaurant.TableName}.{nameof(SqliteRestaurant.Id)}" +
                $" WHERE {SqliteRestaurant.TableName}.{nameof(SqliteRestaurant.UniqueId)} = \"{restaurant.UniqueId}\"";

            var reviews = _sqliteConnection.Query<SqliteRestaurantReview>(query);

            //Link up the foreign key objects
            //We can be lazy here since we know the reviews are all for the same restaurant
            //TODO: Does this create problems if we're not reading the restaurant from the db?

            var usersById = new Dictionary<int, SqliteUser>();
            foreach (var review in reviews)
            {
                review.Restaurant = restaurant;

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

        public IReadOnlyList<IRestaurantReview> FindReviewsByReviewer(IUser reviewer)
        {
            string query = $"SELECT {SqliteRestaurantReview.FullyQualifiedTableProperties}" +
                $" FROM {SqliteRestaurantReview.TableName} INNER JOIN {SqliteUser.TableName}" +
                $" ON {SqliteRestaurantReview.TableName}.{nameof(SqliteRestaurantReview.ReviewerId)} = {SqliteUser.TableName}.{nameof(SqliteUser.Id)}" +
                $" WHERE {SqliteUser.TableName}.{nameof(SqliteUser.UniqueId)} = \"{reviewer.UniqueId}\"";

            var reviews = _sqliteConnection.Query<SqliteRestaurantReview>(query);

            var restaurantsById = new Dictionary<int, SqliteRestaurant>();
            foreach (var review in reviews)
            {
                review.Reviewer = reviewer;

                if (!restaurantsById.TryGetValue(review.RestaurantId, out SqliteRestaurant restaurant))
                {
                    string restaurantQuery = $"SELECT * FROM {SqliteRestaurant.TableName} WHERE {nameof(SqliteRestaurant.Id)} = {review.RestaurantId} LIMIT 1";
                    restaurant = _sqliteConnection.Query<SqliteRestaurant>(restaurantQuery).FirstOrDefault();

                    string addressQuery = $"SELECT * FROM {SqliteAddress.TableName} WHERE {nameof(SqliteAddress.Id)} = {restaurant.AddressId} LIMIT 1";

                    restaurant.Address = _sqliteConnection.Query<SqliteAddress>(addressQuery).FirstOrDefault();

                    restaurantsById[restaurant.Id] = restaurant;
                }

                review.Restaurant = restaurant;
            }

            return reviews;
        }

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
