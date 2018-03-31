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
    /// Provides persistance methods and retrieval for review instances.
    /// </summary>
    internal static class ReviewSQL
    {
        #region private methods
        private static List<Review> EnumerateReviewSqlDataReader(SqlDataReader reader)
        {
            List<Review> reviews = new List<Review>();

            while (reader.Read())
            {
                Review review = new Review();
                review.Id = (long)reader["id"];
                review.RestaurantId = (long)reader["restaurant_id"];
                review.MemberId = (long)reader["member_id"];
                review.Body = (string)reader["body"];
                reviews.Add(review);
            }

            return reviews;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Persists a new instance of a review.
        /// </summary>
        /// <param name="review">The Review instance to persist.</param>
        public static void CreateReview(Review review)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("CreateReview", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@reviewId", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Output });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantId", SqlDbType = SqlDbType.BigInt, Value = review.RestaurantId });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@memberId", SqlDbType = SqlDbType.BigInt, Value = review.MemberId });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@body", SqlDbType = SqlDbType.NVarChar, Value = review.Body });

                    if (com.ExecuteNonQuery() != 1)
                        throw (new PersistanceException("Failed to create review."));

                    review.Id = (long)com.Parameters["@reviewId"].Value;
                }
                con.Close();
            }
        }

        /// <summary>
        /// Updates a previously created Review instance.
        /// </summary>
        /// <param name="review">The Review instance to update.</param>
        public static void UpdateReview(Review review)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("UpdateReview", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@reviewId", SqlDbType = SqlDbType.BigInt, Value = review.Id });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantId", SqlDbType = SqlDbType.BigInt, Value = review.RestaurantId });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@memberId", SqlDbType = SqlDbType.BigInt, Value = review.MemberId });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@body", SqlDbType = SqlDbType.NVarChar, Value = review.Body });

                    if (com.ExecuteNonQuery() != 1)
                        throw (new PersistanceException($"Failed to update review, ID={review.Id}."));
                }
                con.Close();
            }
        }

        /// <summary>
        /// Retrieves a previously persisted Review instance.
        /// </summary>
        /// <param name="reviewId">The ID of the Review to retrieve.</param>
        /// <returns>An instance of Review.</returns>
        public static Review GetReview(long reviewId)
        {
            Review review = null;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("GetReview", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@reviewId", SqlDbType = SqlDbType.BigInt, Value = reviewId });

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            throw (new RetrievalException($"Failed to retrieve review instance, ID={reviewId}."));

                        reader.Read();

                        review = new Review();
                        review.Id = (long)reader["id"];
                        review.RestaurantId = (long)reader["restaurant_id"];
                        review.MemberId = (long)reader["member_id"];
                        review.Body = (string)reader["body"];

                        reader.Close();
                    }
                }
                con.Close();
            }

            return review;
        }

        /// <summary>
        /// Retrieves Reviews for a specific Restaurant.
        /// </summary>
        /// <param name="restaurantId">The Restaurant ID to retrieve Reviews for.</param>
        /// <returns>A collection of Reviews.</returns>
        public static List<Review> GetReviewsByRestaurant(long restaurantId)
        {
            List<Review> reviews = null;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("GetReviewsByRestaurant", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@restaurantId", SqlDbType = SqlDbType.BigInt, Value = restaurantId });

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        reviews = EnumerateReviewSqlDataReader(reader);

                        reader.Close();
                    }
                }
                con.Close();
            }

            return reviews;
        }

        /// <summary>
        /// Retrievs Review instances for a specific Member.
        /// </summary>
        /// <param name="memberId">The Member ID to retieve Reviews for.</param>
        /// <returns>A collection of Reviews.</returns>
        public static List<Review> GetReviewsByMember(long memberId)
        {
            List<Review> reviews = null;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("GetReviewsByMember", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@memberId", SqlDbType = SqlDbType.BigInt, Value = memberId });

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        reviews = EnumerateReviewSqlDataReader(reader);

                        reader.Close();
                    }
                }
                con.Close();
            }

            return reviews;
        }
        /// <summary>
        /// Deletes a previously persisted Review.
        /// </summary>
        /// <param name="reviewId">The ID of the Review to delete.</param>
        public static void DeleteReview(long reviewId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("DeleteReview", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@reviewId", SqlDbType = SqlDbType.BigInt, Value = reviewId });

                    if (com.ExecuteNonQuery() <= 0)
                        throw (new PersistanceException($"Failed to delete review, ID={reviewId}."));
                }
                con.Close();
            }
        }
        #endregion
    }
}
