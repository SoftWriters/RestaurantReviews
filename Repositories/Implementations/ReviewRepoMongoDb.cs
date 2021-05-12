using DTOs;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
  public class ReviewRepoMongoDb : IReviewRepo
  {

    private readonly string _connectionString;

    public ReviewRepoMongoDb(string connectionString)
    {
      _connectionString = connectionString;
    }

    public Task AddReview(ReviewDTO newReview)
    {
      throw new NotImplementedException();
    }

    public Task<bool> DeleteReview(long reviewId)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<ReviewDTO>> GetReviews(long? userId = null, long? restaurantId = null, bool sortLowToHigh = false)
    {
      throw new NotImplementedException();
    }
  }
}
