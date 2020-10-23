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
    public class RestaurantApiModelValidatorTests
    {
        private IValidator<RestaurantApiModel> RestaurantApiModelValidator => new RestaurantApiModelValidator();

        [Fact]
        public void RestaurantApiModelNameValidation()
        {
            var apiModel = new RestaurantApiModel();
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Name");
            Assert.NotNull(validation);
            Assert.Equal("Name is required.", validation.ErrorMessage);

            apiModel.Name = HelperFunctions.RandomString(101);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Name");
            Assert.NotNull(validation);
            Assert.Equal("The Name field can only be up to 100 characters long.", validation.ErrorMessage);

            apiModel.Name = HelperFunctions.RandomString(100);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Name");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantApiModelCityValidation()
        {
            var apiModel = new RestaurantApiModel();
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "City");
            Assert.NotNull(validation);
            Assert.Equal("City is required.", validation.ErrorMessage);

            apiModel.City = HelperFunctions.RandomString(101);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "City");
            Assert.NotNull(validation);
            Assert.Equal("The City field can only be up to 100 characters long.", validation.ErrorMessage);

            apiModel.City = HelperFunctions.RandomString(100);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "City");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantApiModelStateValidation()
        {
            var apiModel = new RestaurantApiModel();
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "State");
            Assert.NotNull(validation);
            Assert.Equal("State is required.", validation.ErrorMessage);

            apiModel.State = HelperFunctions.RandomString(4);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "State");
            Assert.NotNull(validation);
            Assert.Equal("The State field be a valid value.", validation.ErrorMessage);

            apiModel.State = HelperFunctions.RandomElement<string>(ValidationHelper.ValidationConstants.StateAbbreviations);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "State");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantApiModelZipCodeValidation()
        {
            var apiModel = new RestaurantApiModel() { ZipCode = "invalid" };
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "ZipCode");
            Assert.NotNull(validation);
            Assert.Equal("The ZipCode field be a valid value.", validation.ErrorMessage);

            apiModel.ZipCode = "12345";
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "ZipCode");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantApiModelPhoneValidation()
        {
            var apiModel = new RestaurantApiModel() { Phone = "invalid" };
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Phone");
            Assert.NotNull(validation);
            Assert.Equal("The Phone field be a valid value.", validation.ErrorMessage);

            apiModel.Phone = "4129229229";
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Phone");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantApiModelEmailValidation()
        {
            var apiModel = new RestaurantApiModel() { Email = "invalid" };
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Email");
            Assert.NotNull(validation);
            Assert.Equal("The Email field be a valid value.", validation.ErrorMessage);

            apiModel.Email = "valid@validemail.com";
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Email");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantApiModelAddressLine1Validation()
        {
            var apiModel = new RestaurantApiModel() { AddressLine1 = HelperFunctions.RandomString(101) };
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "AddressLine1");
            Assert.NotNull(validation);
            Assert.Equal("The AddressLine1 field can only be up to 100 characters long.", validation.ErrorMessage);

            apiModel.AddressLine1 = HelperFunctions.RandomString(100);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "AddressLine1");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantApiModelAddressLine2Validation()
        {
            var apiModel = new RestaurantApiModel() { AddressLine2 = HelperFunctions.RandomString(101) };
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "AddressLine2");
            Assert.NotNull(validation);
            Assert.Equal("The AddressLine2 field can only be up to 100 characters long.", validation.ErrorMessage);

            apiModel.AddressLine2 = HelperFunctions.RandomString(100);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "AddressLine2");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantApiModelWebsiteValidation()
        {
            var apiModel = new RestaurantApiModel() { Website = HelperFunctions.RandomString(101) };
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Website");
            Assert.NotNull(validation);
            Assert.Equal("The Website field can only be up to 100 characters long.", validation.ErrorMessage);

            apiModel.Website = HelperFunctions.RandomString(100);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Website");
            Assert.Null(validation);
        }

        [Fact]
        public void RestaurantApiModelDescriptionValidation()
        {
            var apiModel = new RestaurantApiModel() { Description = HelperFunctions.RandomString(501) };
            var validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Description");
            Assert.NotNull(validation);
            Assert.Equal("The Description field can only be up to 500 characters long.", validation.ErrorMessage);

            apiModel.Description = HelperFunctions.RandomString(500);
            validation = RestaurantApiModelValidator.Validate(apiModel).Errors.FirstOrDefault(x => x.PropertyName == "Description");
            Assert.Null(validation);
        }
    }
}
