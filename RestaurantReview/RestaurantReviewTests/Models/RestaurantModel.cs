using RestaurantReview.Models;
using Xunit;

namespace RestaurantReviewTests
{
    public class RestaurantModel
    {
        [Fact]
        public void RestaurantModel_CityWithSpaceValid()
        {
            var sut = new Restaurant
            {
                City = "Sau Paulo"
            };
            Assert.True(sut.ValidateCity(), "should return 1 match");
        }

        [Fact]
        public void RestaurantModel_CityWithNumberInvalid()
        {
            var sut = new Restaurant
            {
                City = "Sau Paulo1"
            };
            Assert.False(sut.ValidateCity(), "should return 0 matches");
        }

        [Fact]
        public void RestaurantModel_CityWithSpecialCharsInvalid()
        {
            var sut = new Restaurant
            {
                City = "Sau Paulo!?!"
            };
            Assert.False(sut.ValidateCity(), "should return 0 matches");
        }
    }
}