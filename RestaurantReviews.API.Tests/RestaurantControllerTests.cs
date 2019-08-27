using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantReviews.API.Controllers;
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
            // List that will be returned from mock repository
            var expected = new List<IRestaurant>();

            // Mock repository that will be returned from mock data factory
            var mockRepo = new Mock<IRestaurantRepository>();
            mockRepo.Setup(y => y.GetAll()).Returns(expected);

            // Mock data factory
            var mockDataFactory = new Mock<IDataFactory>();
            mockDataFactory.Setup(y => y.RestaurantRepo).Returns(mockRepo.Object);

            // Setup and execute
            var controller = new RestaurantController(mockDataFactory.Object);
            var actionResult = controller.Get();

            // Assertions
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actual = okObjectResult.Value;
            Assert.Equal(expected, actual);
        }
    }
}
