using RestaurantReviews.Data.QueryBuilder;
using RestaurantReviews.Data.Seed;
using RestaurantReviews.Logic.Model.Restaurant;
using RestaurantReviews.Logic.Model.Restaurant.Create;
using RestaurantReviews.Logic.Model.Restaurant.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantReviews.Test.Queries
{
    public class RestaurantQueryBuilderTests
    {
        [Fact]
        public void NullRequest_ReturnsAll()
        {
            var response = new RestaurantQueryBuilder()
                .BuildSearchQuery(SeedRestaurants.All.AsQueryable(), null)
                .ToList();
            Assert.Equal(SeedRestaurants.All.Count(), response.Count);
        }
        [Fact]
        public void DefaultRequest_ReturnsAll()
        {
            var response = new RestaurantQueryBuilder()
                .BuildSearchQuery(SeedRestaurants.All.AsQueryable(), new SearchRestaurantRequest())
                .ToList();
            Assert.Equal(SeedRestaurants.All.Count(), response.Count);
        }

        [Fact]
        public void State_Empty_ReturnsEmpty()
        {
            var request = new SearchRestaurantRequest()
            {
                State = string.Empty
            };
            var response = new RestaurantQueryBuilder()
                .BuildSearchQuery(SeedRestaurants.All.AsQueryable(), request)
                .ToList();
            Assert.Empty(response);
        }

        [Fact]
        public void State_Single()
        {
            var request = new SearchRestaurantRequest()
            {
                State = SeedRestaurants.Wendys.State
            };
            var response = new RestaurantQueryBuilder()
                .BuildSearchQuery(SeedRestaurants.All.AsQueryable(), request)
                .ToList();
            Assert.Equal(SeedRestaurants.All.Count(p => p.State == SeedRestaurants.Wendys.State), response.Count);
        }

        [Fact]
        public void State_Single_CaseInsensitive()
        {
            var request = new SearchRestaurantRequest()
            {
                State = SeedRestaurants.Wendys.State.ToLower()
            };
            var response = new RestaurantQueryBuilder()
                .BuildSearchQuery(SeedRestaurants.All.AsQueryable(), request)
                .ToList();
            Assert.Equal(SeedRestaurants.All.Count(p => p.State == SeedRestaurants.Wendys.State), response.Count);
        }

        [Fact]
        public void City_Single()
        {
            var expectedCity = SeedRestaurants.Wendys.City;
            var request = new SearchRestaurantRequest()
            {
                State = SeedRestaurants.Wendys.State,
                Cities = new[] { SeedRestaurants.Wendys.City }
            };
            var response = new RestaurantQueryBuilder()
                .BuildSearchQuery(SeedRestaurants.All.AsQueryable(), request)
                .ToList();
            Assert.Equal(SeedRestaurants.All.Count(p => p.City == expectedCity), response.Count);
        }

        [Fact]
        public void City_Two()
        {
            var expectedCities = new[]
            {
                SeedRestaurants.TacoBell.City,
                SeedRestaurants.WaWa.City
            };

            var request = new SearchRestaurantRequest()
            {
                Cities = expectedCities
            };
            var response = new RestaurantQueryBuilder()
                .BuildSearchQuery(SeedRestaurants.All.AsQueryable(), request)
                .ToList();
            Assert.Equal(SeedRestaurants.All.Count(p => expectedCities.Contains(p.City)), response.Count);
        }

        [Fact]
        public void Create_FindsExisting()
        {
            var r = SeedRestaurants.TacoBell;
            var request = new CreateRestaurantRequest()
            {
                Name = r.Name,
                City = r.City,
                State = r.State,
                Zip = r.ZipCode
            };
            var response = new RestaurantQueryBuilder()
                .BuildUpsertQuery(SeedRestaurants.All.AsQueryable(), request)
                .ToList();
            Assert.Single(response);
        }
    }
}
