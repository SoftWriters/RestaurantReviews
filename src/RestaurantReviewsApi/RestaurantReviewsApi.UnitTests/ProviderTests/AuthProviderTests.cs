using RestaurantReviewsApi.Bll.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.ProviderTests
{
    public class AuthProviderTests : AbstractTestHandler   
    {
        private const string JwtIssuer = "https://localhost:44384";
        private const string JwtKey = "ThisIsATheKeyForJWT";


        [Fact]
        public void GetAccessTokenApiModelReturnsApiModel()
        {
            var provider = new AuthProvider(JwtIssuer, JwtKey);
            var accessModel = provider.GetAccessTokenApiModel(HelperFunctions.RandomString(20));
            Assert.NotNull(accessModel);
            Assert.NotNull(accessModel.AccessToken);
        }

        [Fact]
        public void CanGetUserModelFromBearer()
        {
            var userName = HelperFunctions.RandomString(20);
            var provider = new AuthProvider(JwtIssuer, JwtKey);
            var accessModel = provider.GetAccessTokenApiModel(userName);
            var userModel = provider.GetUserModel(accessModel.AccessToken);
            Assert.Equal(userName, userModel.UserName);
        }
    }
}
