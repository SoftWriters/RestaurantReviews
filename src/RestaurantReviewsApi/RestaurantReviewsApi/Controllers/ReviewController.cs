using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IReviewManager _manager;

        public ReviewController(ILogger<ReviewController> logger, IReviewManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ReviewApiModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReviewAsync(Guid reviewId)
        {
            try
            {
                var model = await _manager.GetReviewAsync(reviewId);

                if (model != null)
                    return Ok(model);

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(ICollection<ReviewApiModel>), 200)]
        public async Task<IActionResult> SearchReviewsAsync(ReviewSearchApiModel model)
        {
            try
            {
                var searchResult = await _manager.SearchReviewsAsync(model);
                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PostReviewAsync([FromBody] ReviewApiModel model)
        {
            try
            {
                var result = await _manager.PostReviewAsync(model);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReviewAsync(Guid reviewId)
        {
            try
            {
                var result = await _manager.DeleteReviewAsync(reviewId);
                if (result)
                    return Ok();

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }
    }
}
