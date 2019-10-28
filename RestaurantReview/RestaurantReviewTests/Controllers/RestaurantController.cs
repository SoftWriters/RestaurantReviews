using Microsoft.AspNetCore.Mvc;
using RestaurantReview.Controllers;
<<<<<<< HEAD
using RestaurantReview.Services;
=======
using RestaurantReview.Models;
using RestaurantReview.Services;
using System.Collections.Generic;
>>>>>>> 0e003586d4895633c82957f8d50d4fc4e29eed18
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