using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FSharp.Core;
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
        private Id CreateNewId() =>
            // this will always have a valid value, no need to check IsError
            // since the NewGuid() method always returns a non-empty guid
            IdModule.create(Guid.NewGuid()).ResultValue;

        private FSharpResult<NonEmptyString, string> ValidateNonEmptyString(string str) =>
            NonEmptyStringModule.create(str);

        [HttpPut]
        [Route("restaurants")]
        public IActionResult PutRestaurant(string name, string city)
        {
            var tryName = ValidateNonEmptyString(name);
            var tryCity = ValidateNonEmptyString(city);

            if (tryName.IsError) return BadRequest(tryName.ErrorValue);
            if (tryCity.IsError) return BadRequest(tryCity.ErrorValue);

            _restaurantReviewRepository.AddRestaurant(
                new Restaurant(CreateNewId(), tryName.ResultValue, tryCity.ResultValue));

            return Ok("Restaurant added.");
        }

        [HttpGet]
        [Route("restaurants")]
        public IActionResult GetRestaurants(string city)
        {
            var tryCity = ValidateCity(city);

            if (tryCity.IsError) return BadRequest(tryCity.ErrorValue);

            var restaurants = _restaurantReviewRepository.GetRestaurantsByCity(tryCity.ResultValue);

            return Ok(RestaurantModule.unwrapMany(restaurants));
        }
    }
}
