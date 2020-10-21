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
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Tests
{
    [TestClass]
    public class ReviewsControllerTests
    {
        ReviewsController controller;
        Mock<ILogger<ReviewsController>> mockLogger = new Mock<ILogger<ReviewsController>>();
        Mock<IReviewRepository> mockRepository = new Mock<IReviewRepository>();

        public ReviewsControllerTests()
        {
            controller = new ReviewsController(mockLogger.Object, mockRepository.Object);
        }

        [TestMethod]
        public async Task CreateReviewAsync_Ok()
        {
            var request = new ReviewPostRequest();
            var review = new Review();

            mockRepository.Setup(m => m.CreateReviewAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(review);

            var result = await controller.CreateReviewAsync(request);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = result as OkObjectResult;
            Assert.AreSame(review, okObjectResult.Value);
            mockRepository.Verify(m => m.CreateReviewAsync(request.RestaurantId, request.UserId, request.Content), Times.Once);
        }

        [TestMethod]
        public async Task CreateReviewAsync_BadRequest()
        {
            var request = new ReviewPostRequest();

            mockRepository.Setup(m => m.CreateReviewAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .ThrowsAsync(new UserNotFoundException());

            var result = await controller.CreateReviewAsync(request);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            mockRepository.Verify(m => m.CreateReviewAsync(request.RestaurantId, request.UserId, request.Content), Times.Once);
        }

        [TestMethod]
        public async Task CreateReviewAsync_InternalServerError()
        {
            var request = new ReviewPostRequest();

            mockRepository.Setup(m => m.CreateReviewAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var result = await controller.CreateReviewAsync(request);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            mockRepository.Verify(m => m.CreateReviewAsync(request.RestaurantId, request.UserId, request.Content), Times.Once);
        }

        [TestMethod]
        public async Task DeleteReviewAsync_Ok()
        {
            var reviewId = 1;
            var review = new Review();

            mockRepository.Setup(m => m.DeleteReviewAsync(It.IsAny<int>())).ReturnsAsync(review);

            var result = await controller.DeleteReviewAsync(reviewId);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            mockRepository.Verify(m => m.DeleteReviewAsync(reviewId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteReviewAsync_NotFound()
        {
            var reviewId = 1;

            mockRepository.Setup(m => m.DeleteReviewAsync(It.IsAny<int>())).ReturnsAsync(null as Review);

            var result = await controller.DeleteReviewAsync(reviewId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            mockRepository.Verify(m => m.DeleteReviewAsync(reviewId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteReviewAsync_InternalServerError()
        {
            var reviewId = 1;

            mockRepository.Setup(m => m.DeleteReviewAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            var result = await controller.DeleteReviewAsync(reviewId);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            mockRepository.Verify(m => m.DeleteReviewAsync(reviewId), Times.Once);
        }
    }
}

