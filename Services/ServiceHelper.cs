using Softwriters.RestaurantReviews.Models;
using System.Collections.Generic;

namespace Softwriters.RestaurantReviews.Services
{
    public class ServiceHelper
    {
        private readonly RestaurantService _restaurantService;

        public ServiceHelper(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        private Restaurant GetRestaurant(int id)
        {
            var restaurant = _restaurantService.Context.Restaurants.Find(id);
            if (restaurant == null) throw new KeyNotFoundException("Restaurant not found");
            return restaurant;
        }

        private RestaurantType GetRestaurantType(int id)
        {
            var restaurantType = _restaurantService.Context.RestaurantTypes.Find(id);
            if (restaurantType == null) throw new KeyNotFoundException("RestaurantType not found");
            return restaurantType;
        }

        private City GetCity(int id)
        {
            var city = _restaurantService.Context.Cities.Find(id);
            if (city == null) throw new KeyNotFoundException("City not found");
            return city;
        }

        private Menu GetMenu(int id)
        {
            var menu = _restaurantService.Context.Menus.Find(id);
            if (menu == null) throw new KeyNotFoundException("Menu not found");
            return menu;
        }
    }
}