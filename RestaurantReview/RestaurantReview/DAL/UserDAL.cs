using RestaurantReview.Models;
using RestaurantReview.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;

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
        //public (bool IsSuccessful, Restaurant toreturn) PostRestaurant(Restaurant restaurant)
        //{
        //    bool IsSuccessful;
        //    Restaurant toreturn = new Restaurant();
        //    using (SqlConnection conn = new SqlConnection(connectionstring))
        //    {
        //        conn.Open();
        //        SqlCommand SelectAll = new SqlCommand($"INSERT INTO RESTAURANTS VALUES(@Name, @City);", conn);
        //        try
        //        {
        //            if (!(restaurant.ValidateName() && restaurant.ValidateCity())) throw new HttpResponseException(HttpStatusCode.NotModified);
        //            SelectAll.Parameters.AddWithValue("@Name", restaurant.Name);
        //            SelectAll.Parameters.AddWithValue("@City", restaurant.City);
        //            toreturn.Name = restaurant.Name;
        //            toreturn.City = restaurant.City;
        //            SelectAll.ExecuteNonQuery();
        //            IsSuccessful = true;
        //        }
        //        catch (HttpResponseException e)
        //        {
        //            if (!restaurant.ValidateCity())
        //            {
        //                toreturn.City = "City is incorrect " + e.Message;
        //            }

        //            IsSuccessful = false;
        //            if (!restaurant.ValidateName())
        //            {
        //                toreturn.Name = "Name is too short " + e.Message + " name must be at least 1 character";
        //            }

        //            if (toreturn.Name is null) toreturn.Name = restaurant.Name;
        //            if (toreturn.City is null) toreturn.City = restaurant.City;
        //        }
        //    }
        //    return (IsSuccessful, toreturn);
        //}
        //public (bool IsSuccesssful, Review toreturn) PostReview(Review review)
        //{
        //    bool IsSuccessful;
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionstring))
        //        {
        //            if (!review.ValidateUserNameFormat()) throw new HttpResponseException(HttpStatusCode.NotModified);
        //            conn.Open();
        //            SqlCommand SelectAll = new SqlCommand($"INSERT INTO Reviews VALUES(@RestaurantId, @UserId, @ReviewText);", conn);
        //            SelectAll.Parameters.AddWithValue("@RestaurantId", review.Restaurant.RestaurantId);
        //            SelectAll.Parameters.AddWithValue("@UserId", review.User.UserId);
        //            SelectAll.Parameters.AddWithValue("@ReviewText", review.ReviewText);
        //            SelectAll.ExecuteNonQuery();
        //        }
        //    }
        //    catch
        //    {
        //        IsSuccessful = false;
        //    }

        //}

       

    }
}