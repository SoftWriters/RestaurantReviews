using RestaurantReviews.Core.Interfaces;
using System;

namespace RestaurantReviews.Core.DataTypes
{
    /// <summary>
    /// Basic In-memory implementation of IRestaurantReview
    /// </summary>
    public class RestaurantReview : IRestaurantReview
    {
        public RestaurantReview()
            : this(Guid.NewGuid())
        {
            
        }

        public RestaurantReview(Guid uniqueId)
        {
            UniqueId = uniqueId;
        }

        public Guid UniqueId { get; }

        public Guid RestaurantUniqueId { get; set; }

        public IUser Reviewer { get; set; }

        public int FiveStarRating { get; set; }

        public string ReviewText { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
