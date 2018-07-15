using System;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Repositories.Interfaces
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        IEnumerable<Restaurant> GetRestaurantsByAddress(RestaurantRequest address);
        Restaurant GetRestaurantByProperties(Restaurant restaurauntToFind);
        Restaurant GetRestaurantById(long restaurantId);
    }
}