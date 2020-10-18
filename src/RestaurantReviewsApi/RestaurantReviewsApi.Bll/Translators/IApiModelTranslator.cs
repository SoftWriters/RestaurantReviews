using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.Bll.Translators
{
    public interface IApiModelTranslator
    {
        public RestaurantApiModel ToRestaurantApiModel(Restaurant restaurantModel);
        public Restaurant ToRestaurantModel(RestaurantApiModel restaurantApiModel, Restaurant restaurantModel = null);
        public ReviewApiModel ToReviewApiModel(Review reviewModel);
        public Review ToReviewModel(ReviewApiModel reviewApiModel, Review reviewModel = null);
    }
}
