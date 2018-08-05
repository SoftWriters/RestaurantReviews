using RestaurantReviews.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Repositories
{
    public interface IReviewRepository : IDisposable
    {
        Task<int> Add(Review review);
        Task Delete(int id);
        Task<Review> GetReview(int id);
    }
}
