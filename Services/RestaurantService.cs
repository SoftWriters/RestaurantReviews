using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Softwriters.RestaurantReviews.Data;
using Softwriters.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softwriters.RestaurantReviews.Services
{
    public class RestaurantService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RestaurantService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetById(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not in reviews database.");
            }

            return restaurant;
        }

        //Update
        //public async Task Update(int id, )

        //Create

        //Delete
    }
}
