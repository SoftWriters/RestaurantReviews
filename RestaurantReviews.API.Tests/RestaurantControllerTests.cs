using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.API.Controllers.CRUD;
using RestaurantReviews.API.Dtos;
using RestaurantReviews.API.Helpers;
using RestaurantReviews.API.Tests.Mocks;
using RestaurantReviews.Data.Contracts.Logging;
using RestaurantReviews.Data.Contracts.Repositories;
using RestaurantReviews.Data.Contracts.Repositories.Entities;
using RestaurantReviews.Data.DataSeeding;
using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.API.Tests
{
    [TestClass]
    public class RestaurantControllerTests
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
            _repositoryWrapper = Mock.Of<RepositoryWrapper>();
            _repositoryWrapper.Restaurant = Mock.Of<IRestaurantRepository>();
            _repositoryWrapper.Review = Mock.Of<IReviewRepository>();
        }

        [TestMethod]
        public void GetAllRestaurants()
        {
            // Arrange
            Mock.Get(_repositoryWrapper.Restaurant).Setup(x => x.GetAllRestaurants()).ReturnsAsync(DataSeeder.Restaurants);
            var controller = new RestaurantController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.GetAllRestaurants().Result;
            // Assert 
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var results = okObjectResult.Value as IEnumerable<Restaurant>;
            Assert.IsTrue(results.Count() == DataSeeder.Restaurants.Count());
        }

        [TestMethod]
        public void GetRestaurantById()
        {
            // Arrange
            var restaurant = DataSeeder.Restaurants.FirstOrDefault();
            Assert.IsNotNull(restaurant, string.Format("No restaurants were setup in the DataSeeder"));
            Mock.Get(_repositoryWrapper.Restaurant).Setup(x => x.GetRestaurantById(restaurant.Id)).ReturnsAsync(restaurant);
            var controller = new RestaurantController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.GetRestaurantById(restaurant.Id).Result;
            // Assert 
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var resultObject = okObjectResult.Value as Restaurant;
            Assert.IsTrue(resultObject.Id == restaurant.Id);
        }

        [TestMethod]
        public void CreateRestaurant()
        {
            // Arrange
            var restaurantDto = new RestaurantDto
            {
                Address = "1 Avely Rd.",
                City = "Girard",
                Country = "USA",
                EmailAddress = "NewRestaurant@email.com",
                Name = "New Restaurant",
                Phone = "(330) 454-4543",
                PostalCode = "44420",
                State = "OH",
                WebsiteUrl = "http://www.NewRestaurant.com"
            };
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);
            restaurant.IsConfirmed = true;
            restaurant.Id = Guid.NewGuid();
            Mock.Get(_repositoryWrapper.Restaurant).Setup(x => x.CreateRestaurant(restaurant));
            Mock.Get(_repositoryWrapper.Restaurant).Setup(x => x.GetRestaurantById(restaurant.Id)).ReturnsAsync(restaurant);
            var controller = new RestaurantController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.CreateRestaurant(restaurantDto).Result;
            // Assert 
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
        }

        [TestMethod]
        public void UpdateRestaurant()
        {
            // Arrange
            var restaurantDto = new RestaurantDto
            {
                Address = "1 Avely Rd.",
                City = "Girard",
                Country = "USA",
                EmailAddress = "NewRestaurant@email.com",
                Name = "New Restaurant",
                Phone = "(330) 454-4543",
                PostalCode = "44420",
                State = "OH",
                WebsiteUrl = "http://www.NewRestaurant.com"
            };
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);
            restaurant.Id = Guid.NewGuid();
            Mock.Get(_repositoryWrapper.Restaurant).Setup(x => x.UpdateRestaurant(restaurant, restaurant));
            Mock.Get(_repositoryWrapper.Restaurant).Setup(x => x.GetRestaurantById(restaurant.Id)).ReturnsAsync(restaurant);
            var controller = new RestaurantController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.UpdateRestaurant(restaurant.Id, restaurantDto).Result;
            // Assert 
            var noContentResult = actionResult as NoContentResult;
            Assert.IsNotNull(noContentResult);
        }

        // ToDo: Figure out why the mock setup for ReviewRepository.DeleteReview() is not being mocked correctly 
        //[TestMethod]
        public void DeleteRestaurant()
        {
            // Arrange
            var restaurant = DataSeeder.Restaurants[0];
            Mock.Get(_repositoryWrapper.Restaurant).Setup(x => x.DeleteRestaurant(restaurant));
            Mock.Get(_repositoryWrapper.Restaurant).Setup(x => x.GetRestaurantById(restaurant.Id)).ReturnsAsync(restaurant);
            Mock.Get(_repositoryWrapper.Review).Setup(x => x.GetReviewsByRestaurant(restaurant.Id)).ReturnsAsync(new List<Review>());
            var controller = new RestaurantController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.DeleteRestaurant(restaurant.Id).Result;
            // Assert 
            var noContentResult = actionResult as NoContentResult;
            Assert.IsNotNull(noContentResult);
        }
    }
}
