using RestaurantReviews.API.Models;
using System.Collections.Generic;

namespace RestaurantReviews.API.Repository
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetAll();
        Restaurant GetById(long id);
        long Create(Restaurant restaurant);
    }
}