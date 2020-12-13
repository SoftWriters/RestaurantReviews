using RestaurantReviews.Data.Seed;
using RestaurantReviews.Logic.Model.Review.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantReviews.Test
{
    public class ReviewQueryBuilderTests
    {
        [Fact]
        public void DefaultRequest_ReturnsAll()
        {
            var response = new ReviewQueryRequest().BuildQuery(SeedReviews.All.AsQueryable()).ToList();
            Assert.Equal(SeedReviews.All.Count(), response.Count);
        }

        [Fact]
        public void UserIds_Empty()
        {
            var response = new ReviewQueryRequest()
            {
                UserIds = Enumerable.Empty<string>()
            }.BuildQuery(SeedReviews.All.AsQueryable()).ToList();
            Assert.Empty(response);
        }

        [Fact]
        public void UserIds_Single()
        {
            var response = new ReviewQueryRequest()
            {
                UserIds = new[] {SeedUsers.Homer.Id.ToString()}
            }.BuildQuery(SeedReviews.All.AsQueryable()).ToList();
            Assert.Equal(SeedReviews.All.Count(p => p.UserId == SeedUsers.Homer.Id), response.Count);
        }

        [Fact]
        public void UserIds_Two()
        {
            var expectedUsers = new[]
            {
                SeedUsers.Homer.Id.ToString(),
                SeedUsers.Marge.Id.ToString()
            };
            var response = new ReviewQueryRequest()
            {
                UserIds = expectedUsers
            }.BuildQuery(SeedReviews.All.AsQueryable()).ToList();
            Assert.Equal(SeedReviews.All.Count(p => expectedUsers.Contains(p.UserId.ToString())), response.Count);
        }

    }
}
