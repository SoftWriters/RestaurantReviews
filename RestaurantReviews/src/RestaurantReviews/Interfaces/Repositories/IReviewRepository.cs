using System;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Repositories.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        IEnumerable<Review> GetReviewsByUser(UserRequest user);
        IEnumerable<Review> GetReviewsByRestaurant(RestaurantRequest restaurant);
        Review GetReview(long reviewId);
        void Add(ReviewRequest review);
        void Delete(long reviewId);
    }
}