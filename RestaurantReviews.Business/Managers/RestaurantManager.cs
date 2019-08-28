using RestaurantReviews.Interfaces.Business;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Business.Managers
{
    public class RestaurantManager : IRestaurantManager
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantManager(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public ICollection<IRestaurant> GetAll()
        {
            return _restaurantRepository.GetAll();
        }

        public IRestaurant GetById(long id)
        {
            return _restaurantRepository.GetById(id);
        }        

        public void Create(IRestaurant restaurant)
        {
            if (restaurant == null)
                throw new ArgumentNullException("restaurant");

            _restaurantRepository.Create(restaurant);
        }
    }
}
