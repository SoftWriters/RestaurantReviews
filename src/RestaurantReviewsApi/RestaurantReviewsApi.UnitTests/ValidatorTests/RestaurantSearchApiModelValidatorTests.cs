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
    public class RestaurantSearchApiModelValidatorTests
    {
        private IValidator<RestaurantSearchApiModel> _restaurantSearchApiModelValidator => new RestaurantSearchApiModelValidator();

        [Fact]
        public void RestaurantSearchApiModelStateValidation()
        {
            var apiModel = new RestaurantSearchApiModel() { State = HelperFunctions.RandomString(4) };
            var validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "State");
            Assert.NotNull(validation);
            Assert.Equal("The State field be a valid value.", validation.ErrorMessage);

            apiModel.State = HelperFunctions.RandomElement<string>(ValidationHelper.ValidationConstants.StateAbbreviations);
            validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "State");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantSearchApiModelZipCodeValidation()
        {
            var apiModel = new RestaurantSearchApiModel() { ZipCode = HelperFunctions.RandomString(4) };
            var validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "ZipCode");
            Assert.NotNull(validation);
            Assert.Equal("The ZipCode field be a valid value.", validation.ErrorMessage);

            apiModel.ZipCode = "12345";
            validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "ZipCode");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantSearchApiModelNameValidation()
        {
            var apiModel = new RestaurantSearchApiModel() { Name = HelperFunctions.RandomString(101) };
            var validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Name");
            Assert.NotNull(validation);
            Assert.Equal("The Name field can only be up to 100 characters long.", validation.ErrorMessage);

            apiModel.Name = HelperFunctions.RandomString(100);
            validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Name");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantSearchApiModelCityValidation()
        {
            var apiModel = new RestaurantSearchApiModel() { City = HelperFunctions.RandomString(101) };
            var validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "City");
            Assert.NotNull(validation);
            Assert.Equal("The City field can only be up to 100 characters long.", validation.ErrorMessage);

            apiModel.City = HelperFunctions.RandomString(100);
            validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "City");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantSearchApiModelNullValidation()
        {
            var apiModel = new RestaurantSearchApiModel() { };
            var validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors
                .FirstOrDefault(x => x.ErrorMessage == "At least one of the following fields must have a value: Name, AddressLine1, City, State, ZipCode.");
            Assert.NotNull(validation);


            apiModel = new RestaurantSearchApiModel() { Name = HelperFunctions.RandomString(20) };
            validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors
                .FirstOrDefault(x => x.ErrorMessage == "At least one of the following fields must have a value: Name, AddressLine1, City, State, ZipCode.");
            Assert.Null(validation);

            apiModel = new RestaurantSearchApiModel() { City = HelperFunctions.RandomString(20) };
            validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors
                .FirstOrDefault(x => x.ErrorMessage == "At least one of the following fields must have a value: Name, AddressLine1, City, State, ZipCode.");
            Assert.Null(validation);

            apiModel = new RestaurantSearchApiModel() { State = HelperFunctions.RandomElement<string>(ValidationHelper.ValidationConstants.StateAbbreviations) };
            validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors
                .FirstOrDefault(x => x.ErrorMessage == "At least one of the following fields must have a value: Name, AddressLine1, City, State, ZipCode.");
            Assert.Null(validation);

            apiModel = new RestaurantSearchApiModel() { ZipCode = "12345" };
            validation = _restaurantSearchApiModelValidator.Validate(apiModel).Errors
                .FirstOrDefault(x => x.ErrorMessage == "At least one of the following fields must have a value: Name, AddressLine1, City, State, ZipCode.");
            Assert.Null(validation);
        }
    }
}
