
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
        Task<List<Restaurant>> ListRestaurantsByCityAsync(string city);
    }
}
