using System;

namespace RestaurantReviews.Core
{
    /// <summary>
    /// Interface for modifying the restaurant review database
    /// </summary>
    public interface IRestaurantReviewMutableDatabase
    {
        void AddRestaurant(IRestaurant restaurant);

        void DeleteRestaurant(Guid restaurantId);

        void AddReview(IRestaurantReview review);

        bool DeleteReview(Guid reviewId);
    }
}
