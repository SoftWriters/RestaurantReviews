using Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IRestaurantRepository
    {
        IEnumerable<IRestaurantModel> AddRestaurant(IRestaurantModel restaurant);

        IEnumerable<IRestaurantModel> GetRestaurants();

        IRestaurantModel GetRestaurantById(int id);

        IEnumerable<IRestaurantModel> GetRestaurantsByCity(ICityModel city);

        bool HasData();
    }
}
