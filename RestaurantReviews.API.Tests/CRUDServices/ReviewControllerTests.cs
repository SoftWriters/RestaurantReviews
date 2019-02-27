using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.API.Controllers.CRUD;
using RestaurantReviews.API.Dtos;
using RestaurantReviews.API.Tests.Base;
using RestaurantReviews.Data.DataSeeding;
using RestaurantReviews.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.API.Tests.CRUDServices
{
    [TestClass]
    public class ReviewControllerTests : BaseControllerUnitTests
    {
        [TestMethod]
        public void GetAllReviews()
        {
            // Arrange
            Mock.Get(_repositoryWrapper.Review).Setup(x => x.GetAllReviews()).ReturnsAsync(DataSeeder.Reviews);
            var controller = new ReviewController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.GetAllReviews().Result;
            // Assert 
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var results = okObjectResult.Value as IEnumerable<Review>;
            Assert.IsTrue(results.Count() == DataSeeder.Reviews.Count());
        }

        [TestMethod]
        public void GetReviewById()
        {
            // Arrange
            var review = DataSeeder.Reviews.FirstOrDefault();
            Assert.IsNotNull(review, string.Format("No reviews were setup in the DataSeeder"));
            Mock.Get(_repositoryWrapper.Review).Setup(x => x.GetReviewById(review.Id)).ReturnsAsync(review);
            var controller = new ReviewController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.GetReviewById(review.Id).Result;
            // Assert 
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var resultObject = okObjectResult.Value as Review;
            Assert.IsTrue(resultObject.Id == review.Id);
        }

        [TestMethod]
        public void CreateReview()
        {
            // Arrange
            var reviewDto = new ReviewDto
            {
                Comment = "Comment 1",
                Rating = 4,
                RestaurantId = DataSeeder.Restaurants[0].Id,
                UserId = DataSeeder.Users[0].Id
            };
            var review = _mapper.Map<Review>(reviewDto);
            Mock.Get(_repositoryWrapper.Review).Setup(x => x.CreateReview(review));
            Mock.Get(_repositoryWrapper.Review).Setup(x => x.GetReviewById(review.Id)).ReturnsAsync(review);
            var controller = new ReviewController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.CreateReview(reviewDto).Result;
            // Assert 
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
        }

        [TestMethod]
        public void UpdateReview()
        {
            // Arrange
            var reviewDto = new ReviewDto
            {
                Comment = "Comment 1",
                Rating = 4,
                RestaurantId = DataSeeder.Restaurants[0].Id,
                UserId = DataSeeder.Users[0].Id
            };
            var review = _mapper.Map<Review>(reviewDto);
            Mock.Get(_repositoryWrapper.Review).Setup(x => x.UpdateReview(review, review));
            Mock.Get(_repositoryWrapper.Review).Setup(x => x.GetReviewById(review.Id)).ReturnsAsync(review);
            var controller = new ReviewController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.UpdateReview(review.Id, reviewDto).Result;
            // Assert 
            var noContentResult = actionResult as NoContentResult;
            Assert.IsNotNull(noContentResult);
        }

        // ToDo: Figure out why the mock setup for ReviewRepository.DeleteReview() is not being mocked correctly 
        [TestMethod]
        public void DeleteReview()
        {
            // Arrange
            var review = DataSeeder.Reviews[0];
            Mock.Get(_repositoryWrapper.Review).Setup(x => x.DeleteReview(review));
            Mock.Get(_repositoryWrapper.Review).Setup(x => x.GetReviewById(review.Id)).ReturnsAsync(review);
            var controller = new ReviewController(_loggerManager, _mapper, _repositoryWrapper);
            // Act
            var actionResult = controller.DeleteReview(review.Id).Result;
            // Assert 
            var noContentResult = actionResult as NoContentResult;
            Assert.IsNotNull(noContentResult);
        }
    }
}
