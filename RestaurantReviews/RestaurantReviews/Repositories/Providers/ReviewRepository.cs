using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReviews.Data;
using RestaurantReviews.Entities;

namespace RestaurantReviews.Repositories.Providers
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RestaurantReviewsContext context = new RestaurantReviewsContext();

        public async Task<int> Add(Review review)
        {
            review.DateCreated = DateTime.UtcNow;
            context.Reviews.Add(review);
            await context.SaveChangesAsync();

            return review.Id;
        }

        public async Task Delete(int reviewId)
        {
            var review = await context.Reviews.SingleOrDefaultAsync(r => r.Id == reviewId);

            if (review != null)
            {
                context.Reviews.Remove(review);
                context.SaveChanges();
            }
        }

        public async Task<Review> GetReview(int restaurantId)
        {
            return await context.Reviews.Where(r => r.Id == restaurantId).Include(r => r.Restaurant).Include(r => r.User).SingleOrDefaultAsync();
        }

        public void Dispose() => context.Dispose();
    }
}
