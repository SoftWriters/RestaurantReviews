using Dapper;
using RestarauntReviews.DTO;
using RestaurantReviews.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantReviews.DAL
{
    public class Restaurant : IRestaurantReviewDAL
    {
        public string connectionString;
        public Restaurant()
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

        public IEnumerable<Restaurant> GetRestaurants()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var restaraunts = connection.Query<Restaraunt>("Select * FROM RESTAURANT").AsList();
                return (restaraunts);
            }
        }

        public IEnumerable<Review> GetReviews(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var reviews = connection.Query<Review>("Select R.ReviewId, U.UserName, R.ReviewDescription, R.Score, R.RestaurantId FROM REVIEW R INNER JOIN USER U on R.UserId = U.UserId WHERE U.UserName='" + username + "'").AsList();
                return (reviews);
            }
        }

        public void AddReview(Restaraunt restaurant)
        {
            var sql = "InsertReview";
            using (var connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql,
                new { Restaraunt = restaurant, Code = "Single_Insert_1" },
                    commandType: CommandType.StoredProcedure);
                if (affectedRows != 1)
                {
                    throw (new Exception());
                }
            }
        }

        public void AddReview(Review review)
        {
            var sql = "InsertReview";
            using (var connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql,
                new { Review = review, Code = "Single_Insert_1" },
                    commandType: CommandType.StoredProcedure);
                if(affectedRows != 1)
                {
                    throw (new Exception());
                }
            }
        }

        public void AddRestaurant(Restaraunt restaraunt)
        {
            var sql = "InsertRestaraunt";
            using (var connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql,
                new { Restaraunt = restaraunt, Code = "Single_Insert_1" },
                    commandType: CommandType.StoredProcedure);
                if (affectedRows != 1)
                {
                    throw (new Exception());
                }
            }
        }

        public void DeleteReview(int ReviewId)
        {
            var sql = "DeleteReview";
            using (var connection = new SqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sql,
                new { ReviewId = ReviewId },
                    commandType: CommandType.StoredProcedure);
                if (affectedRows != 1)
                {
                    throw (new Exception());
                }
            }
        }
    }
}
