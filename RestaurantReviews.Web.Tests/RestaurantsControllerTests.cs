using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.Web.Controllers;
using RestaurantReviews.Web.Data.Entities;
using RestaurantReviews.Web.Models;
using RestaurantReviews.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Tests
{
    [TestClass]
    public class RestaurantsControllerTests
    {
        RestaurantsController controller;
        Mock<ILogger<RestaurantsController>> mockLogger = new Mock<ILogger<RestaurantsController>>();
        Mock<IRestaurantRepository> mockRepository = new Mock<IRestaurantRepository>();

        public RestaurantsControllerTests()
        {
            controller = new RestaurantsController(mockLogger.Object, mockRepository.Object);
        }

        [TestMethod]
        public async Task GetRestaurantsAsync_Ok()
        {
            var restaurants = new List<Restaurant>();
            var city = "Pittsburgh";

            mockRepository.Setup(m => m.GetRestaurantsAsync(It.IsAny<String>())).ReturnsAsync(restaurants);

            var result = await controller.GetRestaurantsAsync(city);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = result as OkObjectResult;
            Assert.AreSame(restaurants, okObjectResult.Value);
            mockRepository.Verify(m => m.GetRestaurantsAsync(city), Times.Once);
        }

        [TestMethod]
        public async Task GetRestaurantsAsync_InternalServerError()
        {
            var restaurants = new List<Restaurant>();
            var city = "Pittsburgh";

            mockRepository.Setup(m => m.GetRestaurantsAsync(It.IsAny<String>())).ThrowsAsync(new Exception());

            var result = await controller.GetRestaurantsAsync(city);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            mockRepository.Verify(m => m.GetRestaurantsAsync(city), Times.Once);
        }

        [TestMethod]
        public async Task CreateRestaurantAsync_Ok()
        {
            var request = new RestaurantPostRequest();
            var restaurant = new Restaurant();

            mockRepository.Setup(m => m.CreateRestaurantAsync(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                .ReturnsAsync(restaurant);

            var result = await controller.CreateRestaurantAsync(request);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = result as OkObjectResult;
            Assert.AreSame(restaurant, okObjectResult.Value);
            mockRepository.Verify(m => m.CreateRestaurantAsync(request.Name, request.Address, request.City, request.State, request.ZipCode), Times.Once);
        }

        [TestMethod]
        public async Task CreateRestaurantAsync_InternalServerError()
        {
            var request = new RestaurantPostRequest();
            var restaurant = new Restaurant();

            mockRepository.Setup(m => m.CreateRestaurantAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var result = await controller.CreateRestaurantAsync(request);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            mockRepository.Verify(m => m.CreateRestaurantAsync(request.Name, request.Address, request.City, request.State, request.ZipCode), Times.Once);
        }
    }
}
