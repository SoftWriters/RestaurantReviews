namespace RestaurantReviews.Api.Models
{
    /// <summary>
    /// A new restaurant review.
    /// </summary>
    public class NewReview
    {
        /// <summary>
        /// The identifier for the restaurant this review is about.
        /// </summary>
        public long RestaurantId { get; set; }
        
        /// <summary>
        /// The email address of the user who provided this review.
        /// </summary>
        public string ReviewerEmail { get; set; }
        
        /// <summary>
        /// The number of stars the reviewer gave the restaurant.
        /// </summary>
        public decimal RatingStars  { get; set; }
        
        /// <summary>
        /// The reviewers comments about the restaurant.
        /// </summary>
        public string Comments { get; set; }
    }
}