
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReviews.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(ReviewContext context) : base(context) { }

        public override async Task<List<Restaurant>> ListAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public override async Task<Restaurant> CreateAsync(Object dto)
        {
            var restaurantDto = (RestaurantDTO)dto;
            var restaurant = new Restaurant()
            {
                Name = restaurantDto.Name,
                Address = restaurantDto.Address,
                City = restaurantDto.City,
                State = restaurantDto.State,
                Phone = restaurantDto.Phone

            };

            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();

            return restaurant;
        }

        public override async Task<Restaurant> ReadAsync(long id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public override Task<Restaurant> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Restaurant> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Restaurant>> ListRestaurantsByCityAsync(string city)
        {
            return await _context.Restaurants
                .Where(restaurant => restaurant.City.ToLower() == city.ToLower())
                .ToListAsync();
        }
    }
}
