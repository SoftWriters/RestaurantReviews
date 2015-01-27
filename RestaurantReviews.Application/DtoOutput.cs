using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace RestaurantReviews
{
    public class GetCitiesOutput : IOutputDto
    {
        public List<City> Cities { get; set; }
    }

    public class GetStatesOutput : IOutputDto
    {
        public List<City> Cities { get; set; }
    }

    public class GetRestaurantsOutput : IOutputDto
    {
        public List<Restaurant> Restaurants { get; set; }
    }

    public class GetReviewsOutput : IOutputDto
    {
        public List<Review> Reviews { get; set; }
    }
}
