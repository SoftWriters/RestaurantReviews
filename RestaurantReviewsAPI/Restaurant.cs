using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsAPI
{
    class Restaurant
    {
        public int restaurantID { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public bool disposed { get; set; }

        public Restaurant(string name, string address, string city, string state, string zipCode)
        {
            // restaurantID would be incremented in the database
            this.name = name;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
            this.disposed = false;
        }

        // This is a placeholder for the unknown data source of the larger system
        List<Restaurant> restaurants = new List<Restaurant>();

        // Return all Restaurants in a given city
        public List<Restaurant> GetRestaurantsByCity(string city)
        {
            List<Restaurant> matchedRestaurants = new List<Restaurant>();
            foreach (Restaurant rest in restaurants)
            {
                if (rest.name.Equals(city))
                {
                    matchedRestaurants.Add(rest);
                }
            }
            return matchedRestaurants;
        }

        // Post a Restaurant not in the database
        public string PostRestaurant(string name, string address, string city, string zipCode)
        {
            string response = "";
            if (!restaurantExists(name, address))
            {
                Restaurant rest = new Restaurant(name, address, city, state, zipCode);
                restaurants.Add(rest);
                response = "Restauraunt added successfully.";
            }
            else
            {
                response = "A restaurant with that name and address already exists in our database.";
            }
            return response;
        }

        // Check if Restaurant with the supplied name and address already exists
        public bool restaurantExists(string name, string address)
        {
            bool exists = false;
            foreach (Restaurant rest in restaurants)
            {
                if (rest.name == name && rest.address == address)
                {
                    exists = true;
                }
            }
            return exists;
        }
    }
}
