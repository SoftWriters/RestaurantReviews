using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities.Logic
{
    /// <summary>
    /// Exposes business logic for Review instances.
    /// </summary>
    public static class ReviewManager
    {
        /// <summary>
        /// Validates the Review instance for persistance.
        /// </summary>
        /// <param name="review">The Review to validate.</param>
        private static void ValidateReview(Review review)
        {
            if (string.IsNullOrWhiteSpace(review.Body))
                throw (new System.ArgumentException("Review body cannot be null or whitespace."));

            if (review.RestaurantId == -1)
                throw (new System.ArgumentException("Review restaurant id is invalid."));

            if (review.MemberId == -1)
                throw (new System.ArgumentException("Review member id is invalid."));
        }
        /// <summary>
        /// Persists a new instance of a Review.
        /// </summary>
        /// <param name="restaurantId">The ID of the Restaurant to create a Review for.</param>
        /// <param name="memberId">The ID of the Member who created the Review.</param>
        /// <param name="body">The textual content of the Review.</param>
        /// <returns>An instance of the persisted Review.</returns>
        public static Review CreateReview(long restaurantId, long memberId, string body)
        {
            Review review = new Review();
            review.RestaurantId = restaurantId;
            review.MemberId = memberId;
            review.Body = body;

            CreateReview(review);

            return review;
        }
        /// <summary>
        /// Persists a new Review instance.
        /// </summary>
        /// <param name="review">The Review to persist.</param>
        public static void CreateReview(Review review)
        {
            if (review.Id != -1)
                throw (new System.ArgumentException("Review is not a new instance."));

            ValidateReview(review);

            Data.ReviewSQL.CreateReview(review);
        }
        /// <summary>
        /// Updates a previously persisted Review instance.
        /// </summary>
        /// <param name="reviewId">The ID of the Review to update.</param>
        /// <param name="restaurantId">The ID of the Restaurant that the view pertains to.</param>
        /// <param name="memberId">The ID of the Member who created the Review.</param>
        /// <param name="body">The textual content of the Review.</param>
        /// <returns>An instance of the updated Review.</returns>
        public static Review UpdateReview(long reviewId, long restaurantId, long memberId, string body)
        {
            Review review = new Review();
            review.Id = reviewId;
            review.RestaurantId = restaurantId;
            review.MemberId = memberId;
            review.Body = body;

            UpdateReview(review);

            return review;
        }
        /// <summary>
        /// Updates a previously persisted Review instance.
        /// </summary>
        /// <param name="review">The review to Update.</param>
        public static void UpdateReview(Review review)
        {
            if (review.Id == -1)
                throw (new System.ArgumentException("Review is new instance and needs to be saved before updating."));

            ValidateReview(review);

            Data.ReviewSQL.UpdateReview(review);
        }
        /// <summary>
        /// Retrieves a previously persisted Review.
        /// </summary>
        /// <param name="reviewId">The ID of the Review to retrieve.</param>
        /// <returns>An instance of the Review.</returns>
        public static Review GetReview(long reviewId)
        {
            return Data.ReviewSQL.GetReview(reviewId);
        }
        /// <summary>
        /// Deletes a Review.
        /// </summary>
        /// <param name="reviewId">The ID of the Review to delete.</param>
        public static void DeleteReview(long reviewId)
        {
            Data.ReviewSQL.DeleteReview(reviewId);
        }
        /// <summary>
        /// Deletes a Review.
        /// </summary>
        /// <param name="review">The Review to delete.</param>
        public static void DeleteReview(Review review)
        {
            DeleteReview(review.Id);
        }
        /// <summary>
        /// Retrieves Review instances that are associated to a Restaurant.
        /// </summary>
        /// <param name="restaurant">The Restaurant to retrieve Review instances for.</param>
        /// <returns>A list of Review instances.</returns>
        public static List<Review> GetReviewsByRestaurant(Restaurant restaurant)
        {
            return GetReviewsByRestaurant(restaurant.Id);
        }
        /// <summary>
        /// Retrieves Review instances that are associated to a Restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the Restaurant to retrieve Review instances for.</param>
        /// <returns>A list of Review instances.</returns>
        public static List<Review> GetReviewsByRestaurant(long restaurantId)
        {
            return Data.ReviewSQL.GetReviewsByRestaurant(restaurantId);
        }
        /// <summary>
        /// Retrieves a collection of Review instances that were authored by a Member.
        /// </summary>
        /// <param name="member">The Member to retrieve authored Reviews.</param>
        /// <returns>A list of Review instances.</returns>
        public static List<Review> GetReviewsByMember(Member member)
        {
            return GetReviewsByMember(member.Id);
        }
        /// <summary>
        /// Retrieves a collection of Review instances that were authored by a Member.
        /// </summary>
        /// <param name="memberId">The ID of the Member to retrieve authored Reviews for.</param>
        /// <returns>A list of Review instances.</returns>
        public static List<Review> GetReviewsByMember(long memberId)
        {
            return Data.ReviewSQL.GetReviewsByMember(memberId);
        }
    }
}
