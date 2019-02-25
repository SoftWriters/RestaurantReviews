using RestaurantReviews.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Contracts.Repositories.Entities
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<IEnumerable<Review>> GetAllReviews();

        Task<IEnumerable<Review>> GetReviewsByCondition(Expression<Func<Review, bool>> expression);

        Task<IEnumerable<Review>> GetReviewsByRestaurant(Guid restaurantId);

        Task<IEnumerable<Review>> GetReviewsByUser(Guid userId);

        Task<Review> GetReviewById(Guid reviewId);

        Task<Review> GetReviewWithDetails(Guid reviewId);

        Task CreateReview(Review review);

        Task UpdateReview(Review dbReview, Review review);

        Task DeleteReview(Review review);
    }
}
