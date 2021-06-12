using System;
using System.Collections.Generic;

namespace RestaurantReviews.Core
{
    /// <summary>
    /// Interface for querying the restaurant review database
    /// </summary>
    public interface IRestaurantReviewQueryDatabase
    {
        IReadOnlyList<IRestaurant> FindRestaurants(string name = null, string city = null, string StateOrProvince = null, string postalCode = null);

        IReadOnlyList<IRestaurantReview> GetReviewsForRestaurant(Guid restaurantId);

        IReadOnlyList<IRestaurantReview> GetReviewsForReviewer(Guid reviewerId);
    }
}
