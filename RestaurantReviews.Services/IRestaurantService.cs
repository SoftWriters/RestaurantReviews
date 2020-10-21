using RestaurantReviews.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Services
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<IEnumerable<Restaurant>> GetAllByCity(string city);
        Task<Restaurant> GetRestaurant(int id);
        void UpdateRestaurant(int id, Restaurant restaurant);
        void CreateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
        bool RestaurantExists(int id);
        Task<int> SaveChanges();
    }
}
