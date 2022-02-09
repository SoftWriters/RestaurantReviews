using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.Web.Controllers;
using RestaurantReviews.Web.Data.Entities;
using RestaurantReviews.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Tests
{
    [TestClass]
    public class UserReviewsControllerTests
    {
        UserReviewsController controller;
        Mock<ILogger<UserReviewsController>> mockLogger = new Mock<ILogger<UserReviewsController>>();
        Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();

        public UserReviewsControllerTests()
        {
            controller = new UserReviewsController(mockLogger.Object, mockRepository.Object);
        }

        [TestMethod]
        public async Task GetReviewsByUserAsync_UserId_Ok()
        {
            var userId = 1;
            var reviews = new List<Review>();

            mockRepository.Setup(m => m.GetReviewsAsync(It.IsAny<int>())).ReturnsAsync(reviews);

            var result = await controller.GetReviewsByUserAsync(userId);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = result as OkObjectResult;
            Assert.AreSame(reviews, okObjectResult.Value);
            mockRepository.Verify(m => m.GetReviewsAsync(userId), Times.Once);
        }

        [TestMethod]
        public async Task GetReviewsByUserAsync_UserId_NotFound()
        {
            var userId = 1;

            mockRepository.Setup(m => m.GetReviewsAsync(It.IsAny<int>())).ReturnsAsync(null as List<Review>);

            var result = await controller.GetReviewsByUserAsync(userId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            mockRepository.Verify(m => m.GetReviewsAsync(userId), Times.Once);
        }

        [TestMethod]
        public async Task GetReviewsByUserAsync_UserId_InternalServerError()
        {
            var userId = 1;

            mockRepository.Setup(m => m.GetReviewsAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            var result = await controller.GetReviewsByUserAsync(userId);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            mockRepository.Verify(m => m.GetReviewsAsync(userId), Times.Once);
        }

        [TestMethod]
        public async Task GetReviewsByUserAsync_Username_Ok()
        {
            var username = "username";
            var reviews = new List<Review>();

            mockRepository.Setup(m => m.GetReviewsAsync(It.IsAny<string>())).ReturnsAsync(reviews);

            var result = await controller.GetReviewsByUserAsync(username);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = result as OkObjectResult;
            Assert.AreSame(reviews, okObjectResult.Value);
            mockRepository.Verify(m => m.GetReviewsAsync(username), Times.Once);
        }

        [TestMethod]
        public async Task GetReviewsByUserAsync_Username_NotFound()
        {
            var username = "username";

            mockRepository.Setup(m => m.GetReviewsAsync(It.IsAny<string>())).ReturnsAsync(null as List<Review>);

            var result = await controller.GetReviewsByUserAsync(username);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            mockRepository.Verify(m => m.GetReviewsAsync(username), Times.Once);
        }

        [TestMethod]
        public async Task GetReviewsByUserAsync_Username_InternalServerError()
        {
            var username = "username";

            mockRepository.Setup(m => m.GetReviewsAsync(It.IsAny<string>())).ThrowsAsync(new Exception());

            var result = await controller.GetReviewsByUserAsync(username);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            mockRepository.Verify(m => m.GetReviewsAsync(username), Times.Once);
        }
    }
}
