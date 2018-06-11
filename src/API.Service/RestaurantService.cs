/******************************************************************************
 * Name: RestaurantService.cs
 * Purpose: Restaurant Service class definition
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Data;

namespace RestaurantReviews.API.Service
{
    public class RestaurantService : APIServiceBase, IRestaurantService
    {
        IRestaurantRepository restaurantRepository;

        public RestaurantService()
        {
            this.restaurantRepository = new RestaurantRespository();
        }

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public RestaurantModelList GetRestaurants(string city)
        {
            if(string.IsNullOrEmpty(city) || city.Length > 50)
            {
                throw new Exception("Invalid City. Is either empty or null or longer than 50 characters");
            }
            return this.restaurantRepository.GetRestaurants(city);
        }

        public RestaurantModelDTO GetRestaurantById(int id)
        {
            if(id <= 0)
            {
                throw new Exception("Restaurant Id is negative number");
            }
            return this.restaurantRepository.GetRestaurantById(id);
        }

        public RestaurantModelDTO AddRestaurant(RestaurantModelDTO newRestaurant)
        {
            ValidateModel(newRestaurant);
            if (this.restaurantRepository.CheckRestaurantExists(newRestaurant))
                throw new Exception("Restaurant already exists");
            return this.restaurantRepository.AddRestaurant(newRestaurant);
        }
    }
}
