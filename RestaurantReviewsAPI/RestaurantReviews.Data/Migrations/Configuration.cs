using System.Collections.Generic;
using RestaurantReviews.Data.EfLibrary.Context;
using RestaurantReviews.Data.EfLibrary.Entities;

namespace RestaurantReviews.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestaurantReviewsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RestaurantReviewsContext context)
        {

        }
    }
}
