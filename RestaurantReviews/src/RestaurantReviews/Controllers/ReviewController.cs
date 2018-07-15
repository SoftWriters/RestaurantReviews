using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Models;
using System.Web.Http;
using RestaurantReviews.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly ILogger<ReviewController> _logger;
        public ReviewController(ILogger<ReviewController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of restaurant reviews that have been submitted by a user.
        /// </summary>
        /// <remarks>This method returns all reviews submitted by a user based on the user's username, email or user Id. </remarks>
        /// <param name="user"></param>
        /// <response code="200">The request was successful. </response>
        /// <response code="404">No valid reviews were found based upon the search criteria supplied.</response>
        /// <returns>List or reviews. </returns>
        [HttpGet]
        [Route("GetByUser")]
        public IActionResult GetByUser([FromUri]UserRequest user)
        {
            _logger.LogInformation($"Review Controller received request for reviews by user information. UserId: {user.UserId}, UserName: {user.UserName}");
            try
            {
                ReviewFacade facade = new ReviewFacade();
                var reviews = facade.GetByUser(user).ToList();
                if (reviews == null || reviews.Count() == 0)
                {
                    return NotFound("No Restaurant reviews exist for user. ");
                }
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Please contact support for additional information. ");
            }
        }

        /// <summary>
        /// Get reviews per restaurant. 
        /// </summary>
        /// <remarks>Returns a list of reviews for the restaurant requested. If reviews exist the response will have HttpStatusCode 200. If 
        /// there are no valid reviews then the response will have HttpStatusCode 404. </remarks>
        /// <param name="restaurant"></param>
        /// <response code="200"></response>
        /// <response code="404">No valid reviews were found based upon the search criteria supplied.</response>
        /// <returns>List or reviews. </returns>
        [HttpGet]
        [ProducesResponseType(typeof(Review), 200)]
        [ProducesResponseType(typeof(Review), 404)]
        [Route("GetByRestaurant")]
        public IActionResult GetByRestaurant([FromUri]RestaurantRequest restaurant)
        {
            _logger.LogInformation($"Review Controller received request for reviews by Restaurant. RestaurantId: {restaurant.RestaurantId}.");
            try
            {
                ReviewFacade facade = new ReviewFacade();
                List<Review> reviews = facade.GetByRestaurant(restaurant).ToList();
                if (reviews == null || reviews.Count == 0)
                {
                    return NotFound("No Restaurant reviews exist for this restaurant. ");
                }
                return Ok(reviews);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Please contact support for additional information. ");
            }
        }


        /// <summary>
        /// Adds a review for a restaurant. 
        /// </summary>
        /// <remarks>If the review is added successfully to the database a 201 reponse code is returned.</remarks>
        /// <param name="review"></param>
        /// <returns>HttpStatusCode.Created(201) the review was successfully created.</returns>
        /// <response code="201">The review was successfully created.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Review), 201)]
        [Route("Add")]
        public IActionResult AddRestaurantReview([FromBody]ReviewRequest review)
        {
            _logger.LogInformation($"Review Controller received a request to post a review. RestaurantId: {review.RestaurantId}, UserId: {review.UserId}");
            try
            {
                ReviewFacade facade = new ReviewFacade();
                facade.AddReviewForRestaurant(review);
                return StatusCode(201, "The review has been added for the restaurant. ");
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"Invalid request. Please confirm that Comment, UserId and RestaurantId are not null and the provided Ids are valid. ");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Please contact support for additional information. ");
            }
        }


        /// <summary>
        /// Removes a review based upon its Id. 
        /// </summary>
        /// <remarks></remarks>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        /// <response code="200">The review has been deleted. </response>
        /// <repsonse code="400">The review was unable to be deleted. </repsonse>
        [HttpPost]
        [ProducesResponseType(typeof(Review), 200)]
        [Route("Remove")]
        public IActionResult RemoveRestaurantReview([FromBody]long reviewId)
        {
            _logger.LogInformation($"Review controller received a request to Remove a record {reviewId}");
            if (reviewId == 0) return BadRequest("Invalid Request, reviewId must be a non-negative integer. ");
            try
            {
                ReviewFacade facade = new ReviewFacade();
                facade.RemoveRestaurantReview(reviewId);
                return Ok();
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"Invalid Request. ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Please contact support for additional information. ");
            }
        }
    }
}