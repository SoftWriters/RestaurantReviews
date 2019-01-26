using System;

namespace RestaurantReviews.Api.Models
{
    /// <summary>
    /// A review of a restaurant.
    /// </summary>
    public class Review : NewReview
    {
        /// <summary>
        /// The internal identifier for this Review.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// When the review was filed.
        /// </summary>
        public DateTimeOffset ReviewedOn  { get; set; }

        public static Review FromNew(long newId, 
            DateTimeOffset reviewedOn, NewReview newReview)
        {
            return new Review
            {
                Id = newId,
                RestaurantId = newReview.RestaurantId,
                ReviewerEmail = newReview.ReviewerEmail,
                RatingStars = newReview.RatingStars,
                Comments = newReview.Comments,
                ReviewedOn = reviewedOn
            };
        }
    }
}