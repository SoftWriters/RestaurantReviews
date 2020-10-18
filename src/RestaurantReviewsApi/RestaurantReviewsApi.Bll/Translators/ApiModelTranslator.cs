using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.Bll.Translators
{
    public class ApiModelTranslator : IApiModelTranslator
    {
        public RestaurantApiModel ToRestaurantApiModel(Restaurant restaurantModel, float? averageRating = null)
        {
            return new RestaurantApiModel()
            {
                RestaurantId = restaurantModel.RestaurantId,
                Name = restaurantModel.Name,
                AddressLine1 = restaurantModel.AddressLine1,
                AddressLine2 = restaurantModel.AddressLine2,
                City = restaurantModel.City,
                ZipCode = restaurantModel.ZipCode,
                State = restaurantModel.State,
                Description = restaurantModel.Description,
                Email = restaurantModel.Email,
                Phone = restaurantModel.Phone,
                Website = restaurantModel.Website,
                AverageRating = averageRating
            };
        }

        public Restaurant ToRestaurantModel(RestaurantApiModel restaurantApiModel, Restaurant restaurantModel = null)
        {
            if (restaurantModel == null)
                restaurantModel = new Restaurant();

            restaurantModel.Name = restaurantApiModel.Name;
            restaurantModel.AddressLine1 = restaurantApiModel.AddressLine1;
            restaurantModel.AddressLine2 = restaurantApiModel.AddressLine2;
            restaurantModel.City = restaurantApiModel.City;
            restaurantModel.ZipCode = restaurantApiModel.ZipCode;
            restaurantModel.State = restaurantApiModel.State;
            restaurantModel.Description = restaurantApiModel.Description;
            restaurantModel.Email = restaurantApiModel.Email;
            restaurantModel.Phone = restaurantApiModel.Phone;
            restaurantModel.Website = restaurantApiModel.Website;
            restaurantModel.IsDeleted = false;

            return restaurantModel;
        }

        public ReviewApiModel ToReviewApiModel(Review reviewModel)
        {
            return new ReviewApiModel()
            {
                RestaurantId = reviewModel.RestaurantId,
                Rating = reviewModel.Rating,
                ReviewId = reviewModel.ReviewId,
                Details = reviewModel.Details,
                UserName = reviewModel.UserName
            };
        }

        public Review ToReviewModel(ReviewApiModel reviewApiModel)
        {
            return new Review()
            {
                RestaurantId = reviewApiModel.RestaurantId.Value,
                Rating = reviewApiModel.Rating,
                Details = reviewApiModel.Details,
                UserName = reviewApiModel.UserName,
            };
        }
    }
}
