using Microsoft.AspNetCore.Mvc;
using RestaurantReview.Controllers;
using RestaurantReview.Models;
using RestaurantReview.Services;
using System.Collections.Generic;
using Xunit;

namespace RestaurantReviewTests
{
    public class RestaurantController
    {
        public IConn connection = new Conn();

        [Fact]
        public void RestaurantsControllerTest()
        {
            //Arrange
            var RC = new RestaurantsController(connection);

            //Act
            var result = RC.Get("Boston");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}