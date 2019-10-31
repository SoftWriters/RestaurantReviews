using Microsoft.AspNetCore.Mvc;
using RestaurantReview.Controllers;
using RestaurantReview.Models;
using RestaurantReview.Services;
using System.Net;
using Xunit;

namespace RestaurantReviewTests.ControllerTests
{
    public class RestaurantsControllerTests
    {
        public IConn connection = new Conn();

        [Fact]
        public void GetTest_ReturnsValid()
        {
            //Arrange
            var RC = new RestaurantsController(connection);

            //Act
            var result = RC.Get("Boston");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetTest_RejectsInValid()
        {
            //Arrange
            var RC = new RestaurantsController(connection);
            
            //Act
            var result = RC.Get("HocusPocus");

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void PostTest_SuccessReturnsOK()
        {
            var RC = new RestaurantsController(connection);

            var restaurant = new Restaurant();
            restaurant.Name = "My Test Restaurant";
            restaurant.City = "Pittsburgh";

            var result = RC.Post(restaurant);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostTest_FailedReturnsNotFound()
        {
            var RC = new RestaurantsController(connection);

            var restaurant = new Restaurant();
            restaurant.City = "bad!!! city1234";
            restaurant.Name = "good name";

            var result = RC.Post(restaurant);

            Assert.IsType<ObjectResult>(result);
        }
    }
}

