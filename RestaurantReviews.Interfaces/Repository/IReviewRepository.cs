using System.Collections.Generic;
using RestaurantReviews.Interfaces.Models;

namespace RestaurantReviews.Interfaces.Repository
{
    public interface IReviewRepository
    {
        ICollection<IReview> GetAll();
        IReview GetById(long id);
        long Create(IReview review);
        IList<IReview> GetByUserId(int userId);
    }
}