using System.Collections.Generic;
using RestaurantReviews.Interfaces.Models;

namespace RestaurantReviews.Interfaces.Repositories
{
    public interface IReviewRepository : IRepository<IReview>
    {
        ICollection<IReview> GetByUserId(int userId);
        void Delete(long id);
    }
}