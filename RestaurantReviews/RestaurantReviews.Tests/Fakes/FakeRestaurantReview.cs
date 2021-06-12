using RestaurantReviews.Core;
using System;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    /// <summary>
    /// Basic In-memory implementation of IRestaurantReview for tests
    /// </summary>
    internal class FakeRestaurantReview : IRestaurantReview
    {
        public Guid UniqueId { get; set; }

        public Guid RestaurantUniqueId { get; set; }

        public IUser Reviewer { get; set; }

        public int FiveStarRating { get; set; }

        public string ReviewText { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
