using FluentValidation;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Utility;
using RestaurantReviewsApi.Bll.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.ValidatorTests
{
    public class ReviewSearchApiModelValidatorTests
    {
        private IValidator<ReviewSearchApiModel> ReviewSearchApiModelValidator => new ReviewSearchApiModelValidator();

        [Fact]
        public void RestaurantSearchApiModelNullValidation()
        {
            var apiModel = new ReviewSearchApiModel() { };
            var validation = ReviewSearchApiModelValidator.Validate(apiModel).Errors
                .FirstOrDefault(x => x.ErrorMessage == "At least one of the following fields must have a value: UserName, RestaurantId.");
            Assert.NotNull(validation);


            apiModel = new ReviewSearchApiModel() { UserName = HelperFunctions.RandomString(20) };
            validation = ReviewSearchApiModelValidator.Validate(apiModel).Errors
                .FirstOrDefault(x => x.ErrorMessage == "At least one of the following fields must have a value: UserName, RestaurantId.");
            Assert.Null(validation);

            apiModel = new ReviewSearchApiModel() { RestaurantId = Guid.NewGuid() };
            validation = ReviewSearchApiModelValidator.Validate(apiModel).Errors
                .FirstOrDefault(x => x.ErrorMessage == "At least one of the following fields must have a value: UserName, RestaurantId.");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantSearchApiModelUserNameValidation()
        {
            var apiModel = new ReviewSearchApiModel() { UserName = HelperFunctions.RandomString(101) };
            var validation = ReviewSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "UserName");
            Assert.NotNull(validation);
            Assert.Equal("The UserName field can only be up to 100 characters long.", validation.ErrorMessage);

            apiModel = new ReviewSearchApiModel() { UserName = HelperFunctions.RandomString(100) };
            validation = ReviewSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "UserName");
            Assert.Null(validation);
        }
    }
}
