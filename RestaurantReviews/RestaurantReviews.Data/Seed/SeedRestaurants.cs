using Platform.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Data.Seed
{
    public static class SeedRestaurants
    {
        public static Restaurant TacoBell { get; } = new Restaurant()
        {
            Id = Guid.Parse("17549f92-07e0-4c31-b954-d033e76a16d2"),
            Name = "Taco Bell",
            City = "Pittsburgh",
            State = "PA",
            ZipCode = "15237"
        };

        public static Restaurant Wendys { get; } = new Restaurant()
        {
            Id = Guid.Parse("176f2fdd-4c80-4da5-9fdb-0dcc8ac875fd"),
            Name = "Wendy's",
            City = "New York",
            State = "NY",
            ZipCode = "11201"
        };

        public static IEnumerable<Restaurant> All { get; } 
            = new LazyStaticProperties<Restaurant>(typeof(SeedRestaurants));
    }
}
