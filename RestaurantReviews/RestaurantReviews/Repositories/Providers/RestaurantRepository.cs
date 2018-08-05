using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReviews.Classes;
using RestaurantReviews.Data;
using RestaurantReviews.Entities;

namespace RestaurantReviews.Repositories.Providers
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantReviewsContext context = new RestaurantReviewsContext();

        public async Task<int> Add(Restaurant restaurant)
        {
            context.Restaurants.Add(restaurant);
            await context.SaveChangesAsync();

            return restaurant.Id;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return await context.Restaurants.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<Restaurant> GetRestaurant(int restaurantId)
        {
            return await context.Restaurants.SingleOrDefaultAsync(r => r.Id == restaurantId);
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsByLocation(RestaurantLocation restaurantLocation)
        {
            var restaurants = context.Restaurants.AsQueryable();

            if (!string.IsNullOrEmpty(restaurantLocation.City))
                restaurants = restaurants.Where(r => r.RestaurantLocation.City.Contains(restaurantLocation.City));

            if (!string.IsNullOrEmpty(restaurantLocation.State))
                restaurants = restaurants.Where(r => r.RestaurantLocation.State.Contains(restaurantLocation.State));

            if (!string.IsNullOrEmpty(restaurantLocation.ZipCode))
                restaurants = restaurants.Where(r => r.RestaurantLocation.ZipCode.Contains(restaurantLocation.ZipCode));

            return await restaurants.OrderBy(r => r.Id).ToListAsync();
        }

        public void Dispose() => context.Dispose();
    }
}
