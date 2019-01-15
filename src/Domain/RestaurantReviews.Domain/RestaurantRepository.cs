using RestaurantReviews.Common;
using RestaurantReviews.Data;
using RestaurantReviews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain
{

    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetRestaurantsAsync(int page, int pagesize, DbFilter<Restaurant> filter);
        Task<Restaurant> GetRestaurantAsync(int id);
        Task<Restaurant> CreateRestaurantAsync(string name, string address, string city);
        Task DeleteRestaurantAsync(int id);
    }

    public class RestaurantRepository : IRestaurantRepository
    {
        private IRestaurantDataManager _restaurantDataManager;

        public RestaurantRepository(IRestaurantDataManager restaurantDataManager)
        {
            _restaurantDataManager = restaurantDataManager;
        }

        public Task<Restaurant> CreateRestaurantAsync(string name, string address, string city)
        {
            var restaurant = new Restaurant() { Name = name, Address = address, City = city };
            return  _restaurantDataManager.CreateRestaurantAsync(restaurant);
        }

        public Task DeleteRestaurantAsync(int id)
        {
           return _restaurantDataManager.DeleteRestaurantAsync(id);
        }

        public Task<Restaurant> GetRestaurantAsync(int id)
        {
            return _restaurantDataManager.GetRestaurantAsync(id);
        }

        public Task<IEnumerable<Restaurant>> GetRestaurantsAsync(int page, int pagesize, DbFilter<Restaurant> filter)
        {
            return _restaurantDataManager.GetRestaurantsAsync(page, pagesize, filter);
        }
    }
}
