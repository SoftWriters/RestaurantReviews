using RestaurantReviews.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.DataAccess
{
    public class SqlServerRestaurantReviewRepository : IRestaurantReviewRepository
    {
        private string _connectionString = @"Server=(localdb)\MSSqlLocalDb;Database=RestaurantReviews;Trusted_Connection=True;";
        
        private const string DELETE_REVIEW_SPROC = "DeleteReview";
        private const string INSERT_RESTAURANT_SPROC = "InsertRestaurant";
        private const string INSERT_REVIEW_SPROC = "InsertReview";
        private const string INSERT_USER_SPROC = "InsertUser";
        private const string SELECT_RESTAURANT_SPROC = "SelectRestaurant";
        private const string SELECT_RESTAURANTS_BY_CITY_SPROC = "SelectRestaurantsByCity";
        private const string SELECT_REVIEWS_BY_USER_SPROC = "SelectReviewsByUser";
        private const string SELECT_USER_SPROC = "SelectUser";

        public void AddRestaurant(Restaurant restaurant)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(INSERT_RESTAURANT_SPROC, conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", IdModule.unwrap(restaurant.Id));
                command.Parameters.AddWithValue("@name", NonEmptyStringModule.unwrap(restaurant.Name));
                command.Parameters.AddWithValue("@city", NonEmptyStringModule.unwrap(restaurant.City));
                
                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddReview(Review review)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(INSERT_REVIEW_SPROC, conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", IdModule.unwrap(review.Id));
                command.Parameters.AddWithValue("@userId", IdModule.unwrap(review.User));
                command.Parameters.AddWithValue("@city", IdModule.unwrap(review.Restaurant));
                command.Parameters.AddWithValue("@rating", RatingModule.unwrap(review.Rating));
                command.Parameters.AddWithValue("@reviewText", review.ReviewText);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteReview(Id id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(DELETE_REVIEW_SPROC, conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", IdModule.unwrap(id));

                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Restaurant> GetRestaurantsByCity(NonEmptyString city)
        {
            var restaurants = new List<Restaurant>();

            using var connection = new SqlConnection(_connectionString);
            using (var command = new SqlCommand(SELECT_RESTAURANTS_BY_CITY_SPROC, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@city", NonEmptyStringModule.unwrap(city));

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var dbId = reader.GetGuid("Id");
                    var dbName = reader.GetString("Name");

                    var idResult = IdModule.create(dbId);
                    var nameResult = NonEmptyStringModule.create(dbName);

                    if (idResult.IsError || nameResult.IsError)
                        continue;
                    else
                        restaurants.Add(
                            new Restaurant(idResult.ResultValue, nameResult.ResultValue, city));
                }
            }

            return restaurants;
        }

        public IEnumerable<Review> GetReviewsByUser(Id id)
        {
            var reviews = new List<Review>();

            using var connection = new SqlConnection(_connectionString);
            using (var command = new SqlCommand(SELECT_REVIEWS_BY_USER_SPROC, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", IdModule.unwrap(id));

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var dbId = reader.GetGuid("Id");
                    var dbRestaurantId = reader.GetGuid("RestaurantId");
                    var dbRating = reader.GetInt32("Rating");

                    var reviewText = reader.GetString("ReviewText");

                    var idResult = IdModule.create(dbId);
                    var restaurantIdResult = IdModule.create(dbRestaurantId);
                    var ratingResult = RatingModule.create(dbRating);

                    bool isDataError = idResult.IsError ||
                        restaurantIdResult.IsError ||
                        ratingResult.IsError;

                    if (isDataError)
                        continue;
                    else
                    {
                        reviews.Add(
                            new Review(idResult.ResultValue, id, restaurantIdResult.ResultValue, ratingResult.ResultValue, reviewText));
                    }
                }
            }

            return reviews;
        }

        public Restaurant GetRestaurant(Id id)
        {
            using var connection = new SqlConnection(_connectionString);
            using (var command = new SqlCommand(SELECT_RESTAURANT_SPROC, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", IdModule.unwrap(id));

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                var isRead = reader.Read();

                if (!isRead) return null;
                                
                var dbCity = reader.GetString("City");
                var dbName = reader.GetString("Name");

                var cityResult = NonEmptyStringModule.create(dbCity);
                var nameResult = NonEmptyStringModule.create(dbName);

                return new Restaurant(id, nameResult.ResultValue, cityResult.ResultValue);                
            }
        }

        public User GetUser(Id id)
        {
            using var connection = new SqlConnection(_connectionString);
            using (var command = new SqlCommand(SELECT_USER_SPROC, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", IdModule.unwrap(id));

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                var isRead = reader.Read();

                if (!isRead) return null;

                var dbFirst = reader.GetString("FirstName");
                var dbLast = reader.GetString("LastName");

                var firstNameResult = NonEmptyStringModule.create(dbFirst);

                return new User(id, firstNameResult.ResultValue, dbLast);
            }
        }

        public void AddUser(User user)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(INSERT_USER_SPROC, conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", IdModule.unwrap(user.Id));
                command.Parameters.AddWithValue("@firstName", NonEmptyStringModule.unwrap(user.FirstName));
                command.Parameters.AddWithValue("@lastName", user.LastName);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
