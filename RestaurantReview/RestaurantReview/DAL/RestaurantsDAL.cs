using RestaurantReview.Models;
using RestaurantReview.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.DAL
{
    public class RestaurantsDAL
    {
        private readonly string connectionstring;
        public RestaurantsDAL(string connString)
        {
            this.connectionstring = new Conn().AWSconnstring();
        }

        public List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
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
            using (SqlConnection conn = new SqlConnection(connectionstring))
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