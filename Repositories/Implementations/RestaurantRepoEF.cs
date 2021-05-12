using DTOs;
using DTOs.Validators;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ORMLayer;
using ORMLayer.TableDefinitions;
using Repositories.Interfaces;
using Repositories.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
  public class RestaurantRepoEF : IRestaurantRepo
  {
    private readonly string _connectionString;
    private readonly ITranslator<RestaurantDTO,Restaurant> _translator;

    public RestaurantRepoEF(string connectionString)
    {
      _connectionString = connectionString;
      _translator = new RestaurantTranslator();
    }

    public async Task AddRestaruantAsync(RestaurantDTO newRestaurant)
    {
      using(var context = getContext())
      {
        try
        {
          context.Restaurants.Add(_translator.DtoToEntity(newRestaurant));
          await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
          //If exception was caused by Foriegn Key Constraints or Unique Constraints
            // throw validation exception that will be proccessed to an HTTPResponse with clear failure reason in Abstract Controller
          if (!await context.Cities.AnyAsync(c => c.Id == newRestaurant.CityId))
            throw new ValidationException(String.Format("City with id {0} does not exists", newRestaurant.CityId), ex);
          if (await context.Restaurants.AnyAsync(r => r.Name == newRestaurant.Name && r.Address == newRestaurant.Address))
            throw new ValidationException(String.Format("Restaurant with name, {0}, and address, {1}, already exists", newRestaurant.Name, newRestaurant.Address), ex);

          throw;
        }
      }
    }

    public async Task<IEnumerable<RestaurantDTO>> GetRestaurants(long cityId, string cuisine, bool sortByAvg)
    {
      using (var context = getContext())
      {
        if (sortByAvg)
        {
          // Join on Reviews -> group by Resturants -> order by average stars
          var selectQuery =
            @"SELECT Restaurants.Id, Restaurants.Name, Restaurants.Cuisine, Restaurants.CityId, Restaurants.Address, avg(Reviews.Stars)
            FROM Restaurants
            LEFT JOIN Reviews ON Restaurants.Id = Reviews.RestaurantId
            WHERE CityId = @cityId
            AND Cuisine like @cuisine
            GROUP BY Restaurants.Id, Restaurants.Name, Restaurants.Cuisine, Restaurants.CityId, Restaurants.Address
            ORDER BY avg(Reviews.Stars) desc";
          var result = context.Restaurants.FromSqlRaw(selectQuery, new SqlParameter("@cityId", cityId), new SqlParameter("@cuisine", string.Format("%{0}%", cuisine)));

          return (await result.ToListAsync()).Select(_translator.EntityToDto);
        }
        else
        {
          return (await context.Restaurants.Where(r => r.CityId == cityId && r.Cuisine.Contains(cuisine)).OrderBy(r => r.Name).ToListAsync()).Select(_translator.EntityToDto);
        }
      }
    }

    private DatabaseContext getContext() { return new DatabaseContext(_connectionString); }
  }
}
