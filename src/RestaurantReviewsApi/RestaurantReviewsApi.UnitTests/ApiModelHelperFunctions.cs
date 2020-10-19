using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.UnitTests
{
    public static class ApiModelHelperFunctions
    {
        public static RestaurantApiModel RandomRestaurantApiModel()
        {
            return new RestaurantApiModel()
            {
                Name = HelperFunctions.RandomString(20),
                City =  HelperFunctions.RandomString(20),
                State = HelperFunctions.RandomElement<string>(ValidationHelper.ValidationConstants.StateAbbreviations),
                ZipCode = "12345",
                AddressLine1 = HelperFunctions.RandomString(20),
                Description = HelperFunctions.RandomString(100),
                Email = HelperFunctions.RandomEmail(),
                Phone = HelperFunctions.RandomPhone(),
                AddressLine2 = HelperFunctions.RandomString(20),
                Website = HelperFunctions.RandomString(20)
            };
        }
        
        public static ReviewApiModel RandomReviewApiModel(Guid restaurantId)
        {
            return new ReviewApiModel()
            {
                Details = HelperFunctions.RandomString(500),
                Rating = HelperFunctions.RandomNumber(10),
                UserName = HelperFunctions.RandomString(20),
                RestaurantId = restaurantId
            };
        }
    }
}
