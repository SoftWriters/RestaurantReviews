using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.DataAccess
{
    public interface IRestaurantQuery
    {
        Task<List<Restaurant>> GetRestaurants(string city=null, string state=null);

        Task<Restaurant> GetRestaurant(long id);

        Task<Restaurant> GetRestaurant(string name, string city, string state);
    }
}