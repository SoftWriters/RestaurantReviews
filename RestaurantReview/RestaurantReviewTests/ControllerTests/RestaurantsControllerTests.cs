using Microsoft.AspNetCore.Mvc;
using RestaurantReview.Controllers;
using RestaurantReview.Services;
using Xunit;

namespace RestaurantReviewTests
{
    public class RestaurantsControllerTests
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