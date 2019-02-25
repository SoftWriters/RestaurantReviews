using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.API.Controllers.PublicServices;
using RestaurantReviews.API.Dtos;
using RestaurantReviews.API.Helpers;
using RestaurantReviews.API.Tests.Mocks;
using RestaurantReviews.Data.Contracts.Logging;
using RestaurantReviews.Data.Contracts.Repositories;
using RestaurantReviews.Data.Contracts.Repositories.Entities;
using RestaurantReviews.Data.DataSeeding;
using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.Repositories;
using RestaurantReviews.Data.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.API.Tests
{
    [TestClass]
    public class RestaurantReviewsControllerTests
    {
        #region Private Variables

        private ILoggerManager _loggerManager = new MockLoggerManager();
        private IMapper _mapper;
        private MapperConfiguration _mapperConfiguration;
        private IRepositoryWrapper _repositoryWrapper;

        #endregion Private Variables

        [TestInitialize]
        public void TestInitialization()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); });
            _mapper = _mapperConfiguration.CreateMapper();
            _mapper = new Mapper(_mapperConfiguration);
            _repositoryWrapper = Mock.Of<IRepositoryWrapper>();
            _repositoryWrapper.Restaurant = Mock.Of<IRestaurantRepository>();
            _repositoryWrapper.Review = Mock.Of<IReviewRepository>();
        }

        [TestMethod]
        public void GetReviewsByUser()
        {
            // Arrange
            var user = DataSeeder.Users.FirstOrDefault();
            Assert.IsNotNull(user, string.Format("No users were setup in the DataSeeder"));
            var seededReviews = DataSeeder.Reviews.Where(r => r.UserId == user.Id);
            Mock.Get(_repositoryWrapper.Review).Setup(x => (x.GetReviewsByUser(user.Id))).ReturnsAsync(seededReviews);
            var controller = new RestaurantReviewsController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.GetReviewsByUser(user.Id).Result;
            // Assert 
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var reviews = okObjectResult.Value as IEnumerable<Review>;
            Assert.IsTrue(reviews.Count() == seededReviews.Count());
        }

        [TestMethod]
        public void GetRestaurantsByCity()
        {
            // Arrange
            var restaurantsByCityDto = new RestaurantsByCityDto
            {
                City = "Niles",
                Country = "USA",
                State = "OH"
            };
            var seededRestaurants = DataSeeder.Restaurants.Where(r =>
                r.City == restaurantsByCityDto.City
                && r.Country == restaurantsByCityDto.Country
                && r.State == restaurantsByCityDto.State
            );
            Mock.Get(_repositoryWrapper.Restaurant).Setup(x => (x.GetRestaurantsByCity(restaurantsByCityDto.City, restaurantsByCityDto.State, restaurantsByCityDto.Country))).ReturnsAsync(seededRestaurants);
            var controller = new RestaurantReviewsController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.GetRestaurantsByCity(restaurantsByCityDto).Result;
            // Assert 
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var restaurants = okObjectResult.Value as IEnumerable<Restaurant>;
            Assert.IsTrue(restaurants.Count() == seededRestaurants.Count());
        }

        [TestMethod]
        public void PostANewRestaurant()
        {
        }

        [TestMethod]
        public void PostAReview()
        {
        }

        [TestMethod]
        public void DeleteAReview()
        {
        }
    }
}
