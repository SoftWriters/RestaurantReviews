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

        private FSharpResult<Id, string> ValidateId(Guid guid) =>
            IdModule.create(guid);

        private FSharpResult<Rating, string> ValidateRating(int rating) =>
            RatingModule.create(rating);

        [HttpPut]
        [Route("restaurants/new")]
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

        [HttpPost]
        [Route("new")]
        public IActionResult PostReview(Guid userId, Guid restaurantId, int rating, string reviewText)
        {
            var tryUserId = ValidateId(userId);
            var tryRestaurantId = ValidateId(restaurantId);
            var tryRating = ValidateRating(rating);

            if (tryUserId.IsError) return BadRequest(tryUserId.ErrorValue);
            if (tryRestaurantId.IsError) return BadRequest(tryRestaurantId.ErrorValue);
            if (tryRating.IsError) return BadRequest(tryRating.ErrorValue);

            var restaurant = _restaurantReviewRepository.GetRestaurant(tryRestaurantId.ResultValue);
            if (restaurant is null) return BadRequest("Restaurant does not exist. We were unable to submit this review.");

            var user = _restaurantReviewRepository.GetUser(tryUserId.ResultValue);
            if (user is null) return BadRequest("User does not exist. We were unable to submit this review.");

            _restaurantReviewRepository.AddReview(
                new Review(CreateNewId(), user, restaurant, tryRating.ResultValue, reviewText));

            return Ok("Review submitted.");
        }        
    }
}
