﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Softwriters.RestaurantReviews.Data;
using Softwriters.RestaurantReviews.Dto;
using Softwriters.RestaurantReviews.Models;
using Softwriters.RestaurantReviews.Services.Helpers;
using Softwriters.RestaurantReviews.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softwriters.RestaurantReviews.Services
{
    public class RestaurantTypeService : IRestaurantTypeService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public RestaurantTypeService(DataContext context, IMapper mapper, IServiceHelper serviceHelper)
        {
            _context = context;
            _mapper = mapper;
            _serviceHelper = serviceHelper;
        }

        public async Task<IEnumerable<RestaurantType>> GetAll()
        {
            return await _context.RestaurantTypes.ToListAsync();
        }

        public async Task<RestaurantType> GetById(int id)
        {
            return await _serviceHelper.GetRestaurantType(id);
        }

        public async Task Update(int id, RestaurantTypeRequest dto)
        {
            var restaurantType = await _serviceHelper.GetRestaurantType(id);
            _ = _mapper.Map(dto, restaurantType);
            _context.RestaurantTypes.Update(restaurantType);
            await _context.SaveChangesAsync();
        }

        public async Task Create(RestaurantTypeRequest dto)
        {
            var restaurantType = _mapper.Map<RestaurantType>(dto);
            _context.RestaurantTypes.Add(restaurantType);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var restaurantType = await _serviceHelper.GetRestaurantType(id);
            _context.RestaurantTypes.Remove(restaurantType);
            await _context.SaveChangesAsync();
        }
    }
}
