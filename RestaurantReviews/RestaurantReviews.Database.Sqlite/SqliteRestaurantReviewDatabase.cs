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
    public class SqliteRestaurantReviewDatabase : IRestaurantReviewMutableDatabase, IRestaurantReviewQueryDatabase, IDisposable
    {
        private SQLiteConnection _sqliteConnection;

        public SqliteRestaurantReviewDatabase(ISQLitePlatform sqlitePlatform, string filePath)
        {
            _sqliteConnection = InitializeConnection(sqlitePlatform, filePath);
        }

        #region IRestaurantReviewMutableDatabase implementation

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

        //public void UpdateRestaurant(IRestaurant restaurant)
        //{
        //    if (restaurant == null)
        //        throw new ArgumentNullException(nameof(restaurant));

        //    //Find the existing restaurant
        //    if (!(restaurant is SqliteRestaurant dbRestaurant) || dbRestaurant.Id == 0)
        //    {
        //        dbRestaurant = FindEntityByUniqueId<SqliteRestaurant>(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), restaurant.UniqueId);
        //        if (dbRestaurant == null)
        //            throw new EntityNotFoundException(nameof(IRestaurant), restaurant.UniqueId);
        //    }

        //    //Find the existing address or add a new one
        //    if (!(restaurant.Address is SqliteAddress dbAddress) || dbAddress.Id == 0)
        //    {
        //        dbAddress = FindEntityByUniqueId<SqliteAddress>(_sqliteConnection, SqliteAddress.TableName, nameof(SqliteAddress.UniqueId), restaurant.Address.UniqueId);
        //        if (dbAddress == null)
        //        {
        //            //Add the new address
        //            dbAddress = new SqliteAddress(restaurant.Address);
        //        }
        //        else
        //        {
        //            //Update the existing address
        //            dbAddress.UpdateProperties(restaurant.Address);
        //        }

        //        dbAddress.Save(_sqliteConnection);
        //    }

        //    //Update and save
        //    dbRestaurant.UpdateProperties(restaurant, dbAddress);
        //    dbRestaurant.Save(_sqliteConnection);
        //}

        public void DeleteRestaurant(Guid restaurantId)
        {
            //First delete all associated reviews
            string deleteReviewsQuery = $"DELETE FROM {SqliteRestaurantReview.TableName}" +
                $" WHERE {nameof(SqliteRestaurantReview.RestaurantUniqueId)} = \"{restaurantId}\"";
            
            _sqliteConnection.Execute(deleteReviewsQuery);

            //Count how many restaurants are sharing the same address
           // string addressUseCountQuery = $"SELECT COUNT(*) FROM {SqliteRestaurant.TableName} WHERE {nameof(SqliteRestaurant.AddressId)}"
            //TODO: Get the address, and delete it if there are no other restaurants referencing it
            DeleteEntityByUniqueId(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), restaurantId);
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

        #endregion

        #region IRestaurantReviewQueryDatabase Implementation

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
