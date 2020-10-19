using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantReviewsApi.UnitTests
{
    public class AbstractTestHandlerTests : AbstractTestHandler
    {
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }
    }
}
