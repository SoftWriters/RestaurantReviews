using System;
using System.Collections.Generic;

namespace RestaurantReviews.Core
{
    public interface IRestaurantReviewDatabase
    {
        #region Mutable

        void AddRestaurant(IRestaurant restaurant);

        bool UpdateRestaurant(IRestaurant restaurant);

        bool DeleteRestaurant(Guid restaurantId);

        void AddReview(IRestaurantReview review);

        bool DeleteReview(Guid reviewId);

        void AddUser(IUser user);

        bool DeleteUser(Guid userId);

        #endregion

        #region Queries

        IReadOnlyList<IRestaurant> FindRestaurants(string name = null, string city = null, string stateAbbreviation = null, string zipCode = null);

        IReadOnlyList<IRestaurantReview> FindReviews(IRestaurant restaurant);

        IReadOnlyList<IRestaurantReview> FindReviewsByReviewer(IUser reviewer);

        #endregion
    }
}
