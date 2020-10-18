using RestaurantReviewsApi.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Bll.Managers
{
    public interface IRestaurantManager
    {
        public Task<RestaurantApiModel> GetRestaurant(Guid restaurantId);
        public Task<bool> PostRestaurant(RestaurantApiModel model);
        public Task<bool> DeleteRestaurant(Guid restaurantId);
        public Task<bool> PatchRestaurant(RestaurantApiModel model);
        public IAsyncEnumerable<RestaurantApiModel> SearchRestaurants(RestaurantSearchApiModel model);
    }
}
