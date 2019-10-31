using RestaurantReview.Models;
using Xunit;

namespace RestaurantReviewTests.ModelTests
{
    public class RestaurantModelTests
    {
        [Fact]
        public void RestaurantModel_CityWithSpaceValid()
        {
            //Arrange
            var sut = new Restaurant
            {
                City = "Sau Paulo"
            };

            //Act
            var testCity = sut.ValidateCity();

            //Assert
            Assert.True(testCity, "should return 1 match");
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