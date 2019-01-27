using System.Threading.Tasks;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.DataAccess
{
    public interface IInsertRestaurant
    {
        Task<long> Insert(NewRestaurant restaurant);
    }
}