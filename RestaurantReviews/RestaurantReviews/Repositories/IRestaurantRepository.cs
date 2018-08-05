using RestaurantReviews.Classes;
using RestaurantReviews.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Repositories
{
    public interface IRestaurantRepository : IDisposable
    {
        Task<int> Add(Restaurant restaurant);
        Task<Restaurant> GetRestaurant(int restaurantId);
        Task<IEnumerable<Restaurant>> GetRestaurantsByLocation(RestaurantLocation restaurantLocation);
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
    }
}
