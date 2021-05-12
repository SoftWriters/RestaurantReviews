using DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
  public interface IRestaurantRepo
  {
    Task AddRestaruantAsync(RestaurantDTO newRestaurant);
    Task<IEnumerable<RestaurantDTO>> GetRestaurants(long cityId, string cuisine, bool sortByAvg);
  }
}
