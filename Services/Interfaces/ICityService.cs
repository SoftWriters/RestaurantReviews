using Softwriters.RestaurantReviews.Dto;
using Softwriters.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softwriters.RestaurantReviews.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAll();
        Task<City> GetById(int id);
        Task Update(int id, CityRequest dto);
        Task Create(CityRequest dto);
        Task Delete(int id);
    }
}