using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.API.Dtos;
using RestaurantReviews.Data.Contracts.Logging;
using RestaurantReviews.Data.Contracts.Repositories;
using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.Extensions;

namespace RestaurantReviews.API.Controllers.PublicServices
{
    [Route("api/restaurantreviews")]
    [ApiController]
    public class RestaurantReviewsController : ControllerBase
    {
        #region Private Variables

        private ILoggerManager _loggerManager;
        private IMapper _mapper;
        private IRepositoryWrapper _repositoryWrapper;

        #endregion Private Variables

        #region Constructors

        public RestaurantReviewsController(ILoggerManager loggerManager, IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        #endregion Constructors

        #region Actions

        /// <summary>
        /// Gets reviews by a user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetReviewsByUser{id}", Name = "GetReviewsByUser")]
        public async Task<IActionResult> GetReviewsByUser(Guid id)
        {
            try
            {
                var reviews = await _repositoryWrapper.Review.GetReviewsByUser(id);
                _loggerManager.LogInfo($"Returned { reviews.Count() } restaurants for GetReviewByUser: {id}");
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetReviewByUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets all restaurants for a city, state, country combination
        /// </summary>
        /// <param name="restaurantsByCityDto"></param>
        /// <returns></returns>
        [HttpGet("GetRestaurantsByCity", Name = "GetRestaurantsByCity")]
        public async Task<IActionResult> GetRestaurantsByCity([FromQuery] RestaurantsByCityDto restaurantsByCityDto)
        {
            try
            {
                var restaurants = await _repositoryWrapper.Restaurant.GetRestaurantsByCity(restaurantsByCityDto.City, restaurantsByCityDto.State, restaurantsByCityDto.Country);
                _loggerManager.LogInfo($"Returned { restaurants.Count() } restaurants for RestaurantsByCityDto: {restaurantsByCityDto}");
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetRestaurantsByCity action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Post a new restaurant
        /// </summary>
        /// <param name="restaurantDto"></param>
        /// <returns></returns>
        [HttpPost("PostANewRestaurant", Name = "PostANewRestaurant")]
        public async Task<IActionResult> PostANewRestaurant([FromQuery] RestaurantDto restaurantDto)
        {
            try
            {
                if (restaurantDto == null)
                {
                    _loggerManager.LogError("RestaurantDto object sent from client is null.");
                    return BadRequest("RestaurantDto object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid RestaurantDto object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var restaurant = _mapper.Map<Restaurant>(restaurantDto);
                await _repositoryWrapper.Restaurant.CreateRestaurant(restaurant);
                return CreatedAtRoute("GetRestaurantById", new { id = restaurant.Id }, restaurant);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside PostANewRestaurant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Posts a review for a restaurant by a user
        /// </summary>
        /// <param name="reviewDto"></param>
        /// <returns></returns>
        [HttpPost("PostAReview", Name = "PostAReview")]
        public async Task<IActionResult> PostAReview([FromQuery] ReviewDto reviewDto)
        {
            try
            {
                if (reviewDto == null)
                {
                    _loggerManager.LogError("ReviewDto object sent from client is null.");
                    return BadRequest("ReviewDto object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid reviewDto object sent from client.");
                    return BadRequest("Invalid reviewDto model object");
                }
                var review = _mapper.Map<Review>(reviewDto);
                review.SubmissionDate = DateTime.UtcNow;
                await _repositoryWrapper.Review.CreateReview(review);
                return CreatedAtRoute("GetReviewById", new { id = review.Id }, review);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside CreateReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a review from the data repository by its unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAReview{id}", Name = "DeleteAReview")]
        public async Task<IActionResult> DeleteAReview(Guid id)
        {
            try
            {
                var review = await _repositoryWrapper.Review.GetReviewById(id);
                if (review.IsEmptyObject())
                {
                    _loggerManager.LogError($"Review with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _repositoryWrapper.Review.DeleteReview(review);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside DeleteReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion Actions
    }
}