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
            var seedUsers = new List<UserDBO>
            {
                new UserDBO
                {
                    Id = 1,
                    Username = "jtTestUser",
                    Password = "testing"
                }
            };

            seedUsers
                .ForEach(user => context.Users.AddOrUpdate(user));

            var states = new List<StateDBO>
            {
                new StateDBO
                {
                    Id = 1,
                    Code = "PA",
                    Name = "Pennsylvania"
                },
                new StateDBO
                {
                    Id = 2,
                    Code = "OH",
                    Name = "Ohio"
                }
            };

            states
                .ForEach(state => context.States.AddOrUpdate(state));
        }
    }
}
