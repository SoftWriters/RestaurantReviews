using Microsoft.AspNetCore.Mvc;
using RestaurantReview.Controllers;
using RestaurantReview.DAL;
using RestaurantReview.Models;
using RestaurantReview.Services;
using System.Linq;
using System.Net;
using Xunit;

namespace RestaurantReviewTests.ControllerTests
{
    public class ReviewsControllerTests
    {
        public IConn connection = new Conn();

        [Fact]
        public void GetTest_ReturnsValid()
        {
            //Arrange
            var RC = new ReviewsController(connection);

            //Act
            var result = RC.Get("user1");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetTest_RejectsInValid()
        {
            //Arrange
            var RC = new ReviewsController(connection);

            //Act
            var result = RC.Get("HocusPocus");

            //Assert
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void PostTest_SuccessReturnsOK()
        {
            var RC = new ReviewsController(connection);

            var review = new PostReview();
            review.UserId = 1;
            review.RestaurantId = 1;
            review.ReviewText = "Please submit this review. the restaurant is great";

            var result = RC.Post(review);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void PostTest_FailureReturnsObject()
        {
            var RC = new ReviewsController(connection);

            var review = new PostReview();
            review.RestaurantId = 1;
            review.ReviewText = "Please submit this review. the restaurant is great";

            var result = RC.Post(review);

            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void UpdateTest_SuccessReturnsOK()
        {
            var RC = new ReviewsController(connection);

            var review = new UpdateReview();
            review.ReviewId = 1;
            review.ReviewText = "Please update this review. the restaurant is great";

            var result = RC.Put(review);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void UpdateTest_FailureReturnsObject()
        {
            var RC = new ReviewsController(connection);

            var review = new PostReview();
            review.RestaurantId = 1;
            review.ReviewText = "";

            var result = RC.Post(review);

            Assert.IsType<ObjectResult>(result);
        }
        [Fact]
        public void DeleteTest_SuccessReturnsOK()
        {
            var RC = new ReviewsController(connection);
            var review = new ReviewsDAL(connection.AWSconnstring()).GetAllReviews().LastOrDefault();
            var result = RC.Delete(review.ReviewId);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteTest_FailureReturnsObject()
        {

            var RC = new ReviewsController(connection);
            var review = new ReviewsDAL(connection.AWSconnstring()).GetAllReviews().LastOrDefault();
            var result = RC.Delete(review.ReviewId + 100);
            Assert.IsType<StatusCodeResult>(result);
        }
    }
}