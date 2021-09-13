using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Softwriters.RestaurantReviews.Data;
using Softwriters.RestaurantReviews.Dto;
using Softwriters.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softwriters.RestaurantReviews.Services
{
    public class RestaurantService
    {
        public readonly DataContext Context;
        private readonly IMapper _mapper;
        private readonly ServiceHelper _serviceHelper;

        public RestaurantService(DataContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
            _serviceHelper = new ServiceHelper(this);
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await Context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetById(int id)
        {
            var restaurant = await Context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not in reviews database.");
            }

            return restaurant;
        }

        public async Task Update(int id, RestaurantRequest dto)
        {
            //Restaurant restaurant = new Restaurant();
            //restaurant.Name = "name";
            //restaurant.RestaurantTypeId = 1;
            //restaurant.CityId = 1;
            //restaurant.MenuId = 1;
            //restaurant.IsDeleted = false;
        }

        //Create

        //Delete
    }
}
