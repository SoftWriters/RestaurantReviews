using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories
{ 
    public interface IRestaurantRepository
    {
        bool HasData();
        IEnumerable<IRestaurantModel> GetAllRestaurants();

        IRestaurantModel GetRestaurant(int id);

        IEnumerable<IRestaurantModel> GetRestaurantsByCity(ICityModel city);

        IEnumerable<IRestaurantModel> AddRestaurant(IRestaurantModel restaurant);
    }
}
