using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;

namespace RestaurantReviews.EntityFramework
{
    public class ReviewRepository : RestaurantReviewsRepositoryBase<Review, long>, IReviewRepository
    {
        public List<Review> GetAllByUser(int? userId)
        {
            var query = Context.Reviews.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(review => review.ReviewerId == userId.Value);
            }

            return query
                .OrderByDescending(review => review.CreationTime)
                .ToList();
        }

        protected ReviewRepository(IDbContextProvider<RestaurantReviewsDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }
    }
}
