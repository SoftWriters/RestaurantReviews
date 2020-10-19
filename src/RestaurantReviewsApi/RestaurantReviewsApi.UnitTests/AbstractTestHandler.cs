using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RestaurantReviewsApi.Bll.Utility;
using RestaurantReviewsApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.UnitTests
{
    public abstract class AbstractTestHandler : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly RestaurantReviewsContext DbContext;

        protected AbstractTestHandler()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);

            _connection.CreateFunction("newid", () => Guid.NewGuid());
            _connection.CreateFunction("getutcdate", () => DateTime.UtcNow);

            _connection.Open();
            var options = new DbContextOptionsBuilder<RestaurantReviewsContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new RestaurantReviewsContext(options);
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public Guid AddRestaurant(
            string name = null,
            string city = null,
            string state = null,
            string zipCode = null,
            bool includeReviews = false,
            int reviewCount = 10)
        {
            var restaurant = new Restaurant()
            {
                Name = name ?? HelperFunctions.RandomString(20),
                City = city ?? HelperFunctions.RandomString(20),
                State = state ?? HelperFunctions.RandomElement<string>(ValidationHelper.ValidationConstants.StateAbbreviations),
                ZipCode = zipCode ?? "12345",
                AddressLine1 = HelperFunctions.RandomString(20),
                Description = HelperFunctions.RandomString(100),
                Email = HelperFunctions.RandomEmail(),
                Phone = HelperFunctions.RandomPhone(),
                AddressLine2 = HelperFunctions.RandomString(20),
                Website = HelperFunctions.RandomString(20)
            };

            DbContext.Add(restaurant);
            DbContext.SaveChanges();

            if (includeReviews)
            {
                for(int i = 0; i <= reviewCount; i++)
                {
                    var review = new Review()
                    {
                        UserName = $"User{i}",
                        Details = HelperFunctions.RandomString(200),
                        Rating = HelperFunctions.RandomNumber(10),
                        Restaurant = restaurant
                    };
                    DbContext.Add(review);
                }
                DbContext.SaveChanges();
            }
            return restaurant.RestaurantId;
        }

        public Guid AddReview(Guid restaurantId, string userId = "TestUser1")
        {
            var review = new Review()
            {
                UserName = userId,
                Details = HelperFunctions.RandomString(200),
                Rating = HelperFunctions.RandomNumber(10),
                RestaurantId = restaurantId
            };
            DbContext.Add(review);
            DbContext.SaveChanges();

            return review.ReviewId;
        }
    }
}
