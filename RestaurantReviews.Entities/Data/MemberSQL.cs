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
    /// Provides persistance and retrieval for members.
    /// </summary>
    internal static class MemberSQL
    {
        /// <summary>
        /// Creates a new member instance.
        /// </summary>
        /// <param name="member">The member instance to create.</param>
        public static void CreateMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("CreateMember", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@memberid", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Output });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@username", SqlDbType = SqlDbType.NVarChar, Value = member.UserName });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@firstname", SqlDbType = SqlDbType.NVarChar, Value = member.FirstName ?? (object)DBNull.Value });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@lastname", SqlDbType = SqlDbType.NVarChar, Value = member.LastName ?? (object)DBNull.Value });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@email", SqlDbType = SqlDbType.NVarChar, Value = member.Email ?? (object)DBNull.Value });

                    if (com.ExecuteNonQuery() != 1)
                        throw (new PersistanceException("Failed to create member."));

                    member.Id = (long)com.Parameters["@memberid"].Value;
                }
                con.Close();
            }
        }
        /// <summary>
        /// Updates an existing member instance.
        /// </summary>
        /// <param name="member">The member instance to update.</param>
        public static void UpdateMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("UpdateMember", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@memberid", SqlDbType = SqlDbType.BigInt, Value = member.Id });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@username", SqlDbType = SqlDbType.NVarChar, Value = member.UserName });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@firstname", SqlDbType = SqlDbType.NVarChar, Value = member.FirstName ?? (object)DBNull.Value });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@lastname", SqlDbType = SqlDbType.NVarChar, Value = member.LastName ?? (object)DBNull.Value });
                    com.Parameters.Add(new SqlParameter { ParameterName = "@email", SqlDbType = SqlDbType.NVarChar, Value = member.Email ?? (object)DBNull.Value });

                    if (com.ExecuteNonQuery() != 1)
                        throw (new PersistanceException($"Failed to update member, ID={member.Id}."));
                }
                con.Close();
            }
        }
        /// <summary>
        /// Deletes an existing member.
        /// </summary>
        /// <param name="member">The member to delete.</param>
        public static void DeleteMember(long memberId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("DeleteMember", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@memberid", SqlDbType = SqlDbType.BigInt, Value = memberId });

                    if (com.ExecuteNonQuery() <= 0)
                        throw (new PersistanceException($"Failed to delete member, ID={memberId}."));
                }
                con.Close();
            }
        }
        /// <summary>
        /// Retrieves an existing member instance.
        /// </summary>
        /// <param name="memberId">The ID of the member to retrieve.</param>
        /// <returns>A member instance.</returns>
        public static Member GetMember(long memberId)
        {
            Member member = null;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("GetMember", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter { ParameterName = "@memberid", SqlDbType = SqlDbType.BigInt, Value = memberId });

                    using(SqlDataReader reader = com.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            throw (new RetrievalException($"Failed to retrieve member instance, ID={memberId}."));

                        reader.Read();

                        member = new Member();
                        member.Id = (long)reader["id"];
                        member.UserName = (string)reader["username"];
                        member.FirstName = reader["firstname"] == DBNull.Value ? null : (string)reader["firstname"];
                        member.LastName = reader["lastname"] == DBNull.Value ? null : (string)reader["lastname"];
                        member.Email = reader["email"] == DBNull.Value ? null : (string)reader["email"];

                        reader.Close();
                    }
                }
                con.Close();
            }

            return member;
        }
    }
}
