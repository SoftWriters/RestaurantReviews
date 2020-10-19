using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
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
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IRestaurantManager _manager;
        private readonly IValidator<RestaurantApiModel> _restaurantApiModelValidator;
        private readonly IValidator<RestaurantSearchApiModel> _restaurantSearchApiModelValidator;

        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantManager manager,
            IValidator<RestaurantApiModel> restaurantApiModelValidator, IValidator<RestaurantSearchApiModel> restaurantSearchApiModelValidator)
        {
            _logger = logger;
            _manager = manager;
            _restaurantApiModelValidator = restaurantApiModelValidator;
            _restaurantSearchApiModelValidator = restaurantSearchApiModelValidator;
        }

        /// <summary>
        /// Gets a restaurant.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns>RestaurantApiModel</returns>
        [HttpGet]
        [ProducesResponseType(typeof(RestaurantApiModel), 200)]
        [ProducesResponseType(404)]
        [Authorize(Policy = Policy.User)]
        public async Task<IActionResult> GetRestaurantAsync(Guid restaurantId)
        {
            try
            {
                var model = await _manager.GetRestaurantAsync(restaurantId);

                if (model != null)
                    return Ok(model);

                return NotFound();
            }
            catch(Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Searches for Restaurants based on parameters.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     
        ///     {
        ///        "name": "Fioris",
        ///        "city": "Pittsburgh",
        ///        "state": "PA"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A list of restaurants</returns>
        [HttpPost("search")]
        [ProducesResponseType(typeof(ICollection<RestaurantApiModel>), 200)]
        [ProducesResponseType(typeof(IList<string>), 400)]
        [Authorize(Policy = Policy.User)]
        public async Task<IActionResult> SearchRestaurantsAsync([FromBody] RestaurantSearchApiModel model)
        {
            try
            {
                var validations = _restaurantSearchApiModelValidator.Validate(model);
                if (!validations.IsValid)
                    return BadRequest(ValidationHelper.FormatValidations(validations));

                var searchResult = await _manager.SearchRestaurantsAsync(model);
                return Ok(searchResult);
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a Restaurant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     
        ///     {
        ///        "name": "Fioris",
        ///        "city": "Pittsburgh",
        ///        "state": "PA",
        ///        "zipCode": "15216",
        ///        "description": "Delicious Pizza"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Id of created Restaurant</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid?), 200)]
        [ProducesResponseType(typeof(IList<string>), 400)]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> PostRestaurantAsync([FromBody] RestaurantApiModel model)
        {
            try
            {
                var validations = _restaurantApiModelValidator.Validate(model);
                if (!validations.IsValid)
                    return BadRequest(ValidationHelper.FormatValidations(validations));

                return Ok(await _manager.PostRestaurantAsync(model));
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Patches a Restaurant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     
        ///     {
        ///        "restaurant_id": "7FFC0A18-03CB-4C55-98EC-7D1E42D714D0""
        ///        "name": "Fioris",
        ///        "city": "Pittsburgh",
        ///        "state": "PA",
        ///        "zipcode": "15216",
        ///        "description": "Delicious Pizza"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Id of patched Restaurant</returns>
        [HttpPatch]
        [ProducesResponseType(typeof(Guid?), 200)]
        [ProducesResponseType(typeof(IList<string>), 400)]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> PatchRestaurantAsync([FromBody] RestaurantApiModel model)
        {
            try
            {
                var validations = _restaurantApiModelValidator.Validate(model);
                if (!validations.IsValid)
                    return BadRequest(ValidationHelper.FormatValidations(validations));

                var result = await _manager.PatchRestaurantAsync(model);
                if(result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes a Restaurant.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> DeleteRestaurantAsync(Guid restaurantId)
        {
            try
            {
                var result = await _manager.DeleteRestaurantAsync(restaurantId);
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
