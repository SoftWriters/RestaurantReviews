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
    /// Provides persistance methods and retrieval for restaurant address instances.
    /// </summary>
    internal static class RestaurantAddressSQL
    {
        /// <summary>
        /// Persists a new instance of RestaurantAddress class.
        /// </summary>
        /// <param name="address">The address to persist.</param>
        public static void CreateRestaurantAddress(RestaurantAddress address)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("CreateRestaurantAddress", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantaddressid", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Output });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantid", SqlDbType = SqlDbType.BigInt, Value = address.RestaurantId });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@street1", SqlDbType = SqlDbType.NVarChar, Value = address.Street1 ?? (object)DBNull.Value });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@street2", SqlDbType = SqlDbType.NVarChar, Value = address.Street2 ?? (object)DBNull.Value });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@city", SqlDbType = SqlDbType.NVarChar, Value = address.City });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@region", SqlDbType = SqlDbType.NVarChar, Value = address.Region });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@postalcode", SqlDbType = SqlDbType.NVarChar, Value = address.PostalCode ?? (object)DBNull.Value });

                    if (com.ExecuteNonQuery() != 1)
                        throw (new PersistanceException("Failed to create address."));

                    address.Id = (long)com.Parameters["@restaurantaddressid"].Value;
                }
                con.Close();
            }
        }
        /// <summary>
        /// Updates an existing RestaurantAddress instance.
        /// </summary>
        /// <param name="address">The address instance to perform the update on.</param>
        public static void UpdateRestaurantAddress(RestaurantAddress address)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("UpdateRestaurantAddress", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantaddressid", SqlDbType = SqlDbType.BigInt, Value = address.Id });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantid", SqlDbType = SqlDbType.BigInt, Value = address.RestaurantId });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@street1", SqlDbType = SqlDbType.NVarChar, Value = address.Street1 ?? (object)DBNull.Value });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@street2", SqlDbType = SqlDbType.NVarChar, Value = address.Street2 ?? (object)DBNull.Value });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@city", SqlDbType = SqlDbType.NVarChar, Value = address.City });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@region", SqlDbType = SqlDbType.NVarChar, Value = address.Region });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@postalcode", SqlDbType = SqlDbType.NVarChar, Value = address.PostalCode ?? (object)DBNull.Value });

                    if (com.ExecuteNonQuery() != 1)
                        throw (new PersistanceException($"Failed to update address, ID={address.Id}."));
                }
                con.Close();
            }
        }
        /// <summary>
        /// Retrieves a previously persisted RestaurantAddress instance.
        /// </summary>
        /// <param name="restaurantAddressId">The ID of the address to retrieve.</param>
        /// <returns>A previously persisted RestaurantAddress instance</returns>
        public static RestaurantAddress GetRestaurantAddress(long restaurantId, long restaurantAddressId)
        {
            RestaurantAddress address = null;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("GetRestaurantAddress", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantId", SqlDbType = SqlDbType.BigInt, Value = restaurantId });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantaddressid", SqlDbType = SqlDbType.BigInt, Value = restaurantAddressId });

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            throw (new RetrievalException($"Failed to retrieve address instance, ID={restaurantAddressId}."));

                        reader.Read();

                        address = new RestaurantAddress();
                        address.Id = (long)reader["id"];
                        address.RestaurantId = (long)reader["restaurant_id"];
                        address.Street1 = reader["street1"] == DBNull.Value ? null : (string)reader["street1"];
                        address.Street2 = reader["street2"] == DBNull.Value ? null : (string)reader["street2"];
                        address.City = (string)reader["city"];
                        address.Region = (string)reader["region"];
                        address.PostalCode = reader["postalcode"] == DBNull.Value ? null : (string)reader["postalcode"];

                        reader.Close();
                    }
                }
                con.Close();
            }

            return address;
        }
        /// <summary>
        /// Retrieves all addresses for a given Restaurant Id.
        /// </summary>
        /// <param name="restaurantId">The Id of the Restaurant to retrieve addresses for.</param>
        /// <returns>A list of RestaurantAddress.</returns>
        public static List<RestaurantAddress> GetRestaurantAddresses(long restaurantId)
        {
            List<RestaurantAddress> addresses = null;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("GetRestaurantAddresses", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantId", SqlDbType = SqlDbType.BigInt, Value = restaurantId });

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        addresses = new List<RestaurantAddress>();

                        while (reader.Read())
                        {
                            RestaurantAddress address = new RestaurantAddress();
                            address.Id = (long)reader["id"];
                            address.RestaurantId = (long)reader["restaurant_id"];
                            address.Street1 = reader["street1"] == DBNull.Value ? null : (string)reader["street1"];
                            address.Street2 = reader["street2"] == DBNull.Value ? null : (string)reader["street2"];
                            address.City = (string)reader["city"];
                            address.Region = (string)reader["region"];
                            address.PostalCode = reader["postalcode"] == DBNull.Value ? null : (string)reader["postalcode"];
                            addresses.Add(address);
                        }

                        reader.Close();
                    }
                }
                con.Close();
            }

            return addresses;
        }
        
        /// <summary>
        /// Deletes a persisted RestaurantAddress instance.
        /// </summary>
        /// <param name="restaurantAddressId">The ID of the address to delete.</param>
        public static void DeleteRestaurantAddress(long restaurantId, long restaurantAddressId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("DeleteRestaurantAddress", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantid", SqlDbType = SqlDbType.BigInt, Value = restaurantId });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantaddressid", SqlDbType = SqlDbType.BigInt, Value = restaurantAddressId });

                    if (com.ExecuteNonQuery() <= 0)
                        throw (new PersistanceException($"Failed to delete address, ID={restaurantAddressId}."));
                }
                con.Close();
            }
        }
    }
}
