using RestaurantReviews.Api.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Data
{
    public class RestaurantDAO
    {      
        //
        // In place of a real DB, I setup this in-memory data structure to contain Restaurant data
        //
        public static ConcurrentDictionary<string, Restaurant> Restaurants = new ConcurrentDictionary<string, Restaurant>();
        public static ConcurrentDictionary<Guid, Restaurant> RestaurantsById = new ConcurrentDictionary<Guid, Restaurant>();

        public static Restaurant Add(Restaurant restaurant)
        {
            if (!Restaurants.TryAdd(restaurant.GetKey(), restaurant))
                return null;

            restaurant.RestaurantId = Guid.NewGuid();
            RestaurantsById.TryAdd(restaurant.RestaurantId, restaurant);

            return restaurant;
        }

        public static Restaurant Add(string name, string streetAddress, string city, string region, string country, string postalCode)
        {
            Restaurant restaurant =  new Restaurant(name, streetAddress, city, region, country, postalCode);           

            if (!Restaurants.TryAdd(restaurant.GetKey(), restaurant))
                return null;

            restaurant.RestaurantId = Guid.NewGuid();
            RestaurantsById.TryAdd(restaurant.RestaurantId, restaurant);

            return restaurant;
        }

        public static bool Delete(Guid restaurantId)
        {
            Restaurant deleted;
            RestaurantsById.TryRemove(restaurantId, out deleted);

            if (deleted != null)
                Restaurants.TryRemove(deleted.GetKey(), out deleted);

            return (deleted != null);
        }

        public static bool Delete(string key)
        {
            Restaurant deleted;
            Restaurants.TryRemove(key, out deleted);

            if (deleted != null)
                RestaurantsById.TryRemove(deleted.RestaurantId, out deleted);

            return (deleted != null);
        }

        public static Restaurant GetRestaurantById(Guid restaurantId)
        {
            if (RestaurantsById.ContainsKey(restaurantId))
                return RestaurantsById[restaurantId];
            else
                return null;
        }

        public static List<Restaurant> GetRestaurantsByCity(string city)
        {
            List<Restaurant> results = new List<Restaurant>();

            foreach (Restaurant restaurant in Restaurants.Values)
                if (restaurant.City.Equals(city))
                    results.Add(restaurant);

            return results;
        }

        static RestaurantDAO()
        {
            Add("Taco Bell", "123 State Street", "Pittsburgh", "PA", "US", "15222");
            Add("Wendys", "234 State Street", "Pittsburgh", "PA", "US", "15222");
            Add("Burger King", "345 State Street", "Pittsburgh", "PA", "US", "15222");
            Add("Pinera Bread", "555 Bloom Street", "Cleveland", "OH", "US", "15456");
            Add("Chilis", "666 Bloom Street", "Cleveland", "OH", "US", "15456");
            Add("Bravos", "123 BuckEye Street", "Columbus", "OH", "US", "15756");
        }
    }
}
