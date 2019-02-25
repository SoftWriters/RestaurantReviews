using RestaurantReviews.Data.Entities;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Data.DataSeeding
{
    public static class DataSeeder
    {
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                EmailAddress = "user1@email.com",
                FirstName = "1",
                LastName = "User1",
            },
            new User
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                EmailAddress = "user2@email.com",
                FirstName = "2",
                LastName = "User2",
            },
            new User
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                EmailAddress = "user3@email.com",
                FirstName = "3",
                LastName = "User3",
            }
        };

        public static List<Restaurant> Restaurants = new List<Restaurant>
        {
            new Restaurant
            {
                Id = Guid.NewGuid(),
                Address = "Park Drive",
                City = "Niles",
                Country = "USA",
                EmailAddress = "",
                Name = "Bombay Curry and Grill",
                Phone = "(330) 544-4444",
                PostalCode = "44446",
                State = "OH",
                WebsiteUrl = "https://www.doordash.com/store/bombay-curry-grill-niles-403108/",
                IsConfirmed = true
            },
            new Restaurant
            {
                Id = Guid.NewGuid(),
                Address = "5555 Youngstown Warren Rd #175",
                City = "Niles",
                Country = "USA",
                EmailAddress = "",
                Name = "Mizu Japanese Restaurant - Niles",
                Phone = "(330) 652-2888",
                PostalCode = "44446",
                State = "OH",
                WebsiteUrl = "http://www.mizu-oh.com/",
                IsConfirmed = true
            },
            new Restaurant
            {
                Id = Guid.NewGuid(),
                Address = "824 N State St",
                City = "Girard",
                Country = "USA",
                EmailAddress = "",
                Name = "The Daily Grind, Girard",
                Phone = "(234) 421-5118",
                PostalCode = "44420",
                State = "OH",
                WebsiteUrl = "http://www.thedailygrindgirard.com/",
                IsConfirmed = true
            }
        };

        public static List<Review> Reviews = new List<Review>()
        {
            new Review
            {
                Id = Guid.NewGuid(),
                Comment = "The food was excellent.",
                Rating = 4,
                RestaurauntId = Restaurants[0].Id, // Bombay Curry and Grill
                SubmissionDate = DateTime.UtcNow,
                UserId = Users[0].Id
            },
            new Review
            {
                Id = Guid.NewGuid(),
                Comment = "The food was good.",
                Rating = 3,
                RestaurauntId = Restaurants[1].Id, // Mizu Japanese Restaurant - Niles
                SubmissionDate = DateTime.UtcNow,
                UserId = Users[0].Id
            },
            new Review
            {
                Id = Guid.NewGuid(),
                Comment = "Great food and great service.",
                Rating = 5,
                RestaurauntId = Restaurants[2].Id, // The Daily Grind, Girard
                SubmissionDate = DateTime.UtcNow,
                UserId = Users[0].Id
            },
            new Review
            {
                Id = Guid.NewGuid(),
                Comment = "Excellent coffee.",
                Rating = 5,
                RestaurauntId = Restaurants[2].Id, // The Daily Grind, Girard
                SubmissionDate = DateTime.UtcNow,
                UserId = Users[1].Id
            }
        };
    }
}
