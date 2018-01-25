using RestaurantReviews.Data;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Service.Interfaces
{
    public interface IRestaurantService
    {
        void Delete(Guid id);

        List<Restaurant> GetAll();

        Restaurant GetByID(Guid id);

        List<Restaurant> GetByCity(string city);

        void Save(Restaurant t);
    }
}
