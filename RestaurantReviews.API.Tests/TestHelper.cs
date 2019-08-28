using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace RestaurantReviews.API.Tests
{
    public static class TestHelper
    {
        public static object GetOkResult<T>(ActionResult<T> actionResult)
        {
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            return okObjectResult.Value;
        }
    }
}
