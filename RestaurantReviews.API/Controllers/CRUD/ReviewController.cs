using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.API.Dtos;
using RestaurantReviews.Data.Contracts.Logging;
using RestaurantReviews.Data.Contracts.Repositories;
using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.Extensions;

namespace RestaurantReviews.API.Controllers.CRUD
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBaseRestaurantReviews
    {
        #region Constructors

        public ReviewController(ILoggerManager loggerManager, IMapper mapper, IRepositoryWrapper repositoryWrapper)
            : base(loggerManager, mapper, repositoryWrapper)
        {
        }

        #endregion Constructors

        #region Actions

        /// <summary>
        /// Get all reviews from data respository
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _repositoryWrapper.Review.GetAllReviews();
                _loggerManager.LogInfo($"Returned all reviews from database.");
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetAllReviews action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get a review by its unique Id from the data respository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetReviewById{id}", Name = "GetReviewById")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            try
            {
                var review = await _repositoryWrapper.Review.GetReviewById(id);
                if (review.IsEmptyObject())
                {
                    _loggerManager.LogError($"Review with id: {id}, was not found in db.");
                    return NotFound();
                }
                else
                {
                    _loggerManager.LogInfo($"Returned student with id: {id}");
                    return Ok(review);
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetReviewById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Creates a review in the data repository
        /// </summary>
        /// <param name="reviewDto"></param>
        /// <returns></returns>
        [HttpPost("CreateReview", Name = "CreateReview")]
        public async Task<IActionResult> CreateReview([FromQuery] ReviewDto reviewDto)
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
                    _loggerManager.LogError("Invalid school object sent from client.");
                    return BadRequest("Invalid model object");
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
        /// Updates a review in the data repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reviewDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateReview{id}", Name = "UpdateReview")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromQuery] ReviewDto reviewDto)
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
                    _loggerManager.LogError("Invalid ReviewDto object sent from client.");
                    return BadRequest("Invalid ReviewDto model object");
                }
                var dbReview = await _repositoryWrapper.Review.GetReviewById(id);
                if (dbReview.IsEmptyObject())
                {
                    _loggerManager.LogError($"Review with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var review = _mapper.Map<Review>(reviewDto);
                await _repositoryWrapper.Review.UpdateReview(dbReview, review);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside UpdateReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a review from the data repository by its unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteReview{id}", Name = "DeleteReview")]
        public async Task<IActionResult> DeleteReview(Guid id)
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