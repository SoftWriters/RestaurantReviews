using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.Bll.Translators
{
    public class ApiModelTranslator : IApiModelTranslator
    {
        public RestaurantApiModel ToRestaurantApiModel(Restaurant restaurantModel)
        {
            throw new NotImplementedException();
        }

        public Restaurant ToRestaurantModel(RestaurantApiModel restaurantApiModel)
        {
            throw new NotImplementedException();
        }

        public Review ToReview(ReviewApiModel reviewApiModel)
        {
            throw new NotImplementedException();
        }

        public ReviewApiModel ToReviewApiModel(Review reviewModel)
        {
            throw new NotImplementedException();
        }
    }
}
