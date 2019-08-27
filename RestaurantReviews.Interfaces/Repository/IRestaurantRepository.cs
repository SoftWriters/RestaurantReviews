using RestaurantReviews.Interfaces.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Interfaces.Repository
{
    public interface IRestaurantRepository
    {
        ICollection<IRestaurant> GetAll();
        IRestaurant GetById(long id);
        long Create(IRestaurant restaurant);
    }
}