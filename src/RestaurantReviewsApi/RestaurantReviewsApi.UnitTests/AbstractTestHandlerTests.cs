using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void CanAddRestaurantSeedData()
        {
            var guid = AddRestaurant();
            var rest = DbContext.Restaurant.FirstOrDefault(x => x.RestaurantId == guid);
            Assert.NotNull(rest);
        }

        [Fact]
        public void CanAddReviewSeedData()
        {
            var guid = AddRestaurant();
            var reviewGuid = AddReview(guid);
            var review = DbContext.Review.FirstOrDefault(x => x.ReviewId == reviewGuid && x.RestaurantId == guid);
            Assert.NotNull(review);
        }
    }
}
