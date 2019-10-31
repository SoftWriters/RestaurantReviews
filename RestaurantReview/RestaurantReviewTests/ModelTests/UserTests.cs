using RestaurantReview.Models;
using Xunit;

namespace RestaurantReviewTests.ModelTests
{
    public class UserTests
    {
        public void User_UserNameLengthSHouldBeGreaterThan7()
        {
            //Arrange
            var sut = new User
            {
                UserName = "myself"
            };

            //Act
            var validuser = sut.IsValidUser();

            //Assert
            Assert.False(validuser, "should be false");
        }

        public void User_UserNameLengthGreaterThan7IsValid()
        {
            //Arrange
            var sut = new User
            {
                UserName = "myself123"
            };

            //Act
            var validuser = sut.IsValidUser();

            //Assert
            Assert.True(validuser, "should be true");
        }
    }
}