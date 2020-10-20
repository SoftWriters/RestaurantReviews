using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Bll.Providers;
using RestaurantReviewsApi.Bll.Utility;
using RestaurantReviewsApi.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IReviewManager _manager;
        private readonly IValidator<ReviewApiModel> _reviewApiModelValidator;
        private readonly IValidator<ReviewSearchApiModel> _reviewSearchApiModelValidator;
        private readonly IAuthProvider _authProvider;

        public ReviewController(ILogger<ReviewController> logger, IReviewManager manager,
            IValidator<ReviewApiModel> reviewApiModelValidator, IValidator<ReviewSearchApiModel> reviewSearchApiModelValidator,
            IAuthProvider authProvider)
        {
            _logger = logger;
            _manager = manager;
            _reviewApiModelValidator = reviewApiModelValidator;
            _reviewSearchApiModelValidator = reviewSearchApiModelValidator;
            _authProvider = authProvider;
        }


        /// <summary>
        /// Gets a Review.
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns>ReviewApiModel</returns>
        [HttpDelete]
        [HttpGet]
        [ProducesResponseType(typeof(ReviewApiModel), 200)]
        [ProducesResponseType(404)]
        [Authorize(Policy = Policy.User)]
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

        /// <summary>
        /// Searches for Review based on parameters.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     
        ///     {
        ///        "username": "User12",
        ///        "restaurant_id": "7FFC0A18-03CB-4C55-98EC-7D1E42D714D0"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A list of reviews</returns>
        [HttpPost("search")]
        [ProducesResponseType(typeof(ICollection<ReviewApiModel>), 200)]
        [ProducesResponseType(typeof(IList<string>), 400)]
        [Authorize(Policy = Policy.User)]
        public async Task<IActionResult> SearchReviewsAsync(ReviewSearchApiModel model)
        {
            try
            {
                var validations = _reviewSearchApiModelValidator.Validate(model);
                if (!validations.IsValid)
                    return BadRequest(ValidationHelper.FormatValidations(validations));

                var searchResult = await _manager.SearchReviewsAsync(model);
                return Ok(searchResult);
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a Review.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     
        ///     {
        ///        "restaurant_id": "7FFC0A18-03CB-4C55-98EC-7D1E42D714D0",
        ///        "rating": 8,
        ///        "details": "Was very delicious"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Id of created Review</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid?), 200)]
        [ProducesResponseType(typeof(IList<string>), 400)]
        [Authorize(Policy = Policy.User)]
        public async Task<IActionResult> PostReviewAsync([FromBody] ReviewApiModel model)
        {
            try
            {
                var userModel = _authProvider.GetUserModel(Request);

                var validations = _reviewApiModelValidator.Validate(model);
                if (!validations.IsValid)
                    return BadRequest(ValidationHelper.FormatValidations(validations));

                var result = await _manager.PostReviewAsync(model, userModel);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes a review.
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Policy = Policy.User)]
        public async Task<IActionResult> DeleteReviewAsync(Guid reviewId)
        {
            try
            {
                var userModel = _authProvider.GetUserModel(Request);

                var result = await _manager.DeleteReviewAsync(reviewId, userModel);
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
