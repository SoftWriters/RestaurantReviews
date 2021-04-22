using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IRestaurantRepository
    {
        Restaurant ReadRestaurant(int id);
        void CreateRestaurant(Restaurant restaurant);
        IList<Restaurant> ReadRestaurantsByCity(int cityId);
        IList<Restaurant> ReadAllRestaurants();
    }
}
