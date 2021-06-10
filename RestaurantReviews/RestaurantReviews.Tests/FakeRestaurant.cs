using RestaurantReviews.Core;
using System;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    internal class FakeRestaurant : IRestaurant
    {
        public FakeRestaurant()
        {

        }

        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IAddress Address { get; set; }
    }
}
