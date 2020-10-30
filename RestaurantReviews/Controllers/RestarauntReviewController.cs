using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
            var tryCity = ValidateNonEmptyString(city);

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

            _restaurantReviewRepository.AddReview(
                new Review(CreateNewId(), tryUserId.ResultValue, tryRestaurantId.ResultValue, tryRating.ResultValue, reviewText));

            return Ok("Review submitted.");
        }        

        [HttpDelete]
        public IActionResult DeleteReview(Guid id)
        {
            var tryGuid = ValidateId(id);
            if (tryGuid.IsError) return BadRequest(tryGuid.ErrorValue);

            _restaurantReviewRepository.DeleteReview(tryGuid.ResultValue);

            return Ok("Review deleted.");
        }

        [HttpPost]
        [Route("user/add")]
        public IActionResult AddUser(string firstName, string lastName)
        {
            var tryFirst = ValidateNonEmptyString(firstName);

            if (tryFirst.IsError) return BadRequest(tryFirst.ErrorValue);

            var user = new User(CreateNewId(), tryFirst.ResultValue, lastName ?? string.Empty);
            return Ok("User created.");
        }

        [HttpGet]
        [Route("user")]
        public IActionResult GetReviewsByUser(Guid id)
        {
            var tryId = ValidateId(id);
            if (tryId.IsError) return BadRequest(tryId.ErrorValue);

            var reviews = _restaurantReviewRepository.GetReviewsByUser(tryId.ResultValue);

            return Ok(ReviewModule.unwrapMany(reviews));
        }
    }
}
