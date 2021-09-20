using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.DataAccess
{
    public static class RestaurantManager
    { 
        public static List<RestaurantInfoModel> GetRestaurants(int ID, string city)
        {
            city = string.IsNullOrWhiteSpace(city) ? null : city;
            return DBCaller.CreateModelList<RestaurantInfoModel>("proc_GetRestaurant", DBCaller.CreateParameterList("@ID", ID, "@City", city));
        }

        public static List<RestaurantInfoModel> GetAllRestaurants()
        {
            return GetRestaurants(0, null);
        }

        public static RestaurantInfoModel GetRestaurant(int ID)
        {
            return GetRestaurants(ID, null)[0];
        }
        public static RestaurantViewModel GetRestaurantViewModel(string city)
        {
            return new RestaurantViewModel
            {
                City = city,
                Restaurants = GetRestaurants(0, city)
            };
        }

        public static int InsertRestaurant(int userID, string name, string city, string description)
        {
            return UpdateRestaurant(0, userID, name, city, description);
        }

        public static int UpdateRestaurant(RestaurantModel restaurant)
        {
            return UpdateRestaurant(restaurant.ID, restaurant.UserID, restaurant.Name, restaurant.City, restaurant.Description);
        }

        public static int UpdateRestaurant(int ID, int userID, string name, string city, string description)
        {
            return DBCaller.Call("proc_UpdateRestaurant", DBCaller.CreateParameterList("@ID", ID, "@UserID", userID, "@Name", name, "@City", city, "@Description", description));
        }
    }
}
