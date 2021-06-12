using RestaurantReviews.Core.DataTypes;
using RestaurantReviews.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Controller
{
    /// <summary>
    /// Controller API for the restaurant reviews system. 
    /// Can be used by local apps, web api, UI, etc.
    /// </summary>
    public interface IRestaurantReviewController : IDisposable
    {
        #region Mutable

        /// <summary>
        /// Add a new restaurant to the database. Address is implicitly added if it does not already exist.
        /// </summary>
        /// <remarks>Use UpdateRestaurant to change the restaurant or address.</remarks>
        /// <param name="restaurant">Restaurant to add</param>
        /// <exception cref="ArgumentNullException">Restaurant is null</exception>
        /// <exception cref="DuplicateEntityException">Restaurant with the same unique Id already exists</exception>
        void AddRestaurant(IRestaurant restaurant);

        /// <summary>
        /// Delete a restaurant from the database along with all associated reviews.
        /// Address is also deleted IF no other restaurants share the address.
        /// </summary>
        /// <param name="restaurantId">Restaurant id to delete</param>
        /// <exception cref="ArgumentNullException">Restaurant is null</exception>
        /// <exception cref="EntityNotFoundException">Restaurant does not exists</exception>
        void DeleteRestaurant(Guid restaurantId);

        /// <summary>
        /// Adds a review for a restaurant. Reviewer is implicitly added if it does not already exist.
        /// </summary>
        /// <param name="review">Review to add</param>
        void AddReview(IRestaurantReview review);

        /// <summary>
        /// Deletes a review
        /// </summary>
        /// <param name="reviewId">Id of the review to delete</param>
        void DeleteReview(Guid reviewId);

        #endregion

        #region Queries

        /// <summary>
        /// Get a restaurant by Id
        /// </summary>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <returns>Restaurant or null if not found</returns>
        IRestaurant GetRestaurant(Guid restaurantId);

        /// <summary>
        /// Get a review by id
        /// </summary>
        /// <param name="reviewId">Review id</param>
        /// <returns>Review or null if not found</returns>
        IRestaurantReview GetReview(Guid reviewId);

        /// <summary>
        /// Find restaurants that match the query
        /// </summary>
        /// <param name="query">Query parameters with optional fields</param>
        /// <returns>Restaurants matching the query</returns>
        IReadOnlyList<IRestaurant> FindRestaurants(RestaurantsQuery query);

        /// <summary>
        /// Get reviews for a restaurant
        /// </summary>
        /// <param name="restaurantId">Restaurant id for which to get reviews</param>
        /// <returns>Reviews for the restaurant</returns>
        IReadOnlyList<IRestaurantReview> GetReviewsForRestaurant(Guid restaurantId);

        /// <summary>
        /// Get reviews for a user
        /// </summary>
        /// <param name="userId">User id for which to get reviews</param>
        /// <returns>Reviews for the reviewer</returns>
        IReadOnlyList<IRestaurantReview> GetReviewsForUser(Guid reviewer);

        #endregion
    }
}
