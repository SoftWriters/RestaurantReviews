using System.Threading.Tasks;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.DataAccess
{
    public interface IInsertReview
    {
        Task<long> Insert(NewReview review);
    }
}