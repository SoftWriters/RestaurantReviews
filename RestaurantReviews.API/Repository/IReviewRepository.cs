using System.Collections.Generic;
using RestaurantReviews.API.Models;

namespace RestaurantReviews.API.Repository
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAll();
        Review GetById(long id);
        long Create(Review review);
        IList<Review> GetByUserId(int userId);
    }
}