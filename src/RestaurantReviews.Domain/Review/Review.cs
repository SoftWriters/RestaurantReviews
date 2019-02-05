using System;

namespace RestaurantReviews.Domain
{
    /// <summary>
    /// Domain class
    /// </summary>
    public class Review
    {
        public Guid ReviewId;
        public Guid CreatedUserId;
        public Guid ResturantId;
        public string ReviewTitle;
        public string ReviewComment;
        public int Rating;
        public DateTime CreatedTime;

        public Review()
        {

        }
    }
}
