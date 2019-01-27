using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantReviews.Api.Controllers;
using RestaurantReviews.Api.DataAccess;
using RestaurantReviews.Api.Models;
using Xunit;

namespace RestaurantReviews.Api.UnitTests.ControllerTests
{
    public class ReviewControllerTests
    {
        [Fact]
        public async Task GetWithValidIdReturnsReview()
        {
            var mockReviewQuery = new Mock<IReviewQuery>();
            mockReviewQuery.Setup(q => q.GetReview(TestData.McDonaldsReview.Id))
                .Returns(Task.FromResult(TestData.McDonaldsReview));
            var controller = new ReviewController(null, mockReviewQuery.Object, null, null);

            var result = await controller.GetAsync(TestData.McDonaldsReview.Id);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<Review>(((OkObjectResult)result.Result).Value);
            var review = (Review)((OkObjectResult) result.Result).Value;
            Assert.Equal(TestData.McDonaldsReview.RatingStars, review.RatingStars);
        }
        
        [Fact]
        public async Task GetWithNonexistentIdReturnsNotFound()
        {
            var mockReviewQuery = new Mock<IReviewQuery>();
            mockReviewQuery.Setup(q => q.GetReview(TestData.McDonaldsReview.Id))
                .Returns(Task.FromResult(TestData.McDonaldsReview));
            var controller = new ReviewController(null, mockReviewQuery.Object, null, null);

            var result = await controller.GetAsync(TestData.WendysReview.Id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetWithInvalidIdReturnsBadRequest()
        {
            var controller = new ReviewController(null, null, null, null);

            var result = await controller.GetAsync(0);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        
        [Fact]
        public async Task GetListWithValidReviewerEmailReturnsReviews()
        {
            var mockReviewQuery = new Mock<IReviewQuery>();
            mockReviewQuery.Setup(q => q.GetReviews(TestData.McDonaldsReview.ReviewerEmail))
                .Returns(Task.FromResult(new List<Review> { TestData.McDonaldsReview }));
            var controller = new ReviewController(null, mockReviewQuery.Object, null, null);

            var result = await controller.GetListAsync(TestData.McDonaldsReview.ReviewerEmail);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<Review>>(((OkObjectResult)result.Result).Value);
            var resultList = (List<Review>)((OkObjectResult) result.Result).Value;
            Assert.Single(resultList);
        }
        
        [Fact]
        public async Task GetListWithReviewerEmailNoReviewsReturnsEmptyList()
        {
            var mockReviewQuery = new Mock<IReviewQuery>();
            mockReviewQuery.Setup(q => q.GetReviews(TestData.McDonaldsReview.ReviewerEmail))
                .Returns(Task.FromResult(new List<Review> { TestData.McDonaldsReview }));
            var controller = new ReviewController(null, mockReviewQuery.Object, null, null);

            var result = await controller.GetListAsync(TestData.WendysReview.ReviewerEmail);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Null(((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public async Task GetListWithInvalidReviewerEmailReturnsBadRequest()
        {
            var controller = new ReviewController(null, null, null, null);

            var result = await controller.GetListAsync(string.Empty);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostValidNewReviewReturnsCreated()
        {
            var mockValidator = new Mock<IReviewValidator>();
            mockValidator.Setup(v => v.IsReviewValid(It.IsAny<NewReview>())).Returns(true);
            var mockInsertReview = new Mock<IInsertReview>();
            var controller = new ReviewController(mockValidator.Object, null, mockInsertReview.Object, null);

            var result = await controller.PostAsync(TestData.WendysReview);
            
            Assert.IsType<CreatedResult>(result.Result);
            mockInsertReview.Verify(i => i.Insert(It.IsAny<NewReview>()),
                Times.Once);
        }
        
        [Fact]
        public async Task PostInvalidNewReviewReturnsCreated()
        {
            var mockValidator = new Mock<IReviewValidator>();
            mockValidator.Setup(v => v.IsReviewValid(It.IsAny<NewReview>())).Returns(false);
            var controller = new ReviewController(mockValidator.Object, null, null, null);

            var result = await controller.PostAsync(TestData.WendysReview);
            
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        
        [Fact]
        public async Task DeleteExistingReviewReturnsOk()
        {
            var mockDeleteReview = new Mock<IDeleteReview>();
            mockDeleteReview.Setup(d => d.Delete(123)).Returns(Task.FromResult(1));
            var controller = new ReviewController(null, null, null, mockDeleteReview.Object);

            var result = await controller.DeleteAsync(123);
            
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        [Fact]
        public async Task DeleteMissingReviewReturnsNotFound()
        {
            var mockDeleteReview = new Mock<IDeleteReview>();
            mockDeleteReview.Setup(d => d.Delete(123)).Returns(Task.FromResult(0));
            var controller = new ReviewController(null, null, null, mockDeleteReview.Object);

            var result = await controller.DeleteAsync(123);
            
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteInvalidIdReturnsBadRequest()
        {
            var controller = new ReviewController(null, null, null, null);

            var result = await controller.DeleteAsync(-1);
            
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}