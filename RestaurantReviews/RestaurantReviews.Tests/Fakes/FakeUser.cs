using RestaurantReviews.Core;
using System;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    /// <summary>
    /// Basic In-memory implementation of IUser for tests
    /// </summary>
    internal class FakeUser : IUser
    {
        public Guid UniqueId { get; set; }

        public string DisplayName { get; set; }
    }
}
