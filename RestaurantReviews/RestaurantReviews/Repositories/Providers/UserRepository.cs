using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReviews.Data;
using RestaurantReviews.Entities;

namespace RestaurantReviews.Repositories.Providers
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurantReviewsContext context = new RestaurantReviewsContext();

        public async Task<IEnumerable<Review>> GetUserReviews(int id)
        {
            return await context.Reviews
                .Where(r => r.UserId == id)
                            .Include(r => r.Restaurant)
                            .OrderByDescending(r => r.DateCreated)
                            .ToListAsync();
        }

        public void Dispose() => context.Dispose();
    }
}
