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
        private const string INSERT_RESTAURANT_SPROC = "InsertRestaurant";

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
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Restaurant> GetRestaurantsByCity(NonEmptyString city)
        {
            var restaurants = new List<Restaurant>();

            using var connection = new SqlConnection(_connectionString);
            using (var command = new SqlCommand("SelectRestaurantsByCity", connection))
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

        public IEnumerable<Review> GetReviewsByUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
