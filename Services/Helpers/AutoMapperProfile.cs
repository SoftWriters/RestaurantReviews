using AutoMapper;
using Softwriters.RestaurantReviews.Dto;
using Softwriters.RestaurantReviews.Models;

namespace Softwriters.RestaurantReviews.Services.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CityRequest, City>();
            CreateMap<CriticRequest, Critic>();
            CreateMap<MenuRequest, Menu>();
            CreateMap<RestaurantRequest, Restaurant>();
            CreateMap<RestaurantTypeRequest, RestaurantType>();
            CreateMap<ReviewRequest, Review>();
        }
    }
}