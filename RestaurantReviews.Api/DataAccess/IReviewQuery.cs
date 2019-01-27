using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.DataAccess
{
    public interface IReviewQuery
    {
        Task<Review> GetReview(long id);

        Task<List<Review>> GetReviews(string reviewerEmail);
    }
}