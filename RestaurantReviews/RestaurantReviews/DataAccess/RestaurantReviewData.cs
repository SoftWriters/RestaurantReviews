using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using RestaurantReviews.Domain;

namespace RestaurantReviews.DataAccess
{
    public interface IRestaurantReviewData
    {
        //Restaurant operations
        List<Restaurant> GetRestaurantsByCity(int? cityId);
        int AddRestaurant(Restaurant restaurant);

        //Reviews operations
        List<Review> GetReviews(int? userId);
        int AddReview(Review review);
        bool DeleteReview(int reviewId);

    }
    public class RestaurantReviewData : IRestaurantReviewData
    {
        public List<Restaurant> GetRestaurantsByCity(int? cityId)
        {
            SqlConnection conn = new SqlConnection("connectionString");
            SqlCommand cmd = conn.CreateCommand();
            List<Restaurant> items = new List<Restaurant>();
            Restaurant item;

            int restaurantId_index = -1;
            int restaurantName_index = -1;
            int address_index = -1;
            int city_index = -1;
            int country_index = -1;
            int zipCode_index = -1;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetRestaurantsByCity";
            cmd.Parameters.AddWithValue("@cityId", cityId);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    restaurantId_index = dr.GetOrdinal("RestaurantID");
                    restaurantName_index = dr.GetOrdinal("RestaurantName");
                    address_index = dr.GetOrdinal("Address");
                    city_index = dr.GetOrdinal("City");
                    country_index = dr.GetOrdinal("Country");
                    zipCode_index = dr.GetOrdinal("ZipCode")

                }

                while (dr.Read())
                {
                    item = new Restaurant();
                    if (!dr.IsDBNull(restaurantId_index))
                    {
                        item.RestaurantId = dr.GetInt32(restaurantId_index);
                    }
                    if (!dr.IsDBNull(restaurantName_index))
                    {
                        item.RestaurantName = dr.GetString(restaurantName_index);
                    }
                    if (!dr.IsDBNull(address_index))
                    {
                        item.Address = dr.GetString(address_index);
                    }
                    if (!dr.IsDBNull(city_index))
                    {
                        item.City = dr.GetString(city_index);
                    }
                    if (!dr.IsDBNull(country_index))
                    {
                        item.Country = dr.GetString(country_index);
                    }
                    if (!dr.IsDBNull(zipCode_index))
                    {
                        item.ZipCode = dr.GetString(zipCode_index);
                    }

                    items.Add(item);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return items;
        }

        public int AddRestaurant(Restaurant restaurant)
        {
            SqlConnection conn = new SqlConnection("connectionString");
            SqlCommand cmd = conn.CreateCommand();

            int newRestaurantId = -1;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spAddRestaurant";

            cmd.Parameters.AddWithValue("@RestaurantName", restaurant.RestaurantName);
            cmd.Parameters.AddWithValue("@Adress", restaurant.Address);
            cmd.Parameters.AddWithValue("@City", restaurant.City);
            cmd.Parameters.AddWithValue("@Country", restaurant.Country);

            try
            {
                conn.Open();
                newRestaurantId = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return newRestaurantId;

        }

        public List<Review> GetReviews(int? userId)
        {
            SqlConnection conn = new SqlConnection("connectionString");
            SqlCommand cmd = conn.CreateCommand();
            List<Review> items = new List<Review>();
            Review item;

            int reviewId_index = -1;
            int restaurantId_index = -1;                      
            int reviewContent_index = -1;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetReviewsByUser";
            cmd.Parameters.AddWithValue("@userId", userId); //Get all the reviews for the ReviewUserID

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    reviewId_index = dr.GetOrdinal("ReviewID");
                    restaurantId_index = dr.GetOrdinal("RestaurantID");
                    reviewContent_index = dr.GetOrdinal("ReviewContent");
                }

                while (dr.Read())
                {
                    item = new Review();
                    if (!dr.IsDBNull(reviewId_index))
                    {
                        item.ReviewId = dr.GetInt32(reviewId_index);
                    }
                    if (!dr.IsDBNull(restaurantId_index))
                    {
                        item.ReviewRestaurantId = dr.GetInt32(restaurantId_index);
                    }
                    if (!dr.IsDBNull(reviewContent_index))
                    {
                        item.ReviewContent = dr.GetString(reviewContent_index);
                    }

                    items.Add(item);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return items;
        }

        public int AddReview(Review review)
        {
            SqlConnection conn = new SqlConnection("connectionString");
            SqlCommand cmd = conn.CreateCommand();

            int newReviewId = -1;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spAddReview";

            cmd.Parameters.AddWithValue("@ReviewRestaurantID", review.ReviewRestaurantId);
            cmd.Parameters.AddWithValue("@ReviewUserID", review.ReviewUser);
            cmd.Parameters.AddWithValue("@ReviewContent", review.ReviewContent);
            cmd.Parameters.AddWithValue("@Active", true);

            try
            {
                conn.Open();
                newReviewId = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return newReviewId;
        }

        public bool DeleteReview(int reviewId)
        {
            SqlConnection conn = new SqlConnection("connectionString");
            SqlCommand cmd = conn.CreateCommand();

            bool success = false;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeleteReview"; //sproc sets the active flag to false
                        
            cmd.Parameters.AddWithValue("@ReviewID", reviewId);
            
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return success;
        }
    }
}