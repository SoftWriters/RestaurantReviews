using RestaurantReview.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestaurantReviewTests.ModelTests
{
    public class PostReviewTests
    {
        [Fact]
        public void PostReview_ReviewIdCannotBeNegative()
        {
            //Arrange
            var sut = new PostReview
            {
                ReviewId = -1
            };

            //Act
            var testId = sut.IsValidId();

            //Assert
            Assert.False(testId, "should be false");
        }

        [Fact]
        public void PostReview_ReviewsNeedAtLeastTwoWords()
        {
            //Arrange
            var sut = new PostReview
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
