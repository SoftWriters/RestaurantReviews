using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantReviews.Entities.Data
{
    /// <summary>
    /// Provides persistance methods and retrieval for restaurant instances.
    /// </summary>
    internal static class RestaurantSQL
    {
        /// <summary>
        /// Persists a new instance of a restaurant.
        /// </summary>
        /// <param name="restaurant">The restaurant instance to persist.</param>
        public static void CreateRestaurant(Restaurant restaurant)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("CreateRestaurant", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantid", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Output });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@name", SqlDbType = SqlDbType.NVarChar, Value = restaurant.Name });

                    if (com.ExecuteNonQuery() != 1)
                        throw (new PersistanceException("Failed to create restaurant."));

                    restaurant.Id = (long)com.Parameters["@restaurantid"].Value;
                }
                con.Close();
            }
        }
        /// <summary>
        /// Updates a previously created restaurant instance.
        /// </summary>
        /// <param name="restaurant">The restaraunt instance to update.</param>
        public static void UpdateRestaurant(Restaurant restaurant)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("UpdateRestaurant", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantid", SqlDbType = SqlDbType.BigInt, Value = restaurant.Id });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@name", SqlDbType = SqlDbType.NVarChar, Value = restaurant.Name });

                    if (com.ExecuteNonQuery() != 1)
                        throw (new PersistanceException($"Failed to update restaurant, ID={restaurant.Id}."));
                }
                con.Close();
            }
        }
        /// <summary>
        /// Retrieves and instance of the restaurant with the specified id.
        /// </summary>
        /// <param name="restaurantId">The id of the restaurant to retrieve.</param>
        /// <returns>An instance of a restaurant with the given id.</returns>
        public static Restaurant GetRestaurant(long restaurantId)
        {
            Restaurant restaurant = null;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("GetRestaurant", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantid", SqlDbType = SqlDbType.BigInt, Value = restaurantId });

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            throw (new RetrievalException($"Failed to retrieve restaurant instance, ID={restaurantId}."));

                        reader.Read();

                        restaurant = new Restaurant();
                        restaurant.Id = (long)reader["id"];
                        restaurant.Name = (string)reader["name"];

                        reader.Close();
                    }
                }
                con.Close();
            }

            return restaurant;
        }
        /// <summary>
        /// Retrieves a list of Restaurants for a given City and Region/State.
        /// </summary>
        /// <param name="city">The City of interest.</param>
        /// <param name="region">The Region of interest.</param>
        /// <returns>A list of Restaurants.</returns>
        public static List<Restaurant> GetRestaurantsByCityRegion(string city,string region)
        {
            List<Restaurant> restaurants = null;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("GetRestaurantsByCityRegion", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@city", SqlDbType = SqlDbType.NVarChar, Value = city ?? (object)DBNull.Value });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@region", SqlDbType = SqlDbType.NVarChar, Value = region ?? (object)DBNull.Value });

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        restaurants = new List<Restaurant>();

                        while (reader.Read())
                            restaurants.Add(new Restaurant { Id = (long)reader["id"], Name = (string)reader["name"] });

                        reader.Close();
                    }
                }
                con.Close();
            }

            return restaurants;
        }
        
        /// <summary>
        /// Deletes previously persisted restaurant.
        /// </summary>
        /// <param name="restaurantId">The id of the restaurant to delete.</param>
        public static void DeleteRestaurant(long restaurantId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("DeleteRestaurant", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantid", SqlDbType = SqlDbType.BigInt, Value = restaurantId });

                    if (com.ExecuteNonQuery() <= 0)
                        throw (new PersistanceException($"Failed to delete restaurant, ID={restaurantId}."));
                }
                con.Close();
            }
        }
    }
}
