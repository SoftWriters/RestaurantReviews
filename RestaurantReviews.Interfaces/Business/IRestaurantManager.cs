using RestaurantReviews.Interfaces.Models;
using System.Collections.Generic;

namespace RestaurantRestaurants.Interfaces.Business
{
    public interface IRestaurantManager
    {
        ICollection<IRestaurant> GetAll();
        IRestaurant GetById(long id);
        void Create(IRestaurant restaurant);
    }
}
