using RestaurantReviews.ApiModels.ApiModelsV1;
using RestaurantReviews.EFRepository.Entities;

using System;

namespace RetaurantReviews.BLL.Helpers
{
    internal static class ModelMapper
    {
        internal static Restaurant GetEntity(CreateRestaurantApiModel createRestaurantApiModel)
        {
            DateTime utcNow = DateTime.UtcNow;

            var itemEntity = new Restaurant
            {
                Name = createRestaurantApiModel.Name,
                AddressLine1 = createRestaurantApiModel.AddressLine1,
                AddressLine2 = createRestaurantApiModel.AddressLine2,
                City = createRestaurantApiModel.City,
                StateProvince = createRestaurantApiModel.StateProvince,
                PostalCode = createRestaurantApiModel.PostalCode,
                Country = createRestaurantApiModel.Country,
                CreatedDate = utcNow,
                ModifiedDate = utcNow
            };

            return itemEntity;
        }


        internal static RestaurantApiModel GetApiModel(Restaurant restaurant)
        {
            return new RestaurantApiModel
            {
                RestaurantApiId = restaurant.SystemId,
                Name = restaurant.Name,
                AddressLine1 = restaurant.AddressLine1,
                AddressLine2 = restaurant.AddressLine2,
                City = restaurant.City,
                StateProvince = restaurant.StateProvince,
                PostalCode = restaurant.PostalCode,
                Country = restaurant.Country,
                CreatedDate = restaurant.CreatedDate,
                ModifiedDate = restaurant.ModifiedDate
            };
        }


        internal static RestaurantReviewApiModel GetApiModel(RestaurantReview restaurantReview)
        {
            return new RestaurantReviewApiModel
            {
                RestaurantReviewApiId = restaurantReview.SystemId,
                RestaurantApiId = restaurantReview.RestaurantSystemId,
                ReviewerApiId = restaurantReview.ReviewerSystemId,
                ReviewDate = restaurantReview.ReviewDate,
                NumberOfStars = restaurantReview.NumberOfStars,
                Text = restaurantReview.Text,
                CreatedDate = restaurantReview.CreatedDate,
                ModifiedDate = restaurantReview.ModifiedDate
            };
        }
    }
}