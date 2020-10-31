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

        [HttpPut]
        [Route("restaurants/new")]
        public IActionResult PutRestaurant(string name, string city)
        {
            var tryName = NonEmptyStringModule.create(name);
            var tryCity = NonEmptyStringModule.create(city);

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
            var tryCity = NonEmptyStringModule.create(city);

            if (tryCity.IsError) return BadRequest(tryCity.ErrorValue);

            var restaurants = _restaurantReviewRepository.GetRestaurantsByCity(tryCity.ResultValue);

            return Ok(RestaurantModule.unwrapMany(restaurants));
        }

        [HttpPost]
        [Route("new")]
        public IActionResult PostReview(Guid userId, Guid restaurantId, int rating, string reviewText)
        {
            var tryUserId = IdModule.create(userId);
            var tryRestaurantId = IdModule.create(restaurantId);
            var tryRating = RatingModule.create(rating);

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
            var tryGuid = IdModule.create(id);
            if (tryGuid.IsError) return BadRequest(tryGuid.ErrorValue);

            _restaurantReviewRepository.DeleteReview(tryGuid.ResultValue);

            return Ok("Review deleted.");
        }

        [HttpPost]
        [Route("user/add")]
        public IActionResult AddUser(string firstName, string lastName)
        {
            var tryFirst = NonEmptyStringModule.create(firstName);

            if (tryFirst.IsError) return BadRequest(tryFirst.ErrorValue);

            var user = new User(CreateNewId(), tryFirst.ResultValue, lastName ?? string.Empty);
            _restaurantReviewRepository.AddUser(user);

            return Ok("User created.");
        }

        [HttpGet]
        [Route("user")]
        public IActionResult GetReviewsByUser(Guid id)
        {
            var tryId = IdModule.create(id);
            if (tryId.IsError) return BadRequest(tryId.ErrorValue);

            var reviews = _restaurantReviewRepository.GetReviewsByUser(tryId.ResultValue);

            return Ok(ReviewModule.unwrapMany(reviews));
        }
    }
}
