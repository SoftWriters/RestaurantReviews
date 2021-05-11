using DTOs;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
  public class RestaurantRepoMongoDb : IRestaurantRepo
  {
    private readonly string connectionString;

    public RestaurantRepoMongoDb(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public Task AddRestaruantAsync(RestaurantDTO newRestaurant)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<RestaurantDTO>> GetRestaurants(long cityId, string cuisine, bool sortByAvg)
    {
      throw new NotImplementedException();
    }
  }
}
