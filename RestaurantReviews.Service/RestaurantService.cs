using RestaurantReviews.Data;
using RestaurantReviews.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly Data.Services.RestaurantService _RestaurantService;

        public RestaurantService()
        {
            _RestaurantService = new Data.Services.RestaurantService();
        }

        public void Delete(Guid id)
        {
            _RestaurantService.Delete(id);
        }

        public List<Restaurant> GetAll()
        {
            return _RestaurantService.GetAll();
        }

        public Restaurant GetByID(Guid id)
        {
            return _RestaurantService.GetByID(id);
        }
        
        public List<Restaurant> GetByCity(string city)
        {
            return _RestaurantService.GetByCity(city);
        }

        public void Save(Restaurant t)
        {
            _RestaurantService.Save(t);
        }
    }
}