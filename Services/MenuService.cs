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
    public class MenuService : IMenuService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public MenuService(DataContext context, IMapper mapper, IServiceHelper serviceHelper)
        {
            _context = context;
            _mapper = mapper;
            _serviceHelper = serviceHelper;
        }

        public async Task<IEnumerable<Menu>> GetAll()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu> GetById(int id)
        {
            return await _serviceHelper.GetMenu(id);
        }

        public async Task Update(int id, MenuRequest dto)
        {
            var menu = await _serviceHelper.GetMenu(id);
            _ = _mapper.Map(dto, menu);
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
        }

        public async Task Create(MenuRequest dto)
        {
            var menu = _mapper.Map<Menu>(dto);
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var menu = await _serviceHelper.GetMenu(id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }
    }
}
