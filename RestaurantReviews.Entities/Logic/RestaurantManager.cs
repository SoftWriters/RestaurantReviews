using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities.Logic
{
    /// <summary>
    /// Exposes restaurant business logic.
    /// </summary>
    public static class RestaurantManager
    {
        /// <summary>
        /// Validates that a restaurant instance has the necessary information before persisting.
        /// </summary>
        /// <param name="member"></param>
        private static void ValidateRestaurant(Restaurant restaurant)
        {
            if (string.IsNullOrWhiteSpace(restaurant.Name))
                throw (new System.ArgumentException("Restaurant name cannot be null or whitespace."));
            else if (restaurant.Name.Length > 100)
                throw (new System.ArgumentException("Restaurant name exceeds maximum length of 100 characters."));
        }

        /// <summary>
        /// Creates a restaurant instance.
        /// </summary>
        /// <param name="name">The name of the restaurant.</param>
        /// <returns>A restaurant instance.</returns>
        public static Restaurant CreateRestaurant(string name)
        {
            Restaurant restaurant = new Restaurant { Name = name };

            CreateRestaurant(restaurant);

            return restaurant;
        }
        /// <summary>
        /// Persist a new restaurant instance.
        /// </summary>
        /// <param name="restaurant">The restaurant to persist.</param>
        public static void CreateRestaurant(Restaurant restaurant)
        {
            if (restaurant.Id != -1)
                throw (new System.ArgumentException("Restaurant is not a new instance."));

            ValidateRestaurant(restaurant);

            Data.RestaurantSQL.CreateRestaurant(restaurant);
        }
        /// <summary>
        /// Updates a restaurant instance.
        /// </summary>
        /// <param name="restaurantId">The id of the restaurant to persist.</param>
        /// <param name="name">The name of the restaurant.</param>
        /// <returns>An instance of the updated restaurant.</returns>
        public static Restaurant UpdateRestaurant(long restaurantId, string name)
        {
            Restaurant restaurant = new Restaurant();
            restaurant.Id = restaurantId;
            restaurant.Name = name;

            UpdateRestaurant(restaurant);

            return restaurant;
        }
        /// <summary>
        /// Updates a restaurant instance.
        /// </summary>
        /// <param name="restaurant">The restaurant to persist.</param>
        public static void UpdateRestaurant(Restaurant restaurant)
        {
            if (restaurant.Id == -1)
                throw (new System.ArgumentException("Restaurant is new instance and needs to be saved before updating."));

            ValidateRestaurant(restaurant);

            Data.RestaurantSQL.UpdateRestaurant(restaurant);
        }
        /// <summary>
        /// Retrieves a previously persist restaurant instance.
        /// </summary>
        /// <param name="restaurantId">The id of the restaurant to retrieve.</param>
        /// <returns>An instance of the restaurant specified by the id.</returns>
        public static Restaurant GetRestaurant(long restaurantId)
        {
            return Data.RestaurantSQL.GetRestaurant(restaurantId);
        }
        /// <summary>
        /// Retrieves all Restaurants in a given City and Region/State.
        /// </summary>
        /// <param name="city">City of interest.</param>
        /// <param name="region">Region/State of interest.</param>
        /// <returns>A list of Restaurants.</returns>
        public static List<Restaurant> GetRestaurantsByCityRegion(string city, string region)
        {
            if (string.IsNullOrWhiteSpace(city))
                city = null;
            else if (city.Length > 100)
                throw (new System.ArgumentException("City name exceeds maximum length of 100 characters."));

            if (string.IsNullOrWhiteSpace(region))
                region = null;
            else if (region.Length > 100)
                throw (new System.ArgumentException("Region name exceeds maximum length of 100 characters."));

            return Data.RestaurantSQL.GetRestaurantsByCityRegion(city, region);
        }
        /// <summary>
        /// Deletes a restaurant instance.
        /// </summary>
        /// <param name="restaurantId">The id of the restaurant to delete.</param>
        public static void DeleteRestaurant(long restaurantId)
        {
            Data.RestaurantSQL.DeleteRestaurant(restaurantId);
        }
        /// <summary>
        /// Deletes a restaurant instance.
        /// </summary>
        /// <param name="restaurant">The restaurant to delete.</param>
        public static void DeleteRestaurant(Restaurant restaurant)
        {
            DeleteRestaurant(restaurant.Id);
        }
    }
}
