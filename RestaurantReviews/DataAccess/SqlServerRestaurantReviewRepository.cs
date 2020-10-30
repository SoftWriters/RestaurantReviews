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
        private const string INSERT_RESTAURANT_SPROC = "InsertRestaurant";

        public void AddRestaurant(Restaurant restaurant)
        {
            using (var conn = new SqlConnection(@"Server=(localdb)\MSSqlLocalDb;Database=RestaurantReviews;Trusted_Connection=True;"))
            using (var command = new SqlCommand(INSERT_RESTAURANT_SPROC, conn))
            {
                conn.Open();

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", IdModule.unwrap(restaurant.Id));
                command.Parameters.AddWithValue("@name", NonEmptyStringModule.unwrap(restaurant.Name));
                command.Parameters.AddWithValue("@city", NonEmptyStringModule.unwrap(restaurant.City));

                command.ExecuteNonQuery();
            }
        }

        public void AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task AddReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Restaurant> GetRestaurantsByCity(string city)
        {
            throw new NotImplementedException();
            //using var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString);
            //using (var command = new SqlCommand("SelectRestaurantByCity", connection))
            //{
            //    command.CommandType = CommandType.StoredProcedure;
            //    connection.Open();

            //    SqlDataReader response = command.ExecuteReader();
            //}
        }

        public Task<IEnumerable<Restaurant>> GetRestaurantsByCityAsync(string city)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetReviewsByUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetReviewsByUsersAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
