using RestaurantReviews.Data.Seed;
using RestaurantReviews.Logic.Model.Restaurant;
using RestaurantReviews.Logic.Model.Restaurant.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantReviews.Test
{
    public class RestaurantQueryBuilderTests
    {
        [Fact]
        public void DefaultRequest_ReturnsAll()
        {
            var response = new RestaurantQueryRequest().BuildQuery(SeedRestaurants.All.AsQueryable()).ToList();
            Assert.Equal(SeedRestaurants.All.Count(), response.Count);
        }

        [Fact]
        public void State_Empty()
        {
            var response = new RestaurantQueryRequest()
            {
                State = string.Empty
            }.BuildQuery(SeedRestaurants.All.AsQueryable()).ToList();
            Assert.Empty(response);
        }

        [Fact]
        public void State_Single()
        {
            var response = new RestaurantQueryRequest()
            {
                State = SeedRestaurants.Wendys.State
            }.BuildQuery(SeedRestaurants.All.AsQueryable()).ToList();
            Assert.Equal(SeedRestaurants.All.Count(p => p.State == SeedRestaurants.Wendys.State), response.Count);
        }

        [Fact]
        public void State_Single_CaseInsensitive()
        {
            var response = new RestaurantQueryRequest()
            {
                State = SeedRestaurants.Wendys.State.ToLower()
            }.BuildQuery(SeedRestaurants.All.AsQueryable()).ToList();
            Assert.Equal(SeedRestaurants.All.Count(p => p.State == SeedRestaurants.Wendys.State), response.Count);
        }

        [Fact]
        public void City_Single()
        {
            var expectedCity = SeedRestaurants.Wendys.City;
            var response = new RestaurantQueryRequest()
            {
                State = SeedRestaurants.Wendys.State,
                Cities = new[] { SeedRestaurants.Wendys.City }
            }.BuildQuery(SeedRestaurants.All.AsQueryable()).ToList();
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

            var response = new RestaurantQueryRequest()
            {
                Cities = expectedCities
            }.BuildQuery(SeedRestaurants.All.AsQueryable()).ToList();
            Assert.Equal(SeedRestaurants.All.Count(p => expectedCities.Contains(p.City)), response.Count);
        }

    }
}
