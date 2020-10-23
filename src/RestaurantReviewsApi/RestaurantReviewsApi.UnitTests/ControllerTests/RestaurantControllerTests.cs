using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.ControllerTests
{
    public class RestaurantControllerTests : AbstractTestHandler
    {
        [Fact]
        public async Task DeleteReturnsOk()
        {
            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.DeleteRestaurantAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.DeleteRestaurantAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteReturnsNotFoundWhenNotFound()
        {
            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.DeleteRestaurantAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.DeleteRestaurantAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetReturnsOkWhenFound()
        {
            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.GetRestaurantAsync(It.IsAny<Guid>())).ReturnsAsync(ApiModelHelperFunctions.RandomRestaurantApiModel());
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.GetRestaurantAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetReturnsNotFoundWhenNotFound()
        {
            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.GetRestaurantAsync(It.IsAny<Guid>())).ReturnsAsync((RestaurantApiModel)null);
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.GetRestaurantAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostReturnsOkWhenSuccessful()
        {
            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.PostRestaurantAsync(It.IsAny<RestaurantApiModel>())).ReturnsAsync(new Guid());
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            mockValidator.Setup(m => m.Validate(It.IsAny<RestaurantApiModel>())).Returns(new FluentValidation.Results.ValidationResult());
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.PostRestaurantAsync(new RestaurantApiModel());

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

            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.PostRestaurantAsync(It.IsAny<RestaurantApiModel>())).ReturnsAsync(new Guid());
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            mockValidator.Setup(m => m.Validate(It.IsAny<RestaurantApiModel>())).Returns(new FluentValidation.Results.ValidationResult(validationFail));
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.PostRestaurantAsync(new RestaurantApiModel());

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PatchReturnsBadRequestWhenValidationFails()
        {
            var validationFail = new List<ValidationFailure>()
            {
                new ValidationFailure("default","default message")
            };

            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.PatchRestaurantAsync(It.IsAny<RestaurantApiModel>())).ReturnsAsync(new Guid());
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            mockValidator.Setup(m => m.Validate(It.IsAny<RestaurantApiModel>())).Returns(new FluentValidation.Results.ValidationResult(validationFail));
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.PatchRestaurantAsync(new RestaurantApiModel());

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PatchReturnsOkWhenSuccessful()
        {
            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.PatchRestaurantAsync(It.IsAny<RestaurantApiModel>())).ReturnsAsync(new Guid());
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            mockValidator.Setup(m => m.Validate(It.IsAny<RestaurantApiModel>())).Returns(new FluentValidation.Results.ValidationResult());
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.PatchRestaurantAsync(new RestaurantApiModel());

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PatchReturnsNotFoundWhenNotFound()
        {
            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.PatchRestaurantAsync(It.IsAny<RestaurantApiModel>())).ReturnsAsync((Guid?)null);
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            mockValidator.Setup(m => m.Validate(It.IsAny<RestaurantApiModel>())).Returns(new FluentValidation.Results.ValidationResult());
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.PatchRestaurantAsync(new RestaurantApiModel());

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task SearchReturnsBadRequestWhenValidationFails()
        {
            var validationFail = new List<ValidationFailure>()
            {
                new ValidationFailure("default","default message")
            };

            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.SearchRestaurantsAsync(It.IsAny<RestaurantSearchApiModel>())).ReturnsAsync(new List<RestaurantApiModel>());
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            mockSearchValidator.Setup(m => m.Validate(It.IsAny<RestaurantSearchApiModel>())).Returns(new FluentValidation.Results.ValidationResult(validationFail));

            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.SearchRestaurantsAsync(new RestaurantSearchApiModel());

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SearchReturnsOkWhenSuccessful()
        {
            var mockManager = new Mock<IRestaurantManager>();
            mockManager.Setup(m => m.SearchRestaurantsAsync(It.IsAny<RestaurantSearchApiModel>())).ReturnsAsync(new List<RestaurantApiModel>());
            var mockValidator = new Mock<IValidator<RestaurantApiModel>>();
            var mockSearchValidator = new Mock<IValidator<RestaurantSearchApiModel>>();
            mockSearchValidator.Setup(m => m.Validate(It.IsAny<RestaurantSearchApiModel>())).Returns(new FluentValidation.Results.ValidationResult());
            var controller = new RestaurantController(Logger<RestaurantController>(), mockManager.Object, mockValidator.Object, mockSearchValidator.Object);

            var result = await controller.SearchRestaurantsAsync(new RestaurantSearchApiModel());

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
