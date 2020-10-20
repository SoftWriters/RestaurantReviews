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
        private IValidator<ReviewApiModel> ReviewApiModelValidator => new ReviewApiModelValidator();

        [Fact]
        public void ReviewApiModelRestaurantIdValidation()
        {
            var apiModel = new ReviewApiModel();
            var validation = ReviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "RestaurantId");
            Assert.NotNull(validation);
            Assert.Equal("RestaurantId is required.", validation.ErrorMessage);

            apiModel.RestaurantId = Guid.NewGuid();
            validation = ReviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "RestaurantId");
            Assert.Null(validation);
        }

        [Fact]
        public void ReviewApiModelRatingValidation()
        {
            var apiModel = new ReviewApiModel();
            var validation = ReviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.NotNull(validation);
            Assert.Equal("Rating is required.", validation.ErrorMessage);

            apiModel.Rating = 0;
            validation = ReviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.NotNull(validation);
            Assert.Equal("The Rating field must be between 1 and 10.", validation.ErrorMessage);

            apiModel.Rating = 11;
            validation = ReviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.NotNull(validation);
            Assert.Equal("The Rating field must be between 1 and 10.", validation.ErrorMessage);

            apiModel.Rating = 1;
            validation = ReviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.Null(validation);

            apiModel.Rating = 10;
            validation = ReviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Rating");
            Assert.Null(validation);
        }

        [Fact]
        public void ReviewApiModelDetailsValidation()
        {
            var apiModel = new ReviewApiModel() { Details = HelperFunctions.RandomString(4001) };
            var validation = ReviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Details");
            Assert.NotNull(validation);
            Assert.Equal("The Details field can only be up to 4000 characters long.", validation.ErrorMessage);

            apiModel.Details = HelperFunctions.RandomString(4000);
            validation = ReviewApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Details");
            Assert.Null(validation);
        }
    }
}
