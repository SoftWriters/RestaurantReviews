using System;

namespace RestaurantReviews.Core
{
    /// <summary>
    /// Representation of a restaurant review
    /// </summary>
    public interface IRestaurantReview
    {
        Guid UniqueId { get; }

        /// <summary>
        /// Id of the restaurant the review applies to.
        /// Only referencing by Id because restaurants exist separately and are managed separately from reviews
        /// </summary>
        Guid RestaurantUniqueId { get; }

        /// <summary>
        /// User that wrote the review.
        /// Using the whole object here so that users can be implicitly added for new reviews. 
        /// This is not intended to be a user management database.
        /// </summary>
        IUser Reviewer { get; }

        /// <summary>
        /// Star rating for the review. Must be between 1 and 5.
        /// </summary>
        int FiveStarRating { get; }

        /// <summary>
        /// Written review
        /// </summary>
        string ReviewText { get; }

        /// <summary>
        /// Timestamp for the review
        /// </summary>
        DateTime Timestamp { get; }
    }
}
