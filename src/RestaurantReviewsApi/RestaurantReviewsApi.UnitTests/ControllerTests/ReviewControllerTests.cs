using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Bll.Models;
using RestaurantReviewsApi.Bll.Providers;
using RestaurantReviewsApi.Controllers;
using RestaurantReviewsApi.UnitTests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReviewReviewsApi.UnitTests.ControllerTests
{
    public class ReviewControllerTests : AbstractTestHandler
    {
        [Fact]
        public async Task DeleteReturnsOk()
        {
            var mockManager = new Mock<IReviewManager>();
            mockManager.Setup(m => m.DeleteReviewAsync(It.IsAny<Guid>(), It.IsAny<UserModel>())).ReturnsAsync(true);
            var mockValidator = new Mock<IValidator<ReviewApiModel>>();
            var mockSearchValidator = new Mock<IValidator<ReviewSearchApiModel>>();
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.GetUserModel(It.IsAny<HttpRequest>())).Returns(new UserModel());
            var controller = new ReviewController(Logger<ReviewController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object, mockAuthProvider.Object);

            var result = await controller.DeleteReviewAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteReturnsNotFoundWhenNotFound()
        {
            var mockManager = new Mock<IReviewManager>();
            mockManager.Setup(m => m.DeleteReviewAsync(It.IsAny<Guid>(), It.IsAny<UserModel>())).ReturnsAsync(false);
            var mockValidator = new Mock<IValidator<ReviewApiModel>>();
            var mockSearchValidator = new Mock<IValidator<ReviewSearchApiModel>>();
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.GetUserModel(It.IsAny<HttpRequest>())).Returns(new UserModel());
            var controller = new ReviewController(Logger<ReviewController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object, mockAuthProvider.Object);

            var result = await controller.DeleteReviewAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetReturnsOkWhenFound()
        {
            var mockManager = new Mock<IReviewManager>();
            mockManager.Setup(m => m.GetReviewAsync(It.IsAny<Guid>())).ReturnsAsync(ApiModelHelperFunctions.RandomReviewApiModel(new Guid()));
            var mockValidator = new Mock<IValidator<ReviewApiModel>>();
            var mockSearchValidator = new Mock<IValidator<ReviewSearchApiModel>>();
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.GetUserModel(It.IsAny<HttpRequest>())).Returns(new UserModel());
            var controller = new ReviewController(Logger<ReviewController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object, mockAuthProvider.Object);

            var result = await controller.GetReviewAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetReturnsNotFoundWhenNotFound()
        {
            var mockManager = new Mock<IReviewManager>();
            mockManager.Setup(m => m.GetReviewAsync(It.IsAny<Guid>())).ReturnsAsync((ReviewApiModel)null);
            var mockValidator = new Mock<IValidator<ReviewApiModel>>();
            var mockSearchValidator = new Mock<IValidator<ReviewSearchApiModel>>();
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.GetUserModel(It.IsAny<HttpRequest>())).Returns(new UserModel());
            var controller = new ReviewController(Logger<ReviewController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object, mockAuthProvider.Object);

            var result = await controller.GetReviewAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostReturnsOkWhenSuccessful()
        {
            var mockManager = new Mock<IReviewManager>();
            mockManager.Setup(m => m.PostReviewAsync(It.IsAny<ReviewApiModel>(), It.IsAny<UserModel>())).ReturnsAsync(new Guid());
            var mockValidator = new Mock<IValidator<ReviewApiModel>>();
            mockValidator.Setup(m => m.Validate(It.IsAny<ReviewApiModel>())).Returns(new FluentValidation.Results.ValidationResult());
            var mockSearchValidator = new Mock<IValidator<ReviewSearchApiModel>>();
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.GetUserModel(It.IsAny<HttpRequest>())).Returns(new UserModel());
            var controller = new ReviewController(Logger<ReviewController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object, mockAuthProvider.Object);

            var result = await controller.PostReviewAsync(new ReviewApiModel());

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PostReturnsBadRequestWhenValidationFails()
        {
            var validationFail = new List<ValidationFailure>()
            {
                new ValidationFailure("default","default message")
            };

            var mockManager = new Mock<IReviewManager>();
            mockManager.Setup(m => m.PostReviewAsync(It.IsAny<ReviewApiModel>(), It.IsAny<UserModel>())).ReturnsAsync(new Guid());
            var mockValidator = new Mock<IValidator<ReviewApiModel>>();
            mockValidator.Setup(m => m.Validate(It.IsAny<ReviewApiModel>())).Returns(new FluentValidation.Results.ValidationResult(validationFail));
            var mockSearchValidator = new Mock<IValidator<ReviewSearchApiModel>>();
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.GetUserModel(It.IsAny<HttpRequest>())).Returns(new UserModel());

            var controller = new ReviewController(Logger<ReviewController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object, mockAuthProvider.Object);

            var result = await controller.PostReviewAsync(new ReviewApiModel());

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SearchReturnsBadRequestWhenValidationFails()
        {
            var validationFail = new List<ValidationFailure>()
            {
                new ValidationFailure("default","default message")
            };

            var mockManager = new Mock<IReviewManager>();
            mockManager.Setup(m => m.SearchReviewsAsync(It.IsAny<ReviewSearchApiModel>())).ReturnsAsync(new List<ReviewApiModel>());
            var mockValidator = new Mock<IValidator<ReviewApiModel>>();
            var mockSearchValidator = new Mock<IValidator<ReviewSearchApiModel>>();
            mockSearchValidator.Setup(m => m.Validate(It.IsAny<ReviewSearchApiModel>())).Returns(new FluentValidation.Results.ValidationResult(validationFail));
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.GetUserModel(It.IsAny<HttpRequest>())).Returns(new UserModel());

            var controller = new ReviewController(Logger<ReviewController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object, mockAuthProvider.Object);

            var result = await controller.SearchReviewsAsync(new ReviewSearchApiModel());

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SearchReturnsOkWhenSuccessful()
        {
            var mockManager = new Mock<IReviewManager>();
            mockManager.Setup(m => m.SearchReviewsAsync(It.IsAny<ReviewSearchApiModel>())).ReturnsAsync(new List<ReviewApiModel>());
            var mockValidator = new Mock<IValidator<ReviewApiModel>>();
            var mockSearchValidator = new Mock<IValidator<ReviewSearchApiModel>>();
            mockSearchValidator.Setup(m => m.Validate(It.IsAny<ReviewSearchApiModel>())).Returns(new FluentValidation.Results.ValidationResult());
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.GetUserModel(It.IsAny<HttpRequest>())).Returns(new UserModel());

            var controller = new ReviewController(Logger<ReviewController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object, mockAuthProvider.Object);

            var result = await controller.SearchReviewsAsync(new ReviewSearchApiModel());

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
