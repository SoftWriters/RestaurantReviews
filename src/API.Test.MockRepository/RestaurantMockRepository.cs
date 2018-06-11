/******************************************************************************
 * Name: RestaurantMockRespository.cs
 * Purpose: In Memory Mock Respository of Restaurants for unit test cases
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Model.DTO;

namespace RestaurantReviews.API.Test.MockRepository
{
    public class RestaurantMockRepository : IRestaurantRepository
    {
        private RestaurantModelList restaurants;

        public RestaurantMockRepository()
        {
            this.restaurants = new RestaurantModelList();
            this.restaurants.RestaurantList = new List<RestaurantModelDTO>()
            {

                new RestaurantModelDTO() {
                    Id = 1,
                    Name = "Restaurant 1",
                    City = "Pittsburgh"
                },

                new RestaurantModelDTO() {
                    Id = 2,
                    Name = "Restaurant 2",
                    City = "Pittsburgh"
                },

                new RestaurantModelDTO() {
                    Id = 3,
                    Name = "Restaurant 3",
                    City = "Morgantown"
                },

                new RestaurantModelDTO() {
                    Id = 4,
                    Name = "Restaurant 4",
                    City = "New Stanton"
                }
            };
        }

        protected RestaurantModelList GetRestaurants()
        {
            return restaurants;
        }

        public RestaurantModelList GetRestaurants(string city)
        {
            return new RestaurantModelList()
            {
                RestaurantList = GetRestaurants().RestaurantList.Where(x => x.City.CompareTo(city) == 0).ToList()
            };
        }

        public RestaurantModelDTO GetRestaurantById(int id)
        {
            return GetRestaurants().RestaurantList.Find(x => x.Id == id);
        }

        public bool CheckRestaurantExists(RestaurantModelDTO restaurant)
        {
            return (GetRestaurants().RestaurantList.Find(x => ((x.Name.CompareTo(restaurant.Name) == 0) && (x.City.CompareTo(restaurant.City) == 0))) != null);
        }

        public RestaurantModelDTO AddRestaurant(RestaurantModelDTO newRestaurant)
        {
            RestaurantModelDTO lastRestaurant = GetRestaurants().RestaurantList.Last();
            newRestaurant.Id = lastRestaurant.Id + 1;
            restaurants.RestaurantList.Add(newRestaurant);

            return newRestaurant;
        }
    }
}
