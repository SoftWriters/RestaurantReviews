using RestaurantReview.Models;
using RestaurantReview.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;

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
                    Restaurant restaurant = new Restaurant
                    {
                        RestaurantId = Convert.ToInt32(reader["RestaurantId"]),
                        City = Convert.ToString(reader["City"]),
                        Name = Convert.ToString(reader["Name"])
                    };
                    if (restaurant.ValidateCity() && restaurant.ValidateName())
                        restaurants.Add(restaurant);
                }
            }
            return restaurants;
        }

        public (bool IsSuccessful, Restaurant toreturn) PostRestaurant(Restaurant restaurant)
        {
            bool IsSuccessful;
            Restaurant toreturn = new Restaurant();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand SelectAll = new SqlCommand($"INSERT INTO RESTAURANTS VALUES(@Name, @City);", conn);
                try
                {
                    if (!(restaurant.ValidateName() && restaurant.ValidateCity())) throw new HttpResponseException(HttpStatusCode.NotModified);
                    SelectAll.Parameters.AddWithValue("@Name", restaurant.Name);
                    SelectAll.Parameters.AddWithValue("@City", restaurant.City);
                    toreturn.Name = restaurant.Name;
                    toreturn.City = restaurant.City;
                    SelectAll.ExecuteNonQuery();
                    IsSuccessful = true;
                }
                catch (HttpResponseException e)
                {
                    if (!restaurant.ValidateCity())
                    {
                        toreturn.City = "City is incorrect " + e.Message;
                    }

                    IsSuccessful = false;
                    if (!restaurant.ValidateName())
                    {
                        toreturn.Name = "Name is too short " + e.Message + " name must be at least 1 character";
                    }

                    if (toreturn.Name is null) toreturn.Name = restaurant.Name;
                    if (toreturn.City is null) toreturn.City = restaurant.City;
                }
            }
            return (IsSuccessful, toreturn);
        }
    }
}