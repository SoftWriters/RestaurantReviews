using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Bll.Managers
{
    public class RestaurantManager : IRestaurantManager
    {
        private readonly ILogger<RestaurantManager> _logger;
        private readonly RestaurantReviewsContext _dbContext;

        public RestaurantManager(ILogger<RestaurantManager> logger, RestaurantReviewsContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteRestaurant(Guid restaurantId)
        {
            var restaurant = await _dbContext.Restaurant.FirstOrDefaultAsync(x =>
                x.RestaurantId == restaurantId &&
                !x.IsDeleted);

            if (restaurant == null)
                return false;

            restaurant.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<RestaurantApiModel> GetRestaurant(Guid restaurantId)
        {
            var restaurant =  await _dbContext.Restaurant.FirstOrDefaultAsync(x =>
                x.RestaurantId == restaurantId &&
                !x.IsDeleted);


        }

        public async Task<bool> PatchRestaurant(RestaurantApiModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PostRestaurant(RestaurantApiModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<RestaurantApiModel>> SearchRestaurants(RestaurantSearchApiModel model)
        {
            throw new NotImplementedException();
        }
    }
}
