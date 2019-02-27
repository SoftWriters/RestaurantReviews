using AutoMapper;
using RestaurantReviews.API.Dtos;
using RestaurantReviews.Data.Entities;
using System;

namespace RestaurantReviews.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>().BeforeMap((s, d) => d.Id = Guid.NewGuid());
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantDto, Restaurant>().BeforeMap((s, d) => d.Id = Guid.NewGuid());
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>().BeforeMap((s, d) => d.Id = Guid.NewGuid());
        }
    }
}
