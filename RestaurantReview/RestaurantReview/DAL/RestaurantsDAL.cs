using RestaurantReview.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.DAL
{
    public class RestaurantsDAL
    {
        public List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            string connData;
            connData = @"Data Source=DESKTOP-B54NHFS ; Initial Catalog=RestaurantReviewManager; Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(connData))
            {
                conn.Open();
                SqlCommand SelectAll = new SqlCommand("select * from Restaurants", conn);
                SqlDataReader reader = SelectAll.ExecuteReader();
                while (reader.Read())
                {
                    restaurants.Add(new Restaurant
                    {
                        RestaurantId = Convert.ToInt32(reader["RestaurantId"]),
                        City = Convert.ToString(reader["City"]),
                        Name = Convert.ToString(reader["Name"])
                    });
                }
            }
            return restaurants;
        }

        public void PostRestaurant(Restaurant restaurant)
        {
            string connData;
            connData = @"Data Source=DESKTOP-B54NHFS ; Initial Catalog=RestaurantReviewManager; Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(connData))
            {
                conn.Open();
                SqlCommand SelectAll = new SqlCommand($"INSERT INTO RESTAURANTS VALUES(@Name, @City);", conn);
                SelectAll.Parameters.AddWithValue("@Name", restaurant.Name);
                SelectAll.Parameters.AddWithValue("@City", restaurant.City);
                SelectAll.ExecuteNonQuery();
            }
        }
    }
}