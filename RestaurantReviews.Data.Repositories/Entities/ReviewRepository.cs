using RestaurantReviews.Data.Contracts.Repositories.Entities;
using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Repositories.Entities
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository() { }

        public ReviewRepository(RestaurantReviewsContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return (await FindAll()).OrderBy(review => review.SubmissionDate);
        }
        
        public async Task<IEnumerable<Review>> GetReviewsByCondition(Expression<Func<Review, bool>> expression)
        {
            return this.RepositoryContext.Set<Review>().Where(expression);
        }

        public async Task<IEnumerable<Review>> GetReviewsByRestaurant(Guid restaurantId)
        {
            return await GetReviewsByCondition(review => review.RestaurauntId == restaurantId);
        }

        public async Task<IEnumerable<Review>> GetReviewsByUser(Guid userId)
        {
            return await GetReviewsByCondition(review => review.UserId == userId);
        }

        public async Task<Review> GetReviewById(Guid reviewId)
        {
            return (await FindByCondition(user => user.Id.Equals(reviewId)))
                    .DefaultIfEmpty(new Review())
                    .FirstOrDefault();
        }

        public async Task<Review> GetReviewWithDetails(Guid reviewId)
        {
            return await GetReviewById(reviewId);
        }

        public async Task CreateReview(Review review)
        {
            review.Id = Guid.NewGuid();
            await Create(review);
            await Save();
        }

        public async Task UpdateReview(Review dbReview, Review review)
        {
            dbReview.Map(review);
            await Update(dbReview);
            await Save();
        }

        public async Task DeleteReview(Review review)
        {
            await Delete(review);
            await Save();
        }
    }
}
