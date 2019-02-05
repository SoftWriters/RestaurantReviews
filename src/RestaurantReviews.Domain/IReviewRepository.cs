using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain
{
    public interface IReviewRepository
    {
        Task<Review>  AddAsync(Review review);

        Task DeleteAsync(Guid reviewId);

        Task<Review> FindAsync(Guid reviewId);
        Task<IEnumerable<Review>> GetReviewsByUserAsync(Guid userId);

        Task<IEnumerable<User>> SearchUsersAsync(string searchString);

        Task<User> FindUserAsync(Guid userId);
    }
}
