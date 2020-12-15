using RestaurantReviews.Data.QueryBuilder;
using RestaurantReviews.Data.Seed;
using RestaurantReviews.Logic.Model.Review.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantReviews.Test.Queries
{
    public class ReviewQueryBuilderTests
    {
        [Fact]
        public void NullRequest_ReturnsAll()
        {
            var response = new ReviewQueryBuilder()
                .BuildSearchQuery(SeedReviews.All.AsQueryable(), null)
                .ToList();
            Assert.Equal(SeedReviews.All.Count(), response.Count);
        }

        [Fact]
        public void DefaultRequest_ReturnsAll()
        {
            var request = new SearchReviewRequest();
            var response = new ReviewQueryBuilder()
                .BuildSearchQuery(SeedReviews.All.AsQueryable(), request)
                .ToList();
            Assert.Equal(SeedReviews.All.Count(), response.Count);
        }

        [Fact]
        public void UserIds_Empty()
        {
            var request = new SearchReviewRequest()
            {
                UserIds = Enumerable.Empty<string>()
            };
            var response = new ReviewQueryBuilder()
                .BuildSearchQuery(SeedReviews.All.AsQueryable(), request)
                .ToList();
            Assert.Empty(response);
        }

        [Fact]
        public void UserIds_Single()
        {
            var request = new SearchReviewRequest()
            {
                UserIds = new[] { SeedUsers.Homer.Id.ToString() }
            };
            var response = new ReviewQueryBuilder()
                .BuildSearchQuery(SeedReviews.All.AsQueryable(), request)
                .ToList();
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
            var request = new SearchReviewRequest()
            {
                UserIds = expectedUsers
            };
            var response = new ReviewQueryBuilder()
                .BuildSearchQuery(SeedReviews.All.AsQueryable(), request)
                .ToList();
            Assert.Equal(SeedReviews.All.Count(p => expectedUsers.Contains(p.UserId.ToString())), response.Count);
        }

    }
}
