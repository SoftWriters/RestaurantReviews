using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Web.Data;
using RestaurantReviews.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RestaurantReviewsContext _context;

        public ReviewRepository(RestaurantReviewsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a restaurant review.
        /// </summary>
        public async Task<Review> CreateReviewAsync(int restaurantId, int userId, string content)
        {
            var restaurant = await _context.Restaurants.SingleOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null)
            {
                throw new RestaurantNotFoundException(restaurantId);
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var review = new Review()
            {
                Content = content,
                RestaurantId = restaurantId,
                UserId = userId,
            };

            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();

            return review;
        }

        /// <summary>
        /// Get reviews by user ID.
        /// </summary>
        public async Task<IEnumerable<Review>> GetReviewsAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Reviews)
                .ThenInclude(r => r.Restaurant)
                .SingleOrDefaultAsync(u => u.Id == userId);

            return user?.Reviews.Where(r => r.Active == true);
        }

        /// <summary>
        /// Get reviews by username.
        /// </summary>
        public async Task<IEnumerable<Review>> GetReviewsAsync(string username)
        {
            var user = await _context.Users.Include(u => u.Reviews)
                .ThenInclude(r => r.Restaurant)
                .SingleOrDefaultAsync(u => u.Username == username);

            return user?.Reviews.Where(r => r.Active == true);
        }

        /// <summary>
        /// Delete a review
        /// </summary>
        public async Task<Review> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.SingleOrDefaultAsync(r => r.Id == reviewId);

            if (review == null)
            {
                return null;
            }
            else
            {
                review.Active = false;
            }

            await _context.SaveChangesAsync();

            return review;
        }
    }
}
