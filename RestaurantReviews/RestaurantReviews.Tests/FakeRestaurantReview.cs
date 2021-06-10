using RestaurantReviews.Core;
using System;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    internal class FakeRestaurantReview : IRestaurantReview
    {
        public IRestaurant Restaurant { get; set; }

        public Guid UniqueId { get; set; }

        public IUser Reviewer { get; set; }

        public int FiveStarRating { get; set; }

        public string Title { get; set; }

        public string ReviewText { get; set; }

        public DateTime Date { get; set; }
    }
}
