using AutoMapper;
using RestaurantReviews.API.Dtos;
using RestaurantReviews.Data.Entities;

namespace RestaurantReviews.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RestaurantDto, Restaurant>();
            CreateMap<ReviewDto, Review>();
        }
    }
}
