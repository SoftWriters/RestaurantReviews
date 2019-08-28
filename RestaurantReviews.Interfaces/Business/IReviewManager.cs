using RestaurantReviews.Interfaces.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Interfaces.Business
{
    public interface IReviewManager
    {
        ICollection<IReview> GetAll();
        IReview GetById(long id);
        ICollection<IReview> GetByUserId(int userId);
        void Create(IReview review);
    }
}
