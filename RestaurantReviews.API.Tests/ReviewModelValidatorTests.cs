using Moq;
using RestaurantReviews.Business.Validators;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repositories;
using RestaurantReviews.Models;
using System;
using System.Linq;
using Xunit;

namespace RestaurantReviews.API.Tests
{
    public class ReviewModelValidatorTests
    {
        [Fact]
        public void Get_ShouldPassValidationOnValidInput()
        {
            // Setup
            (var validator, var mockRestaurantRepository, var mockUserRepository) = SetupMocksAndValidator();
            mockRestaurantRepository.Setup(p => p.GetById(It.IsAny<long>())).Returns(new Restaurant());
            mockUserRepository.Setup(p => p.GetById(It.IsAny<long>())).Returns(new User());

            // Execute
            var errors = validator.Validate(new Review { Content = Guid.NewGuid().ToString() });

            // Assert
            var actualHasErrors = errors?.Any() == true;
            Assert.False(actualHasErrors);
        }

        [Fact]
        public void Get_ShouldFailIfContentIsNull()
        {
            // Setup
            (var validator, var mockRestaurantRepository, var mockUserRepository) = SetupMocksAndValidator();

            // Execute
            var errors = validator.Validate(new Review());

            // Assert
            Assert.True(errors?.Any(p => p.Contains("Content is required"))); // <- this is not ideal, but didn't want to implement a more complex error object
        }

        [Fact]
        public void Get_ShouldFailIfRestaurantIdIsInvalid()
        {
            // Setup
            (var validator, var mockRestaurantRepository, var mockUserRepository) = SetupMocksAndValidator();
            mockRestaurantRepository.Setup(p => p.GetById(It.IsAny<long>())).Returns((IRestaurant)null);

            // Execute
            var errors = validator.Validate(new Review());

            // Assert
            Assert.True(errors?.Any(p => p.Contains("RestaurantId must be associated with an existing restaurant"))); // <- this is not ideal, but didn't want to implement a more complex error object
        }

        [Fact]
        public void Get_ShouldFailIfUserIdIsInvalid()
        {
            // Setup
            (var validator, var mockRestaurantRepository, var mockUserRepository) = SetupMocksAndValidator();
            mockUserRepository.Setup(p => p.GetById(It.IsAny<long>())).Returns((IUser)null);

            // Execute
            var errors = validator.Validate(new Review());

            // Assert
            Assert.True(errors?.Any(p => p.Contains("UserId must be associated with an existing user"))); // <- this is not ideal, but didn't want to implement a more complex error object
        }

        (ReviewModelValidator validator, Mock<IRestaurantRepository> mockRestaurantRepository, Mock<IUserRepository> mockUserRepository) SetupMocksAndValidator()
        {
            var mockRestaurantRepository = new Mock<IRestaurantRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var validator = new ReviewModelValidator(mockRestaurantRepository.Object, mockUserRepository.Object);
            return (validator, mockRestaurantRepository, mockUserRepository);
        }
    }
}
