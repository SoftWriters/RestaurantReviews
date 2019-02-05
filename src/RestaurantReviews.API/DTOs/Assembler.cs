using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReviews.Domain;

namespace RestaurantReviews.API.DTOs
{
    public static class Assembler
    {
        public static ReviewDTO MapToDTO(Review review) 
        {
            return new ReviewDTO
            {
                CreatedTime = review.CreatedTime,
                CreatedUserId = review.CreatedUserId,
                Rating = review.Rating,
                ResturantId = review.ResturantId,
                ReviewComment = review.ReviewComment,
                ReviewId = review.ReviewId,
                ReviewTitle = review.ReviewTitle,
            };
        }

        public static CityDTO MapToDTO(City city)
        {
            return new CityDTO
            {
                CityId = city.CityId,
                Country = city.Country,
                Name = city.Name,
                StateOrProvince = city.StateOrProvince,
            };
        }

        public static ResturantDTO MapToDTO(Resturant resturant)
        {
            return new ResturantDTO
            {
                CityId = resturant.CityId,
                Name = resturant.Name,
                FullAddress = resturant.FullAddress,
                ResturantId = resturant.ResturantId,
            };
        }

        public static UserDTO MapToDTO(User user)
        {
            return new UserDTO
            {
                FullName = user.FullName,
                UserId = user.UserId,
            };
        }

        public static Resturant MapToModel(ResturantDTO resturantDTO)
        {
            return new Resturant
            {
                CityId = resturantDTO.CityId,
                Name = resturantDTO.Name,
                FullAddress = resturantDTO.FullAddress,
                ResturantId = resturantDTO.ResturantId,
            };
        }

        public static Review MapToModel(ReviewDTO reviewDTO) 
        {
            return new Review
            {
                CreatedUserId = reviewDTO.CreatedUserId,
                Rating = reviewDTO.Rating,
                ResturantId = reviewDTO.ResturantId,
                ReviewComment = reviewDTO.ReviewComment,
                ReviewId = reviewDTO.ReviewId,
                ReviewTitle = reviewDTO.ReviewTitle,
                CreatedTime = reviewDTO.CreatedTime,
            };
        }
    }
}
