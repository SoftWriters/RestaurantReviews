using RestaurantReview.Models;
using Xunit;

namespace RestaurantReviewTests.ModelTests
{
    public class GetReviewTests
    {
        [Fact]
        public void GetReview_ReviewIdCannotBeNegative()
        {
            //Arrange
            var sut = new GetReview
            {
                ReviewId = -1
            };

            //Act
            var testId = sut.IsValidId();

            //Assert
            Assert.False(testId, "should be false");
        }

        [Fact]
        public void GetReview_ReviewsNeedAtLeastTwoWords()
        {
            //Arrange
            var sut = new GetReview
            {
                ReviewText = "Hello"
            };

            //Act
            var testText = sut.IsValidReviewText();

            //Assert
            Assert.False(testText, "should be false");
        }
    }
}