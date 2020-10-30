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
    [Route("api/reviews")]
    [ApiController]
    public class RestarauntReviewController : ControllerBase
    {
        private IRestaurantReviewRepository _restaurantReviewRepository;
        public RestarauntReviewController(IRestaurantReviewRepository repository)
        {
            _restaurantReviewRepository = repository;
        }

        [HttpPut]
        [Route("restaurants")]
        public IActionResult PutRestaurant(string name, string city)
        {
            var tryId = IdModule.create(Guid.NewGuid());
            var tryName = NonEmptyStringModule.create(name);
            var tryCity = NonEmptyStringModule.create(city);

            if (tryName.IsError) return BadRequest(tryName.ErrorValue);
            if (tryCity.IsError) return BadRequest(tryCity.ErrorValue);

            var restaurant = new Restaurant(tryId.ResultValue, tryName.ResultValue, tryCity.ResultValue);
            _restaurantReviewRepository.AddRestaurant(restaurant);

            return Ok("Restaurant added.");
        }

        [HttpGet]
        [Route("restaurants")]
        public IEnumerable<Restaurant> GetRestaurants(string city)
        {
            return _restaurantReviewRepository.GetRestaurantsByCity(city);
        }
    }
}
