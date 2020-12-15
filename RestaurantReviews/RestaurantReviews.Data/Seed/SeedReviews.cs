using Platform.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Data.Seed
{
    public static class SeedReviews
    {
        public static Review HomerPositiveTacoBell { get; } = new Review()
        {
            Id = Guid.Parse("73336ba8-fa2e-411a-af19-f897b65f8586"),
            RestaurantId = SeedRestaurants.TacoBell.Id,
            UserId = SeedUsers.Homer.Id,
            ReviewText = "Mmmmmmm......Taco Bell!"
        };

        public static Review MargeNegativeTacoBell { get; } = new Review()
        {
            Id = Guid.Parse("480df267-022d-4329-a33b-8aace269ea6b"),
            RestaurantId = SeedRestaurants.TacoBell.Id,
            UserId = SeedUsers.Marge.Id,
            ReviewText = "I don't think Taco Bell is very healthy!"
        };

        public static Review LisaNegativeWendys { get; } = new Review()
        {
            Id = Guid.Parse("4a092744-52c6-4b11-bd83-37ae0f7e8d1a"),
            RestaurantId = SeedRestaurants.Wendys.Id,
            UserId = SeedUsers.Lisa.Id,
            ReviewText = "I'm not confident that they have an ethical supply chain of meat.  Think of the cows!"
        };

        public static IEnumerable<Review> All { get; }
            = new LazyStaticProperties<Review>(typeof(SeedReviews));
    }
}
