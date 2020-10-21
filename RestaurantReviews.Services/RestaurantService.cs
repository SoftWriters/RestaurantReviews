using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Services
{
    public class RestaurantService : IRestaurantService
    {
        private RestaurantContext _context;
        public RestaurantService(RestaurantContext context)
        {
            _context = context;

        }
        public void CreateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
        }

        public async Task<IEnumerable<Restaurant>> GetAllByCity(string city)
        {
            return await _context.Restaurants.Where(_ => _.City == city).ToListAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetRestaurant(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void UpdateRestaurant(int id, Restaurant restaurant)
        {
            _context.Entry(restaurant).State = EntityState.Modified;
        }
    }
}
