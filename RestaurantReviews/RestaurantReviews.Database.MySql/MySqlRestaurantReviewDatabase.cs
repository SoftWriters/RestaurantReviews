using MySql.Data.MySqlClient;
using RestaurantReviews.Core;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Database.MySql
{
    public class MySqlRestaurantReviewDatabase : IRestaurantReviewDatabase, IDisposable
    {
        public MySqlRestaurantReviewDatabase()
        {

        }

        public void AddRestaurant(IRestaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public void AddReview(IRestaurant restaurant, IRestaurantReview review)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRestaurant(Guid restaurantId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteReview(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IRestaurant> FindRestaurants(string city = null, string stateAbbreviation = null, string zipCode = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IRestaurantReview> FindReviewsByReviewer(IUser reviewer)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //Close the connection
        }

        private static MySqlConnection Connect()
        {
            const string myConnectionString = "server=127.0.0.1;uid=root;pwd=12345;database=test";

            try
            {
                var connection = new MySqlConnection(myConnectionString);
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Failed to open mySqlConnection: {ex}");
            }
        }
    }
}
