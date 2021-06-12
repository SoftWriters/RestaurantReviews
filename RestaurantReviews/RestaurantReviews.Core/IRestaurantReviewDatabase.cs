using System;
using System.Collections.Generic;

namespace RestaurantReviews.Core
{
    public interface IRestaurantReviewDatabase //TODO: Maybe split into read and write interfaces
    {
        #region Mutable
        
        /// <summary>
        /// Add a new restaurant to the database. 
        /// Address is implicitly added if it does not already exist, else ignored. 
        /// Use UpdateRestaurant to change the restaurant or address.
        /// </summary>
        /// <param name="restaurant">Restaurant to add</param>
        /// <exception cref="ArgumentNullException">Restaurant is null</exception>
        /// <exception cref="DuplicateEntityException">Restaurant with the same unique Id already exists</exception>
        void AddRestaurant(IRestaurant restaurant);

        /// <summary>
        /// Update an existing restaurant and address in the database.
        /// All the information is saved, so it must be complete to avoid losing data.
        /// </summary>
        /// <param name="restaurant">Restaurant to update</param>
        /// <exception cref="ArgumentNullException">Restaurant is null</exception>
        /// <exception cref="EntityNotFoundException">Restaurant does not exists</exception>
        void UpdateRestaurant(IRestaurant restaurant);

        /// <summary>
        /// Delete a restaurant from the database along with all associated reviews.
        /// Address is also deleted IF no other restaurants share the address.
        /// </summary>
        /// <param name="restaurantId">Restaurant id to delete</param>
        /// <exception cref="ArgumentNullException">Restaurant is null</exception>
        /// <exception cref="EntityNotFoundException">Restaurant does not exists</exception>
        void DeleteRestaurant(Guid restaurantId);

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
