using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Translators;
using RestaurantReviewsApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Bll.Managers
{
    public class RestaurantManager : IRestaurantManager
    {
        private readonly ILogger<RestaurantManager> _logger;
        private readonly RestaurantReviewsContext _dbContext;
        private readonly IApiModelTranslator _translator;

        public RestaurantManager(ILogger<RestaurantManager> logger, RestaurantReviewsContext dbContext, IApiModelTranslator translator)
        {
            _logger = logger;
            _dbContext = dbContext;
            _translator = translator;
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

            return _translator.ToRestaurantApiModel(restaurant);
        }

        public async Task<bool> PatchRestaurant(RestaurantApiModel model)
        {
            var restaurant = await _dbContext.Restaurant.FirstOrDefaultAsync(x =>
               x.RestaurantId == model.RestaurantId &&
               !x.IsDeleted);

            if (restaurant == null)
                return false;

            _translator.ToRestaurantModel(model, restaurant);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PostRestaurant(RestaurantApiModel model)
        {
            var restaurant = _translator.ToRestaurantModel(model);
            _dbContext.Restaurant.Add(restaurant);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async IAsyncEnumerable<RestaurantApiModel> SearchRestaurants(RestaurantSearchApiModel model)
        {
            var restaurants = _dbContext.Restaurant.AsNoTracking().Where(x => !x.IsDeleted);

            if (model.Name != null)
                restaurants = restaurants.Where(x => x.Name.StartsWith(model.Name));
            if (model.AddressLine1 != null)
                restaurants = restaurants.Where(x => x.AddressLine1.StartsWith(model.AddressLine1));
            if (model.City != null)
                restaurants = restaurants.Where(x => x.City == model.City);
            if (model.State != null)
                restaurants = restaurants.Where(x => x.State == model.State);
            if (model.ZipCode != null)
                restaurants = restaurants.Where(x => x.ZipCode.StartsWith(model.ZipCode));

            var ret =  await restaurants.ToListAsync();

            foreach(var r in ret)
            {
                yield return _translator.ToRestaurantApiModel(r);
            }
        }
    }
}
