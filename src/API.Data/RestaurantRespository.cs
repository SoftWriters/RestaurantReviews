/******************************************************************************
 * Name: RestaurantRepository.cs
 * Purpose: Wrapper Repository class for Restaurant
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Data.SqlServer;

namespace RestaurantReviews.API.Data
{
    public class RestaurantRespository : IRestaurantRepository
    {
        IRestaurantRepository restaurantRepository = null;

        public RestaurantRespository(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public RestaurantRespository()
        {
            this.restaurantRepository = new SqlRestaurantRepository();
        }

        public RestaurantModelList GetRestaurants(string city)
        {
            return this.restaurantRepository.GetRestaurants(city);
        }

        public RestaurantModelDTO GetRestaurantById(int id)
        {
            return this.restaurantRepository.GetRestaurantById(id);
        }

        public bool CheckRestaurantExists(RestaurantModelDTO restaurant)
        {
            return this.restaurantRepository.CheckRestaurantExists(restaurant);
        }

        public RestaurantModelDTO AddRestaurant(RestaurantModelDTO newRestaurant)
        {
            return this.restaurantRepository.AddRestaurant(newRestaurant);
        }
    }
}
