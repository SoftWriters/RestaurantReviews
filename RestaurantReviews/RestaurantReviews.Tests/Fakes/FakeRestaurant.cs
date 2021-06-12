using RestaurantReviews.Core;
using System;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    /// <summary>
    /// Basic In-memory implementation of IRestaurant for tests
    /// </summary>
    internal class FakeRestaurant : IRestaurant
    {
        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IAddress Address { get; set; }
    }
}
