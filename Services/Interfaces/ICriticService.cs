using Softwriters.RestaurantReviews.Dto;
using Softwriters.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softwriters.RestaurantReviews.Services.Interfaces
{
    public interface ICriticService
    {
        Task<IEnumerable<Critic>> GetAll();
        Task<Critic> GetById(int id);
        Task Update(int id, CriticRequest dto);
        Task Create(CriticRequest dto);
        Task Delete(int id);
    }
}