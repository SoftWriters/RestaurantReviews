using RestaurantReview.Models;
using RestaurantReview.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RestaurantReview.DAL
{
    public class UserDAL
    {
        private readonly string connectionstring;

        public UserDAL(string connString)
        {
            this.connectionstring = new Conn().AWSconnstring();
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand SelectAll = new SqlCommand("SELECT * FROM Users ", conn);
                SqlDataReader reader = SelectAll.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserName = Convert.ToString(reader["UserName"]),
                        UserId = Convert.ToInt32(reader["UserId"])
                    });
                }
            }
            return users;
        }

        public (bool IsSuccessful, User usertoreturn) GetUser(string username)
        {
            bool IsSuccessful;
            User usermatch;
            try
            {
                usermatch = GetUsers().FirstOrDefault(user => user.UserName.ToLower().Equals(username.ToLower()));
                IsSuccessful = true;
                if (usermatch.UserName is null) throw new Exception();
            }
            catch
            {
                IsSuccessful = false;
                usermatch = new User();
                usermatch.UserName = "none";
            }
            return (IsSuccessful, usermatch);
        }
    }
}