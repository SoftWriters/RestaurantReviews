using FluentValidation;
using Moq;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.ControllerTests
{
    public class RestaurantControllerTests
    {
        [Fact]
        public void DeleteReturnsOk()
        {
            //var mockManager = new Mock<IRestaurantManager>();
            //var mockValidator = new Mock<AbstractValidator<RestaurantApiModel>>();
            //var mockSearchValidator = new Mock<AbstractValidator<RestaurantApiModel>>();
            //var controller = new RestaurantController(Logger<RestaurantController>())
            //// Arrange
            //var controller = new Products2Controller(mockRepository.Object);

            //// Act
            //IHttpActionResult actionResult = controller.Delete(10);

            //// Assert
            //Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }
    }
}
