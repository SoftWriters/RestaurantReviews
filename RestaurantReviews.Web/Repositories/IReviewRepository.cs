using RestaurantReviews.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> CreateReviewAsync(int restaurantId, int userId, string content);

        Task<IEnumerable<Review>> GetReviewsAsync(int userId);

        Task<IEnumerable<Review>> GetReviewsAsync(string username);

        Task<Review> DeleteReviewAsync(int reviewId);
    }
}
