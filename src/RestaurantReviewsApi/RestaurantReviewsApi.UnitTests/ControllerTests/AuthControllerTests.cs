using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantReviewsApi.Bll.Providers;
using RestaurantReviewsApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.ControllerTests
{
    public class AuthControllerTests : AbstractTestHandler
    {
        [Fact]
        public void LoginReturnsOkWhenSuccessful()
        {
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.AuthenticateUser(It.IsAny<string>())).Returns(true);

            var controller = new AuthController(Logger<AuthController>(), mockAuthProvider.Object);

            var result = controller.Login(HelperFunctions.RandomString(20));

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void LoginReturnsForbidWhenUnsuccessful()
        {
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(m => m.AuthenticateUser(It.IsAny<string>())).Returns(false);

            var controller = new AuthController(Logger<AuthController>(), mockAuthProvider.Object);

            var result = controller.Login(HelperFunctions.RandomString(20));

            Assert.NotNull(result);
            Assert.IsType<ForbidResult>(result);
        }
    }
}
