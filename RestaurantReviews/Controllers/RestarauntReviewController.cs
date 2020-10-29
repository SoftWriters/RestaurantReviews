using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.DataAccess;
using RestaurantReviews.DomainModel;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestarauntReviewController : ControllerBase
    {
        private IRestaurantReviewRepository _restaurantReviewRepository;
        public RestarauntReviewController(IRestaurantReviewRepository repository)
        {
            _restaurantReviewRepository = repository;
        }

        [HttpGet]
        public IEnumerable<Restaurant> GetRestaurants(string city)
        {
            return _restaurantReviewRepository.GetRestaurantsByCity(city);
        }
    }
}
