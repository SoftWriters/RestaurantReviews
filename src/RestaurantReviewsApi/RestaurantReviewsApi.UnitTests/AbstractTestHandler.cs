using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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

            _connection.Open();
            var options = new DbContextOptionsBuilder<RestaurantReviewsContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new RestaurantReviewsContext(options);
            DbContext.Database.EnsureCreated();
            Seed();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public virtual void Seed()
        {
            var rest1 = new Restaurant()
            {
                Name = "Fiori's Pizzaria",
                State = "PA",
                AddressLine1 = "103 Capital Ave",
                ZipCode = "15226",
                City = "Pittsburgh",
                Description = "The best Pizzaria around",
                Phone = "4121231234",
                Website = "www.fioris.com",
                Email = "fioris@gmail.com"
            };

            DbContext.Add(rest1);
            DbContext.SaveChanges();

            var review1 = new Review()
            {
                Restaurant = rest1,
                UserName = "TestUser1",
                Details = "The best pizza in pittsburgh",
                Rating = 10
            };

            var review2 = new Review()
            {
                Restaurant = rest1,
                UserName = "TestUser1",
                Details = "The okayest pizza in pittsburgh",
                Rating = 5
            };

            var review3 = new Review()
            {
                Restaurant = rest1,
                UserName = "TestUser2",
                Details = "Its pizza",
                Rating = 8
            };

            var review4 = new Review()
            {
                Restaurant = rest1,
                UserName = "TestUser1",
                Details = "Worst Pizza",
                Rating = 2,
                IsDeleted = true
            };

            DbContext.Add(review1);
            DbContext.Add(review2);
            DbContext.Add(review3);
            DbContext.Add(review4);
            DbContext.SaveChanges();
        }
    }
}
