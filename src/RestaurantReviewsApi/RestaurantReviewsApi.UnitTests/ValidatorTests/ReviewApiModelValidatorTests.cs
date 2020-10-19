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
    public class ReviewApiModelValidatorTests
    {
        private IValidator<ReviewApiModel> _reviewApiModelValidator => new ReviewApiModelValidator();

        [Fact]
        public void ReviewApiModelRestaurantIdValidation()
        {
            var apiModel = new ReviewApiModel();
            var validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "RestaurantId");
            Assert.NotNull(validation);
            Assert.Equal("RestaurantId is required.", validation.ErrorMessage);

            apiModel.RestaurantId = Guid.NewGuid();
            validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "RestaurantId");
            Assert.Null(validation);
        }

        [Fact]
        public void ReviewApiModelUserNameValidation()
        {
            var apiModel = new ReviewApiModel();
            var validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "UserName");
            Assert.NotNull(validation);
            Assert.Equal("UserName is required.", validation.ErrorMessage);

            apiModel.UserName = HelperFunctions.RandomString(101);
            validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "UserName");
            Assert.NotNull(validation);
            Assert.Equal("The UserName field can only be up to 100 characters long.", validation.ErrorMessage);

            apiModel.UserName = HelperFunctions.RandomString(100);
            validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "UserName");
            Assert.Null(validation);
        }

        [Fact]
        public void ReviewApiModelRatingValidation()
        {
            var apiModel = new ReviewApiModel();
            var validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.NotNull(validation);
            Assert.Equal("Rating is required.", validation.ErrorMessage);

            apiModel.Rating = 0;
            validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.NotNull(validation);
            Assert.Equal("The Rating field must be between 1 and 10.", validation.ErrorMessage);

            apiModel.Rating = 11;
            validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.NotNull(validation);
            Assert.Equal("The Rating field must be between 1 and 10.", validation.ErrorMessage);

            apiModel.Rating = 1;
            validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.Null(validation);

            apiModel.Rating = 10;
            validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.Null(validation);
        }

        [Fact]
        public void ReviewApiModelDetailsValidation()
        {
            var apiModel = new ReviewApiModel() { Details = HelperFunctions.RandomString(4001) };
            var validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Details");
            Assert.NotNull(validation);
            Assert.Equal("The Details field can only be up to 4000 characters long.", validation.ErrorMessage);

            apiModel.Details = HelperFunctions.RandomString(4000);
            validation = _reviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Details");
            Assert.Null(validation);
        }
    }
}
