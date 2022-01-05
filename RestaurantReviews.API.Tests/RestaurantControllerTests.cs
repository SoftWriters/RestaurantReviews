using Moq;
using RestaurantReviews.API.Controllers;
using RestaurantReviews.Interfaces.Business;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Models;
using System.Collections.Generic;
using Xunit;

namespace RestaurantReviews.API.Tests
{
    public class RestaurantControllerTests
    {
        [Fact]
        public void Get_ShouldReturnResultsOfManagerGetAll()
        {
            // Setup
            (var mockManager, var controller) = SetupMocksAndController();
            var expected = new List<IRestaurant>();
            mockManager.Setup(y => y.GetAll()).Returns(expected);

            // Execute
            var actionResult = controller.Get();

            // Assert
            var actual = TestHelper.GetOkResult(actionResult);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetById_ShouldReturnResultOfManagerGetById()
        {
            // Setup
            (var mockManager, var controller) = SetupMocksAndController();
            var expected = new Restaurant();
            mockManager.Setup(y => y.GetById(It.IsAny<long>())).Returns(expected);

            // Execute
            var actionResult = controller.Get(0);

            // Assert
            var actual = TestHelper.GetOkResult(actionResult);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Post_ShouldCallManagerCreateWithCorrectModel()
        {
            // Setup
            (var mockManager, var controller) = SetupMocksAndController();
            var newModel = new Restaurant();

            // Execute
            controller.Post(newModel);

            // Assert
            mockManager.Verify(c => c.Create(newModel), Times.Once());
        }

        (Mock<IRestaurantManager>, RestaurantController) SetupMocksAndController()
        {
            var mockManager = new Mock<IRestaurantManager>();
            return (mockManager, new RestaurantController(mockManager.Object));
        }
    }
}
