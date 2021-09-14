
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>> ListReviewsByUserAsync(long userId);
    }
}
