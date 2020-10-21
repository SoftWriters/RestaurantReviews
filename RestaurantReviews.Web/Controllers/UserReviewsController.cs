using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReviews.Web.Data.Entities;
using RestaurantReviews.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserReviewsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReviewRepository _repository;

        public UserReviewsController(ILogger<UserReviewsController> logger, IReviewRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{userId:int}/reviews")]
        [ProducesResponseType(typeof(IEnumerable<Review>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewsByUserAsync(int userId)
        {
            try
            {
                var reviews = await _repository.GetReviewsAsync(userId);

                if (reviews == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(reviews);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{username}/reviews")]
        [ProducesResponseType(typeof(IEnumerable<Review>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewsByUserAsync(string username)
        {
            try
            {
                var reviews = await _repository.GetReviewsAsync(username);

                if (reviews == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(reviews);
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
