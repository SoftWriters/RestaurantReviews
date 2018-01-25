using System.Collections.Generic;
using Abp.Domain.Repositories;

namespace RestaurantReviews
{
    public interface IReviewRepository : IRepository<Review, long>
    {
        List<Review> GetAllByUser(int? reviewerId);
    }
}
