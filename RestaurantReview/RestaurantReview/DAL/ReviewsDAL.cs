using RestaurantReview.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.DAL
{
    public class ReviewsDAL
    {
        public List<Review> GetAllReviews()
        {
            List<Review> reviews = new List<Review>();
            string connData;
            connData = @"Data Source=DESKTOP-B54NHFS ; Initial Catalog=RestaurantReviewManager; Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(connData))
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
            string connData;
            connData = @"Data Source=DESKTOP-B54NHFS ; Initial Catalog=RestaurantReviewManager; Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(connData))
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
            string connData;
            connData = @"Data Source=DESKTOP-B54NHFS ; Initial Catalog=RestaurantReviewManager; Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(connData))
            {
                conn.Open();
                SqlCommand SelectAll = new SqlCommand($"Delete FROM Reviews WHERE Reviews.ReviewId = @ReviewId", conn);
                SelectAll.Parameters.AddWithValue("@ReviewId", id);
             
                SelectAll.ExecuteNonQuery();
            }
        }


    }
}
