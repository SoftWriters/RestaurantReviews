using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
  public interface IReviewRepo
  {
    Task AddReview(ReviewDTO newReview);
    Task<bool> DeleteReview(long reviewId);
    Task<IEnumerable<ReviewDTO>> GetReviews(long? userId = null, long? restaurantId = null, bool sortLowToHigh = false);
  }
}
