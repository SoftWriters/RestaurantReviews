namespace RestaurantRating.Migrations
{
    using RestaurantRating.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestaurantRating.Models.RestaurantDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RestaurantRating.Models.RestaurantDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Restaurants.AddOrUpdate(t => new { t.RestaurantName, t.City }, DummyData.getrest().ToArray());
            context.SaveChanges();

            context.Users.AddOrUpdate(p => new { p.NameOfRest, p.UserName, p.Reviews }, DummyData.getuser().ToArray());

            context.SaveChanges();

        }
    }
}
