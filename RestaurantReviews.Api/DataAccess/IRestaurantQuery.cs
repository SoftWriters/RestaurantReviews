using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.DataAccess
{
    public interface IRestaurantQuery
    {
        Task<List<Restaurant>> GetRestaurants(string city=null, string state=null);
    }
}