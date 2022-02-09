using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReviews.Web.Data.Entities;
using RestaurantReviews.Web.Models;
using RestaurantReviews.Web.Repositories;
using System;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReviewRepository _repository;

        public ReviewsController(ILogger<ReviewsController> logger, IReviewRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Review), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateReviewAsync([FromBody] ReviewPostRequest request)
        {          
            try
            {
                var review = await _repository.CreateReviewAsync(request.RestaurantId, request.UserId, request.Content);

                return Ok(review);
            }
            catch (Exception ex) when (ex is RestaurantNotFoundException || ex is UserNotFoundException)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{reviewId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId)
        {
            try
            {
                var review = await _repository.DeleteReviewAsync(reviewId);

                if (review == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
