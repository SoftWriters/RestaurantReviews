using RestaurantReviewsApi.ApiModels.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.Bll.Providers
{
    public interface IAuthProvider
    {
        public bool AuthenticateUser(string username);
        public AccessTokenApiModel GetAccessTokenApiModel(string username);

    }
}
