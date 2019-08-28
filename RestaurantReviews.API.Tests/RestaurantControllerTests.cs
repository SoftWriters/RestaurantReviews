using Moq;
using RestaurantReviews.API.Controllers;
using RestaurantReviews.API.Models;
using RestaurantReviews.Interfaces.Factories;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repository;
using System.Collections.Generic;
using Xunit;

namespace RestaurantReviews.API.Tests
{
    public class RestaurantControllerTests
    {
        [Fact]
        public void Get_ShouldReturnResultsOfRepoGetAll()
        {
            // Setup
            (var mockRepo, var mockDataFactory, var controller) = SetupMocksAndController();
            var expected = new List<IRestaurant>();
            mockRepo.Setup(y => y.GetAll()).Returns(expected);

            // Execute
            var actionResult = controller.Get();

            // Assert
            var actual = TestHelper.GetOkResult(actionResult);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetById_ShouldReturnResultOfRepoGetById()
        {
            // Setup
            (var mockRepo, var mockDataFactory, var controller) = SetupMocksAndController();
            var expected = new Restaurant();
            mockRepo.Setup(y => y.GetById(It.IsAny<long>())).Returns(expected);

            // Execute
            var actionResult = controller.Get(0);

            // Assert
            var actual = TestHelper.GetOkResult(actionResult);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Post_ShouldCallRepoCreateWithCorrectModel()
        {
            // Setup
            (var mockRepo, var mockDataFactory, var controller) = SetupMocksAndController();
            var newModel = new Restaurant();
            mockDataFactory.Setup(y => y.RestaurantRepo).Returns(mockRepo.Object);

            // Execute
            controller.Post(newModel);

            // Assert
            mockRepo.Verify(c => c.Create(newModel), Times.Once());
        }

        (Mock<IRestaurantRepository>, Mock<IDataFactory>, RestaurantController) SetupMocksAndController()
        {
            // Mock repository that will be returned from mock data factory
            var mockRepo = new Mock<IRestaurantRepository>();

            // Mock data factory
            var mockDataFactory = new Mock<IDataFactory>();
            mockDataFactory.Setup(y => y.RestaurantRepo).Returns(mockRepo.Object);

            return (mockRepo, mockDataFactory, new RestaurantController(mockDataFactory.Object));
        }
    }
}
