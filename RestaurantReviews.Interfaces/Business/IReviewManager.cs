using RestaurantReviews.Interfaces.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Interfaces.Business
{
    public interface IReviewManager : IManager<IReview>
    {
        ICollection<IReview> GetByUserId(int userId);
        void Delete(long id);
    }
}
