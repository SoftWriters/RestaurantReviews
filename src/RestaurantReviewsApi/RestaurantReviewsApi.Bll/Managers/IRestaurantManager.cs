using RestaurantReviewsApi.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Bll.Managers
{
    public interface IRestaurantManager
    {
        public Task<RestaurantApiModel> GetRestaurantAsync(Guid restaurantId);
        public Task<Guid?> PostRestaurantAsync(RestaurantApiModel model);
        public Task<bool> DeleteRestaurantAsync(Guid restaurantId);
        public Task<Guid?> PatchRestaurantAsync(RestaurantApiModel model);
        public Task<ICollection<RestaurantApiModel>> SearchRestaurantsAsync(RestaurantSearchApiModel model);
    }
}
