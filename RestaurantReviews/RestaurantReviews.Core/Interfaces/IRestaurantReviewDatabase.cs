using System;
using System.Collections.Generic;

namespace RestaurantReviews.Core.Interfaces
{
    /// <summary>
    /// Interface for the restaurant review database
    /// </summary>
    public interface IRestaurantReviewDatabase : IDisposable
    {
        void AddRestaurant(IRestaurant restaurant);

        void DeleteRestaurant(Guid restaurantId);

        void AddReview(IRestaurantReview review);

        void DeleteReview(Guid reviewId);

        IReadOnlyList<IRestaurant> FindRestaurants(string name = null, string city = null, string StateOrProvince = null, string postalCode = null);

        IReadOnlyList<IRestaurantReview> GetReviewsForRestaurant(Guid restaurantId);

        IReadOnlyList<IRestaurantReview> GetReviewsForReviewer(Guid reviewerId);
    }
}
