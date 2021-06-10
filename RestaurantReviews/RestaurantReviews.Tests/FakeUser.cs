using RestaurantReviews.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    internal class FakeUser : IUser
    {
        public Guid UniqueId { get; set; }

        public string DisplayName { get; set; }
    }
}
