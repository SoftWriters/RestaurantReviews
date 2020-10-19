using Microsoft.AspNetCore.Http;
using RestaurantReviewsApi.ApiModels.ApiModels;
using RestaurantReviewsApi.Bll.Models;
using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Text;

namespace RestaurantReviewsApi.Bll.Providers
{
    public interface IAuthProvider
    {
        public bool AuthenticateUser(string username);
        public AccessTokenApiModel GetAccessTokenApiModel(string username);
        public UserModel GetUserModel(string token);
        public UserModel GetUserModel(HttpRequest request);

    }
}
