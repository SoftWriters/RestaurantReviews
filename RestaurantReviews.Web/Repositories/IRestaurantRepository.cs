using RestaurantReviews.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string city = null);

        Task<Restaurant> CreateRestaurantAsync(string name, string address, string city, string state, string zipCode);
    }
}
