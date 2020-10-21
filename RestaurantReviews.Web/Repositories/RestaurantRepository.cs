using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Web.Data;
using RestaurantReviews.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Repositories
{
    public class RestaurantRepository : IRestaurantRepository 
    {
        private readonly RestaurantReviewsContext _context;

        public RestaurantRepository(RestaurantReviewsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string city = null)
        {
            var restaurants = _context.Restaurants.AsQueryable();

            if (city != null)
            {
                restaurants = restaurants.Where(r => r.City == city).AsQueryable();
            }

            return await restaurants.ToListAsync();
        }

        public async Task<Restaurant> CreateRestaurantAsync(string name, string address, string city, string state, string zipCode)
        {
            var restaurant = new Restaurant()
            {
                Name = name,
                Address = address,
                State = state,
                City = city,
                ZipCode = zipCode,
            };

            _context.Restaurants.Add(restaurant);

            await _context.SaveChangesAsync();

            return restaurant;
        }
    }
}
