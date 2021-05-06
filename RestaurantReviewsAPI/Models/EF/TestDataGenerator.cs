using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace RestaurantReviewsAPI.Models
{
    public class TestDataGenerator
    {       
        public static void Seed(IApplicationBuilder applicationBuilder, UserManager<ApplicationUser> userManager)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<AppDbContext>();

                // Check if seeded
                if (db.Ratings.Any())
                {
                    return;   // Data has already been populated
                }

                ApplicationUser newUser = new ApplicationUser
                {
                    UserName = "TestUser",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = userManager.CreateAsync(newUser, "Password!123");


                db.Ratings.AddRange(
                    new Rating
                    {
                        Id = 1,
                        Value = 10,
                        Name = "Terrible"
                    },
                    new Rating
                    {
                        Id = 2,
                        Value = 20,
                        Name = "Poor"
                    },
                    new Rating
                    {
                        Id = 3,
                        Value = 30,
                        Name = "Average"
                    },
                    new Rating
                    {
                        Id = 4,
                        Value = 40,
                        Name = "Good"
                    },
                    new Rating
                    {
                        Id = 5,
                        Value = 50,
                        Name = "Great"
                    });

                db.MobileUsers.AddRange(
                    new MobileUser
                    {
                        Id = 1,
                        Name = "John Smith"
                    },
                    new MobileUser
                    {
                        Id = 2,
                         Name = "Jane Doe"
                    });

                db.Cities.AddRange(
                    new City
                    {
                        Name = "Pittsburgh",
                        State = "PA"
                    },
                    new City
                    {
                        Name = "Monroeville",
                        State = "PA"
                    },
                    new City
                    {
                        Name = "Sewickely",
                        State = "PA"
                    });

                db.Restaurants.Add(
                    new Restaurant
                    {
                        Name = "Do Drop Inn",
                        CityID = 1
                    });

                db.Reviews.Add(
                    new Review
                    {
                        MobileUserID = 1,
                        RestaurantID = 1,
                        RatingId = 4,
                        Comment = "Pretty good food and nice atmosphere."
                    });

                db.SaveChanges();
            }
        }
    }
}
