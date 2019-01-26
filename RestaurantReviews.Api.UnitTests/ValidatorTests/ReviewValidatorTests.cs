using System.Threading.Tasks;
using Moq;
using RestaurantReviews.Api.DataAccess;
using RestaurantReviews.Api.Models;
using Xunit;

namespace RestaurantReviews.Api.UnitTests.ValidatorTests
{
    public class ReviewValidatorTests
    {
        [Fact]
        public void NoReviewerEmailFailsValidation()
        {
            var validator = new ReviewValidator(null);
            var review = new NewReview
            {
                ReviewerEmail = null
            };
            
            var result = validator.IsReviewValid(review);
            
            Assert.False(result);
        }
        
        [Fact]
        public void TooLowRatingFailsValidation()
        {
            var validator = new ReviewValidator(null);
            var review = new NewReview
            {
                ReviewerEmail = "x@y.xyz",
                RatingStars = -0.1m
            };
            
            var result = validator.IsReviewValid(review);
            
            Assert.False(result);
        }

        [Fact]
        public void TooHighRatingFailsValidation()
        {
            var validator = new ReviewValidator(null);
            var review = new NewReview
            {
                ReviewerEmail = "x@y.xyz",
                RatingStars = 5.1m
            };
            
            var result = validator.IsReviewValid(review);
            
            Assert.False(result);
        }
        
        [Fact]
        public void RestaurantNotFoundFailsValidation()
        {
            var restaurantQueryMock = new Mock<IRestaurantQuery>();
            restaurantQueryMock.Setup(q => q.GetRestaurant(999)).
                Returns(Task.FromResult<Restaurant>(null));
            var validator = new ReviewValidator(restaurantQueryMock.Object);
            var review = new NewReview
            {
                ReviewerEmail = "x@y.xyz",
                RatingStars = 5.1m,
                RestaurantId = 999
            };
            
            var result = validator.IsReviewValid(review);
            
            Assert.False(result);
        }
        
        [Fact]
        public void GoodReviewPassesValidation()
        {
            var restaurantMock = new Mock<Restaurant>();
            var restaurantQueryMock = new Mock<IRestaurantQuery>();
            restaurantQueryMock.Setup(q => q.GetRestaurant(1)).
                Returns(Task.FromResult(restaurantMock.Object));
            var validator = new ReviewValidator(restaurantQueryMock.Object);
            var review = new NewReview
            {
                ReviewerEmail = "x@y.xyz",
                RatingStars = 5.0m,
                RestaurantId = 1
            };
            
            var result = validator.IsReviewValid(review);
            
            Assert.True(result);
        }
        
        [Fact]
        public void ReallyBadReviewPassesValidation()
        {
            var restaurantMock = new Mock<Restaurant>();
            var restaurantQueryMock = new Mock<IRestaurantQuery>();
            restaurantQueryMock.Setup(q => q.GetRestaurant(1)).
                Returns(Task.FromResult(restaurantMock.Object));
            var validator = new ReviewValidator(restaurantQueryMock.Object);
            var review = new NewReview
            {
                ReviewerEmail = "x@y.xyz",
                RatingStars = 0.0m,
                RestaurantId = 1
            };
            
            var result = validator.IsReviewValid(review);
            
            Assert.True(result);
        }
    }
}