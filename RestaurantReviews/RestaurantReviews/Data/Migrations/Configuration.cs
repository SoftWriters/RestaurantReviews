namespace RestaurantReviews.Data.Migrations
{
    using RestaurantReviews.Classes;
    using RestaurantReviews.Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RestaurantReviewsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Data\Migrations";
        }

        protected override void Seed(RestaurantReviewsContext context)
        {
            var resturants = new[]
            {
                new Restaurant{Name = "Restaurant A", PhoneNumber = "555-555-5555", Website = "www.google.com", RestaurantLocation = new RestaurantLocation{ Street = "Street", City = "Philadelphia", State = "PA", Country = "USA", ZipCode = "55555" } },
                new Restaurant{Name = "Restaurant B", PhoneNumber = "666-666-6666", Website = "www.apple.com", RestaurantLocation = new RestaurantLocation{ Street = "Street", City = "Abington", State = "PA", Country = "USA", ZipCode = "55555" } },
                new Restaurant{Name = "Restaurant C", Website = "www.microsoft.com", RestaurantLocation = new RestaurantLocation{ Street = "Street", City = "Dresher", State = "PA", Country = "USA", ZipCode = "55555" } },
                new Restaurant{Name = "Restaurant D", Website = "www.amazon.com", RestaurantLocation = new RestaurantLocation{ Street = "Street", City = "Ambler", State = "PA", Country = "USA", ZipCode = "55555" } },
            };

            context.Restaurants.AddRange(resturants);

            var users = new[]
            {
                new User{Username = "username1", Email = "address@gmail.com"},
                new User{Username = "username2", Email = "address1@gmail.com"},
                new User{Username = "username3", Email = "address2@gmail.com"},
            };

            context.Users.AddRange(users);

            var reviews = new[]
            {
                new Review{ Restaurant = resturants[0], User = users[0], ServiceGrade = 1, FoodGrade = 1, LookFeelGrade = 2, Text= "Bad Place", DateCreated = DateTime.UtcNow.AddDays(-10) },
                new Review{ Restaurant = resturants[1], User = users[1], ServiceGrade = 4, FoodGrade = 5, LookFeelGrade = 3, Text= "Nice Place", DateCreated = DateTime.UtcNow.AddDays(-15) },
                new Review{ Restaurant = resturants[2], User = users[0], ServiceGrade = 5, FoodGrade = 4, LookFeelGrade = 2, Text= "Not Recommened", DateCreated = DateTime.UtcNow.AddDays(-20) },
                new Review{ Restaurant = resturants[3], User = users[1], ServiceGrade = 3, FoodGrade = 3, LookFeelGrade = 1, Text= "Great Place for Families", DateCreated = DateTime.UtcNow.AddDays(-45) },
                new Review{ Restaurant = resturants[0], User = users[2], ServiceGrade = 2, FoodGrade = 5, LookFeelGrade = 2, Text= "Romantic Place", DateCreated = DateTime.UtcNow.AddDays(-50) },
                new Review{ Restaurant = resturants[0], User = users[1], ServiceGrade = 4, FoodGrade = 3, LookFeelGrade = 1, Text= "Great for lunches", DateCreated = DateTime.UtcNow.AddDays(-60) },
            };

            context.Reviews.AddRange(reviews);

            context.SaveChanges();

        }
    }
}
