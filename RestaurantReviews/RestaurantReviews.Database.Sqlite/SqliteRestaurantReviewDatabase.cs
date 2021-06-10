﻿using RestaurantReviews.Core;
using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestaurantReviews.Database.Sqlite
{
    public class SqliteRestaurantReviewDatabase : IRestaurantReviewDatabase, IDisposable
    {
        private SQLiteConnection _sqliteConnection;

        public SqliteRestaurantReviewDatabase(ISQLitePlatform sqlitePlatform, string filePath)
        {
            _sqliteConnection = InitializeConnection(sqlitePlatform, filePath);
        }

        public void AddRestaurant(IRestaurant restaurant)
        {
            if ((restaurant is SqliteRestaurant dbRestaurant) && dbRestaurant.Id != 0)
            {
                //TODO: Maybe search UniqueId too?
                throw new ArgumentException("Restaurant already exists", nameof(restaurant));
            }

            if (!(restaurant.Address is SqliteAddress dbAddress) || dbAddress.Id == 0)
            {
                dbAddress = FindEntityByUniqueId<SqliteAddress>(_sqliteConnection, SqliteAddress.TableName, nameof(SqliteAddress.UniqueId), restaurant.Address.UniqueId);
                if (dbAddress == null)
                {
                    //Add the address
                    dbAddress = new SqliteAddress(restaurant.Address);
                    dbAddress.Save(_sqliteConnection);
                }
                //TODO: Else update it? maybe separate api for that
            }

            dbRestaurant = new SqliteRestaurant(restaurant, dbAddress);
            dbRestaurant.Save(_sqliteConnection);
        }

        public bool UpdateRestaurant(IRestaurant restaurant)
        {
            //TODO: Maybe just update it in SQL instead?
            if (!(restaurant is SqliteRestaurant dbRestaurant) || dbRestaurant.Id == 0)
            {
                dbRestaurant = FindEntityByUniqueId<SqliteRestaurant>(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), restaurant.UniqueId);
                if (dbRestaurant == null)
                    return false;
            }

            if (!(restaurant.Address is SqliteAddress dbAddress) || dbAddress.Id == 0)
            {
                dbAddress = FindEntityByUniqueId<SqliteAddress>(_sqliteConnection, SqliteAddress.TableName, nameof(SqliteAddress.UniqueId), restaurant.Address.UniqueId);
                if (dbAddress == null)
                {
                    //Add the address
                    dbAddress = new SqliteAddress(restaurant.Address);
                    dbAddress.Save(_sqliteConnection);
                }
                //TODO: Else update it? maybe separate api for that
            }

            dbRestaurant.UpdateProperties(restaurant, dbAddress);
            return dbRestaurant.Save(_sqliteConnection);
        }

        public bool DeleteRestaurant(Guid restaurantId)
        {
            //TODO: Maybe just delete it in SQL instead?
            SqliteRestaurant dbRestaurant  = FindEntityByUniqueId<SqliteRestaurant>(_sqliteConnection, SqliteRestaurant.TableName, nameof(SqliteRestaurant.UniqueId), restaurantId);
            return dbRestaurant?.Remove(_sqliteConnection) ?? false;
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

        private static T FindEntityByUniqueId<T>(SQLiteConnection sqliteConnection, string tableName, string uniqueIdPropertyName, Guid id) where T : class
        {
            return sqliteConnection.Query<T>($"SELECT * FROM {tableName} WHERE {uniqueIdPropertyName} = \"{id}\" LIMIT 1").FirstOrDefault();

        }

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
