using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Bll.Translators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.ManagerTests
{
    public class RestaurantManagerTests : AbstractTestHandler
    {
        private IApiModelTranslator _translator => new ApiModelTranslator();

        [Fact]
        public async void CanDeleteRestaurant()
        {
            var manager = new RestaurantManager(null, DbContext, _translator);
            var restaurant = await manager.DeleteRestaurantAsync(new Guid("315A0FB1-64BC-4D7E-92EB-23EF3091BA86"));
            Assert.NotNull(restaurant);
        }

        [Fact]
        public async void CanGetRestaurant()
        {
            var manager = new RestaurantManager(null, DbContext, _translator);
            var restaurant = await manager.GetRestaurantAsync(new Guid("315A0FB1-64BC-4D7E-92EB-23EF3091BA86"));
            Assert.NotNull(restaurant);
        }


    }
}
