using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using NLog;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Bll.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.ManagerTests
{
    public class RestaurantManagerTests : AbstractTestHandler
    {
        private IApiModelTranslator _translator => new ApiModelTranslator();

        [Fact]
        public async void CanDeleteRestaurant()
        {
            var guid = AddRestaurant();
            var manager = new RestaurantManager(Logger<RestaurantManager>(), DbContext, _translator);
            var delete = await manager.DeleteRestaurantAsync(guid);
            Assert.True(delete);

            var restaurant = DbContext.Restaurant.FirstOrDefault(x => x.RestaurantId == guid);
            Assert.True(restaurant.IsDeleted);
        }

        [Fact]
        public async void CanGetRestaurant()
        {
            var guid = AddRestaurant();
            var manager = new RestaurantManager(Logger<RestaurantManager>(), DbContext, _translator);
            var restaurant = await manager.GetRestaurantAsync(guid);
            Assert.NotNull(restaurant);
        }

        [Fact]
        public async void CanPatchRestaurant()
        {
            var model = ApiModelHelperFunctions.RandomRestaurantApiModel();
            var guid = AddRestaurant();
            model.RestaurantId = guid;
            var manager = new RestaurantManager(Logger<RestaurantManager>(), DbContext, _translator);
            var patch = await manager.PatchRestaurantAsync(model);
            Assert.NotNull(patch);
        }

        [Fact]
        public async void CanPostRestaurant()
        {
            var model = ApiModelHelperFunctions.RandomRestaurantApiModel();
            var guid = AddRestaurant();
            model.RestaurantId = guid;
            var manager = new RestaurantManager(Logger<RestaurantManager>(), DbContext, _translator);
            var post = await manager.PostRestaurantAsync(model);
            Assert.NotNull(post);
        }

        [Fact]
        public async void CanSearchRestaurantByName()
        {
            var guid = AddRestaurant();
            var name = DbContext.Restaurant.FirstOrDefault(x => x.RestaurantId == guid).Name;

            RestaurantSearchApiModel searchModel = new RestaurantSearchApiModel()
            {
                Name = name
            };

            var manager = new RestaurantManager(Logger<RestaurantManager>(), DbContext, _translator);
            var resultSet = await manager.SearchRestaurantsAsync(searchModel);
            var result = resultSet.First();

            Assert.Equal(1, resultSet.Count);
            Assert.Equal(guid, result.RestaurantId);
            Assert.Equal(name, result.Name);
        }


        [Fact]
        public async void CanSearchRestaurantByPartialName()
        {
            var guid = AddRestaurant();
            var name = DbContext.Restaurant.FirstOrDefault(x => x.RestaurantId == guid).Name;

            RestaurantSearchApiModel searchModel = new RestaurantSearchApiModel()
            {
                Name = name.Substring(0, name.Length - 5)
            };

            var manager = new RestaurantManager(Logger<RestaurantManager>(), DbContext, _translator);
            var resultSet = await manager.SearchRestaurantsAsync(searchModel);
            var result = resultSet.First();

            Assert.Equal(1, resultSet.Count);
            Assert.Equal(guid, result.RestaurantId);
            Assert.Equal(name, result.Name);
        }

        [Fact]
        public async void CanSearchRestaurantByCity()
        {
            var guid = AddRestaurant();
            var city = DbContext.Restaurant.FirstOrDefault(x => x.RestaurantId == guid).City;

            RestaurantSearchApiModel searchModel = new RestaurantSearchApiModel()
            {
                City = city
            };

            var manager = new RestaurantManager(Logger<RestaurantManager>(), DbContext, _translator);
            var resultSet = await manager.SearchRestaurantsAsync(searchModel);
            var result = resultSet.First();

            Assert.Equal(1, resultSet.Count);
            Assert.Equal(guid, result.RestaurantId);
            Assert.Equal(city, result.City);
        }

        [Fact]
        public async void CanSearchRestaurantByState()
        {
            var guid = AddRestaurant();
            var state = DbContext.Restaurant.FirstOrDefault(x => x.RestaurantId == guid).State;

            RestaurantSearchApiModel searchModel = new RestaurantSearchApiModel()
            {
                State = state
            };

            var manager = new RestaurantManager(Logger<RestaurantManager>(), DbContext, _translator);
            var resultSet = await manager.SearchRestaurantsAsync(searchModel);
            var result = resultSet.FirstOrDefault(x => x.RestaurantId == guid);

            Assert.NotNull(result);
            Assert.Equal(state, result.State);
        }

        [Fact]
        public async void CanSearchRestaurantByZipCode()
        {
            var guid = AddRestaurant();
            string zipCode = DbContext.Restaurant.FirstOrDefault(x => x.RestaurantId == guid).ZipCode;

            RestaurantSearchApiModel searchModel = new RestaurantSearchApiModel()
            {
                ZipCode = zipCode
            };

            var manager = new RestaurantManager(Logger<RestaurantManager>(), DbContext, _translator);
            var resultSet = await manager.SearchRestaurantsAsync(searchModel);
            var result = resultSet.FirstOrDefault(x => x.RestaurantId == guid);

            Assert.Equal(guid, result.RestaurantId);
            Assert.Equal(zipCode, result.ZipCode);
        }
    }
}
