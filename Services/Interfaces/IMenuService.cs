using Softwriters.RestaurantReviews.Dto;
using Softwriters.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softwriters.RestaurantReviews.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAll();
        Task<Menu> GetById(int id);
        Task Update(int id, MenuRequest dto);
        Task Create(MenuRequest dto);
        Task Delete(int id);
    }
}