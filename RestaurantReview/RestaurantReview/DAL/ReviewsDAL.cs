using RestaurantReview.Models;
using RestaurantReview.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.DAL
{
    public class ReviewsDAL
    {
        private readonly string connectionstring;
        public ReviewsDAL(string connString)
        {
            this.connectionstring = new Conn().AWSconnstring();
        }
        public List<Review> GetAllReviews()
        {
            List<Review> reviews = new List<Review>();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand SelectAll = new SqlCommand("SELECT * FROM Reviews " +
                    "JOIN Restaurants on Reviews.RestaurantId = Restaurants.RestaurantId " +
                    "JOIN Users on Reviews.UserId = Users.UserId", conn);
                SqlDataReader reader = SelectAll.ExecuteReader();
                while (reader.Read())
                {
                    reviews.Add(new Review
                    {
                        Restaurant = new Restaurant
                        {
                            RestaurantId = Convert.ToInt32(reader["RestaurantId"]),
                            City = Convert.ToString(reader["City"]),
                            Name = Convert.ToString(reader["Name"])
                        },
                        ReviewId = Convert.ToInt32(reader["ReviewId"]),
                        ReviewText = Convert.ToString(reader["ReviewText"]),
                        User = new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            UserName = Convert.ToString(reader["UserName"])
                        }
                    });
                }
            }
            return reviews;
        }
        public void PostReview(Review review)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand SelectAll = new SqlCommand($"INSERT INTO Reviews VALUES(@RestaurantId, @UserId, @ReviewText);", conn);
                SelectAll.Parameters.AddWithValue("@RestaurantId", review.Restaurant.RestaurantId);
                SelectAll.Parameters.AddWithValue("@UserId", review.User.UserId);
                SelectAll.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                SelectAll.ExecuteNonQuery();
            }
        }
        public void DeleteReview(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand SelectAll = new SqlCommand($"Delete FROM Reviews WHERE Reviews.ReviewId = @ReviewId", conn);
                SelectAll.Parameters.AddWithValue("@ReviewId", id);
             
                SelectAll.ExecuteNonQuery();
            }
        }


    }
}
