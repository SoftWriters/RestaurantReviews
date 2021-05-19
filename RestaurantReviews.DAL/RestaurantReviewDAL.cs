using Dapper;
using RestarauntReviews.DTO;
using RestaurantReviews.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantReviews.DAL
{
    public class RestaurantReviewDAL : IRestaurantReviewDAL
    {
        public string connectionString;
        public RestaurantReviewDAL()
        {
            connectionString = EnvironmentManagement.GetConnectionString();
        }

        public IEnumerable<Restaraunt> GetRestaurants(string city)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var customer = connection.Query<Restaraunt>("Select * FROM RESTAURANT WHERE City='" + city + "'").AsList();
                return (customer);
            }
        }

        public IEnumerable<Restaraunt> GetRestaurants()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var lstRestraurant = connection.Query<Restaraunt>("Select * FROM RESTAURANT").AsList();
                return (lstRestraurant);
            }
        }

        public IEnumerable<Review> GetReviews(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var reviews = connection.Query<Review>("Select ReviewId, UserName, ReviewDescription, Score  FROM REVIEW R INNER JOIN USER U on R.UserId = U.UserId WHERE U.UserName='" + username + "'").AsList();
                return (reviews);
            }
        }

    }
}
