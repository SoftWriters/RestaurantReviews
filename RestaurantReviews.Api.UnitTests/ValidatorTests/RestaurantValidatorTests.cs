using RestaurantReviews.Api.Models;
using Xunit;

namespace RestaurantReviews.Api.UnitTests.ValidatorTests
{
    public class RestaurantValidatorTests
    {
        private const string ValidName = "McDonald's";
        private const string ValidDescription = "A place.";
        private const string ValidCity = "Pittsburgh";
        private const string ValidState = "PA";
        private const string TooShortState = "P";
        private const string TooLongState = "PEN";
        
        [Fact]
        public void NoNameFailsValidation()
        {
            var validator = new RestaurantValidator();
            var restaurant = new Restaurant
            {
                Name = null,
                Description = ValidDescription,
                City = ValidCity,
                State = ValidState
            };

            var result = validator.IsRestaurantValid(restaurant);
            
            Assert.False(result);
        }
        
        [Fact]
        public void NoDescriptionFailsValidation()
        {
            var validator = new RestaurantValidator();
            var restaurant = new Restaurant
            {
                Name = ValidName,
                Description = null,
                City = ValidCity,
                State = ValidState
            };

            var result = validator.IsRestaurantValid(restaurant);
            
            Assert.False(result);
        }
        
        [Fact]
        public void NoCityFailsValidation()
        {
            var validator = new RestaurantValidator();
            var restaurant = new Restaurant
            {
                Name = ValidName,
                Description = ValidDescription,
                City = null,
                State = ValidState

            };

            var result = validator.IsRestaurantValid(restaurant);
            
            Assert.False(result);
        }

        [Fact]
        public void NoStateFailsValidation()
        {
            var validator = new RestaurantValidator();
            var restaurant = new Restaurant
            {
                Name = ValidName,
                Description = ValidDescription,
                City = ValidCity,
                State = null

            };

            var result = validator.IsRestaurantValid(restaurant);
            
            Assert.False(result);
        }
        
        [Fact]
        public void ShortStateFailsValidation()
        {
            var validator = new RestaurantValidator();
            var restaurant = new Restaurant
            {
                Name = ValidName,
                Description = ValidDescription,
                City = ValidCity,
                State = TooShortState

            };

            var result = validator.IsRestaurantValid(restaurant);
            
            Assert.False(result);
        }

        [Fact]
        public void LongStateFailsValidation()
        {
            var validator = new RestaurantValidator();
            var restaurant = new Restaurant
            {
                Name = ValidName,
                Description = ValidDescription,
                City = ValidCity,
                State = TooLongState

            };

            var result = validator.IsRestaurantValid(restaurant);
            
            Assert.False(result);
        }

        [Fact]
        public void NoAnythingFailsValidation()
        {
            var validator = new RestaurantValidator();
            var restaurant = new Restaurant
            {
                Name = null,
                Description = null,
                City = null,
                State = null

            };

            var result = validator.IsRestaurantValid(restaurant);
            
            Assert.False(result);
        }

        [Fact]
        public void AllGoodPassesValidation()
        {
            var validator = new RestaurantValidator();
            var restaurant = new Restaurant
            {
                Name = ValidName,
                Description = ValidDescription,
                City = ValidCity,
                State = ValidState

            };

            var result = validator.IsRestaurantValid(restaurant);
            
            Assert.True(result);
        }
    }
}