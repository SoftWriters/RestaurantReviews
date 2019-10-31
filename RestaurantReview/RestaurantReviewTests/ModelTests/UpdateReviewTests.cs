using RestaurantReview.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestaurantReviewTests.ModelTests
{
    public class UpdateReviewTests
    {
        [Fact]
        public void UpdateReview_ReviewTextShouldBeALeastTwoWords()
        {
            //Arrange
            var sut = new UpdateReview
            {
                ReviewText = "hello"
            };

            //Act
            var testText = sut.IsValidReviewText();

            //Assert
            Assert.False(testText, "should be false");
        }
    }
}
