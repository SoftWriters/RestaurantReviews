using RestaurantReviewsAPI.Models;
using RestaurantReviewsAPI.Models.DataTransferObjects;

namespace RestaurantReviewsAPI.Helpers
{
    public static class DTOHelper
    {
        public static RatingInfoDTO ConvertToDTO(Rating rating)
        {
            if (rating == null)
            {
                return null;
            };

            return new RatingInfoDTO
            {
                RatingId = rating.Id,
                Value = rating.Value,
                Name = rating.Name
            };
        }

        public static CityInfoDTO ConvertToDTO(City city)
        {
            if (city == null)
            {
                return null;
            };

            return new CityInfoDTO
            {
                CityId = city.Id,
                Name = city.Name,
                State = city.State
            };
        }

        public static MobileUserInfoDTO ConvertToDTO(MobileUser mobileUser)
        {
            if (mobileUser == null)
            {
                return null;
            };

            return new MobileUserInfoDTO
            {
                UserId = mobileUser.Id,
                Name = mobileUser.Name
            };
        }

        public static RestaurantInfoDTO ConvertToDTO(Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return null;
            };

            return new RestaurantInfoDTO
            {
                RestaurantId = restaurant.Id,
                Name = restaurant.Name,
                CreateDT = restaurant.CreateDT,
                CityInfo = new CityInfoDTO
                {
                    CityId = restaurant.City.Id,
                    Name = restaurant.City.Name,
                    State = restaurant.City.State
                }
            };
        }

        public static ReviewInfoDTO ConvertToDTO(Review review)
        {
            if (review == null)
            {
                return null;
            };

            return new ReviewInfoDTO
            { 
                ReviewId = review.Id,
                Comment = review.Comment,
                RatingInfo = new RatingInfoDTO
                {
                    RatingId = review.Rating.Id,
                    Value = review.Rating.Value,
                    Name = review.Rating.Name
                },
                CreateDT = review.CreateDT,
                RestaurantInfo = new RestaurantInfoDTO
                {
                    RestaurantId = review.Restaurant.Id,
                    Name = review.Restaurant.Name,
                    CreateDT = review.Restaurant.CreateDT,
                    CityInfo = new CityInfoDTO
                    {
                        CityId = review.Restaurant.City.Id,
                        Name = review.Restaurant.City.Name,
                        State = review.Restaurant.City.State
                    }
                },
                UserInfo = new MobileUserInfoDTO
                {
                    UserId = review.MobileUser.Id,
                    Name = review.MobileUser.Name
                }
            };
        }
    }
}
