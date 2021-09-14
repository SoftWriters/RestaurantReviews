using Softwriters.RestaurantReviews.Dto;
using Softwriters.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softwriters.RestaurantReviews.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> GetAll();
        Task<Restaurant> GetById(int id);
        Task Update(int id, RestaurantRequest dto);
        Task Create(RestaurantRequest dto);
        Task Delete(int id);
    }
}