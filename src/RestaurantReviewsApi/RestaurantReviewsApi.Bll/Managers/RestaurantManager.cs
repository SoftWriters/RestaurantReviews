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

        public async Task<bool> DeleteRestaurantAsync(Guid restaurantId)
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

        public async Task<RestaurantApiModel> GetRestaurantAsync(Guid restaurantId)
        {
            var restaurant =  await _dbContext.Restaurant.FirstOrDefaultAsync(x =>
                x.RestaurantId == restaurantId &&
                !x.IsDeleted);

            if (restaurant == null)
                return null;

            return _translator.ToRestaurantApiModel(restaurant, await RestaurantAverageRating(restaurant.RestaurantId));
        }

        public async Task<bool> PatchRestaurantAsync(RestaurantApiModel model)
        {
            var restaurant = await _dbContext.Restaurant.FirstOrDefaultAsync(x =>
               x.RestaurantId == model.RestaurantId);

            if (restaurant == null)
                return false;

            _translator.ToRestaurantModel(model, restaurant);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PostRestaurantAsync(RestaurantApiModel model)
        {
            var restaurant = _translator.ToRestaurantModel(model);
            _dbContext.Restaurant.Add(restaurant);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        //TODO Expand this search functionality to better handle fuzzy matching and to search for keywords in description
        public async Task<ICollection<RestaurantApiModel>> SearchRestaurantsAsync(RestaurantSearchApiModel model)
        {
            var restaurants = _dbContext.Restaurant.AsNoTracking()
                .Where(x => !x.IsDeleted);

            if (model.Name != null)
                restaurants = restaurants.Where(x => x.Name.StartsWith(model.Name));
            if (model.City != null)
                restaurants = restaurants.Where(x => x.City == model.City);
            if (model.State != null)
                restaurants = restaurants.Where(x => x.State == model.State);
            if (model.ZipCode != null)
                restaurants = restaurants.Where(x => x.ZipCode.StartsWith(model.ZipCode));

            var restaurantList =  await restaurants.ToListAsync();

            List<RestaurantApiModel> returnList = new List<RestaurantApiModel>();

            restaurantList.ForEach(async x =>
            {
                returnList.Add(_translator.ToRestaurantApiModel(x, await RestaurantAverageRating(x.RestaurantId)));
            });

            return returnList;      
        }

        private async Task<float?> RestaurantAverageRating(Guid restaurantId)
        {
            var reviewList = await _dbContext.Review.AsNoTracking()
                .Where(r => r.RestaurantId == restaurantId && !r.IsDeleted).ToListAsync();
            if (reviewList.Count == 0)
                return null;

            return (float?)reviewList.Average(r => r.Rating);
        }
    }
}
