using RestaurantReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.Helpers
{
    public static class SessionHelper
    {
        private const string UserKey = "User";

        public static int UserID
        {
            get
            {
                return GetUser().ID;
            }
        }

        public static void Clear()
        {
            ClearUser();
            HttpContext.Current.Session.Abandon();
        }

        public static void ClearUser()
        {
            SetUser(null);
        }

        public static bool HasUser()
        {
            return (Get<UserModel>(UserKey) != null);   
        }

        public static UserModel GetUser()
        {
            return Get<UserModel>(UserKey) ?? new UserModel();
        }

        public static T Get<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        public static void SetUser(UserModel user)
        {
            Set<UserModel>(UserKey, user);
        }

        public static void Set<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}