using RestaurantReviewsApi.Bll.Models;
using RestaurantReviewsApi.Bll.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.TranslatorTests
{
    public class ApiModelTranslatorTest : AbstractTestHandler
    {
        private IApiModelTranslator Translator => new ApiModelTranslator();

        [Fact]
        public void RestaurantApiModel()
        {
            var guid = AddRestaurant();
            var dbModel = DbContext.Restaurant.First(x => x.RestaurantId == guid);
            var average = 4.5f;
            var apiModel = Translator.ToRestaurantApiModel(dbModel, average);

            Assert.Equal(dbModel.Name, apiModel.Name);
            Assert.Equal(dbModel.AddressLine1, apiModel.AddressLine1);
            Assert.Equal(dbModel.AddressLine2, apiModel.AddressLine2);
            Assert.Equal(dbModel.City, apiModel.City);
            Assert.Equal(dbModel.State, apiModel.State);
            Assert.Equal(dbModel.ZipCode, apiModel.ZipCode);
            Assert.Equal(dbModel.Phone, apiModel.Phone);
            Assert.Equal(dbModel.Website, apiModel.Website);
            Assert.Equal(dbModel.Email, apiModel.Email);
            Assert.Equal(dbModel.Description, apiModel.Description);
            Assert.Equal(average, apiModel.AverageRating);
        }

        [Fact]
        public void RestaurantModel()
        {
            var apiModel = ApiModelHelperFunctions.RandomRestaurantApiModel();
            var dbModel = Translator.ToRestaurantModel(apiModel);

            Assert.Equal(Guid.Empty, dbModel.RestaurantId);
            Assert.Equal(apiModel.Name, dbModel.Name);
            Assert.Equal(apiModel.AddressLine1, dbModel.AddressLine1);
            Assert.Equal(apiModel.AddressLine2, dbModel.AddressLine2);
            Assert.Equal(apiModel.City, dbModel.City);
            Assert.Equal(apiModel.State, dbModel.State);
            Assert.Equal(apiModel.ZipCode, dbModel.ZipCode);
            Assert.Equal(apiModel.Phone, dbModel.Phone);
            Assert.Equal(apiModel.Website, dbModel.Website);
            Assert.Equal(apiModel.Email, dbModel.Email);
            Assert.Equal(apiModel.Description, dbModel.Description);
        }

        [Fact]
        public void RestaurantModelExistingModel()
        {
            var guid = AddRestaurant();
            var dbModel = DbContext.Restaurant.First(x => x.RestaurantId == guid);
            var apiModel = ApiModelHelperFunctions.RandomRestaurantApiModel();
            var newDbModel = Translator.ToRestaurantModel(apiModel,dbModel);

            Assert.Equal(guid, dbModel.RestaurantId);
            Assert.Equal(apiModel.Name, dbModel.Name);
            Assert.Equal(apiModel.AddressLine1, dbModel.AddressLine1);
            Assert.Equal(apiModel.AddressLine2, dbModel.AddressLine2);
            Assert.Equal(apiModel.City, dbModel.City);
            Assert.Equal(apiModel.State, dbModel.State);
            Assert.Equal(apiModel.ZipCode, dbModel.ZipCode);
            Assert.Equal(apiModel.Phone, dbModel.Phone);
            Assert.Equal(apiModel.Website, dbModel.Website);
            Assert.Equal(apiModel.Email, dbModel.Email);
            Assert.Equal(apiModel.Description, dbModel.Description);
        }

        [Fact]
        public void ReviewApiModel()
        {
            var guid = AddRestaurant();
            var reviewGuid = AddReview(guid);
            var dbModel = DbContext.Review.First(x => x.ReviewId == reviewGuid);
            var apiModel = Translator.ToReviewApiModel(dbModel);

            Assert.Equal(dbModel.Rating, apiModel.Rating);
            Assert.Equal(dbModel.RestaurantId, apiModel.RestaurantId);
            Assert.Equal(dbModel.Details, apiModel.Details);
            Assert.Equal(dbModel.UserName, apiModel.UserName);
            Assert.Equal(dbModel.ReviewId, apiModel.ReviewId);
        }

        [Fact]
        public void ReviewModel()
        {
            var guid = Guid.NewGuid();
            var apiModel = ApiModelHelperFunctions.RandomReviewApiModel(guid);
            var userModel = new UserModel() { UserName = HelperFunctions.RandomString(20) };
            var dbModel = Translator.ToReviewModel(apiModel, userModel);

            Assert.Equal(apiModel.Rating, dbModel.Rating);
            Assert.Equal(apiModel.RestaurantId, dbModel.RestaurantId);
            Assert.Equal(apiModel.Details, dbModel.Details);
            Assert.Equal(userModel.UserName, dbModel.UserName);
        }
    }
}