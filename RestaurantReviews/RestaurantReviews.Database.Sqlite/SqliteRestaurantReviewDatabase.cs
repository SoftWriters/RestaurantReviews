using RestaurantReviews.Core;
using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.IO;

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
                throw new ArgumentException("Restaurant already exists", nameof(restaurant));
            }
            dbRestaurant = new SqliteRestaurant(restaurant);
            dbRestaurant.Save(_sqliteConnection);
        }

        public bool UpdateRestaurant(IRestaurant restaurant)
        {
            //TODO: Maybe just update it in SQL instead?
            var dbRestaurant = restaurant as SqliteRestaurant;
            if (dbRestaurant == null || dbRestaurant.Id == 0)
            {
                dbRestaurant = FindRestaurantByUniqueId(_sqliteConnection, restaurant.UniqueId);
                if (dbRestaurant == null)
                    return false;
            }

            dbRestaurant.UpdateProperties(restaurant);
            return dbRestaurant.Save(_sqliteConnection);
        }

        public bool DeleteRestaurant(Guid restaurantId)
        {
            //TODO: Maybe just delete it in SQL instead?
            SqliteRestaurant dbRestaurant  = FindRestaurantByUniqueId(_sqliteConnection, restaurantId);
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
                dbUser = FindUserByUniqueId(_sqliteConnection, review.Reviewer.UniqueId);
                if (dbUser == null)
                    return; //TODO: Throw?
            }

            //Make sure the user and restaurant are saved
            if (!(review.Restaurant is SqliteRestaurant dbRestaurant))
            {
                dbRestaurant = FindRestaurantByUniqueId(_sqliteConnection, review.Reviewer.UniqueId);
                if (dbRestaurant == null)
                    return; //TODO: Throw?
            }

            var dbReview = new SqliteRestaurantReview(review, dbRestaurant, dbUser);
            dbReview.Save(_sqliteConnection);
        }

        public bool DeleteReview(Guid reviewId)
        {
            //TODO: Maybe just delete it in SQL instead?
            SqliteRestaurantReview dbReview = FindReviewByUniqueId(_sqliteConnection, reviewId);
            return dbReview?.Remove(_sqliteConnection) ?? false;
        }

        public void AddUser(IUser user)
        {
            if ((user is SqliteUser dbUser) && dbUser.Id != 0)
            {
                throw new ArgumentException("Restaurant already exists", nameof(user));
            }
            //TODO: Check if the unique Id already exists. Add Unique constraints on UniqueId fields
            dbUser = new SqliteUser(user);
            dbUser.Save(_sqliteConnection);
        }

        public bool DeleteUser(Guid userId)
        {
            //TODO: Maybe just delete it in SQL instead?
            SqliteUser dbUser = FindUserByUniqueId(_sqliteConnection, userId);
            return dbUser?.Remove(_sqliteConnection) ?? false;
        }

        public IReadOnlyList<IRestaurant> FindRestaurants(string name = null, string city = null, string stateOrProvince = null, string postalCode = null)
        {
            string restaurantQuery = $"SELECT * FROM {SqliteRestaurant.TableName} WHERE 1 = 1"; //1=1 allows us to easily add the "AND" clauses dynamically
            if (!string.IsNullOrEmpty(name))
                restaurantQuery += $" AND {nameof(SqliteRestaurant.Name)} = ?";

            //TODO: Can do some pre-validation on the zipcode
            if (!string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(stateOrProvince) || !string.IsNullOrEmpty(postalCode))
            {
                //Find relevant addresses
                string addressQuery = $"SELECT {nameof(SqliteAddress.Id)} FROM {SqliteAddress.TableName} WHERE 1 = 1";
                if (!string.IsNullOrEmpty(city))
                    addressQuery += $" AND {nameof(SqliteAddress.City)} LIKE %?%";
                if (!string.IsNullOrEmpty(stateOrProvince))
                    addressQuery += $" AND {nameof(SqliteAddress.StateOrProvince)} LIKE %?%";
                if (!string.IsNullOrEmpty(postalCode))
                    addressQuery += $" AND {nameof(SqliteAddress.PostalCode)} LIKE %?%";

                //restaurantQuery += ""
            }

            //using (var command = _sqliteConnection.CreateCommand()

            // throw new NotImplementedException();

            return new List<IRestaurant>();
        }

        public IReadOnlyList<IRestaurantReview> FindReviews(IRestaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IRestaurantReview> FindReviewsByReviewer(IUser reviewer)
        {
            throw new NotImplementedException();
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

        private static SqliteRestaurant FindRestaurantByUniqueId(SQLiteConnection sqliteConnection, Guid id)
        {
            return sqliteConnection.ExecuteScalar<SqliteRestaurant>($"SELECT * FROM {SqliteRestaurant.TableName} WHERE {nameof(SqliteRestaurant.UniqueId)} = {id}");
        }

        private static SqliteUser FindUserByUniqueId(SQLiteConnection sqliteConnection, Guid id)
        {
            return sqliteConnection.ExecuteScalar<SqliteUser>($"SELECT * FROM {SqliteUser.TableName} WHERE {nameof(SqliteUser.UniqueId)} = {id}");
        }

        private static SqliteRestaurantReview FindReviewByUniqueId(SQLiteConnection sqliteConnection, Guid id)
        {
            return sqliteConnection.ExecuteScalar<SqliteRestaurantReview>($"SELECT * FROM {SqliteRestaurantReview.TableName} WHERE {nameof(SqliteRestaurantReview.UniqueId)} = {id}");
        }

        private static SQLiteConnection InitializeConnection(ISQLitePlatform sqlitePlatform, string filePath)
        {
            bool createDb = !File.Exists(filePath);
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
