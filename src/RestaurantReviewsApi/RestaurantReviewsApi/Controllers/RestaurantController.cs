using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Bll.Utility;
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

        [HttpGet]
        [ProducesResponseType(typeof(RestaurantApiModel), 200)]
        [ProducesResponseType(404)]
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

        [HttpPost("search")]
        [ProducesResponseType(typeof(ICollection<RestaurantApiModel>), 200)]
        [ProducesResponseType(typeof(IList<string>), 400)]
        public async Task<IActionResult> SearchRestaurantsAsync([FromBody] RestaurantSearchApiModel model)
        {
            try
            {
                var validations = _restaurantSearchApiModelValidator.Validate(model);
                if (!validations.IsValid)
                    return BadRequest(ValidationHelper.FormatValidations(validations));

                var searchResult = await _manager.SearchRestaurantsAsync(model);
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
        [ProducesResponseType(typeof(IList<string>), 400)]
        public async Task<IActionResult> PostRestaurantAsync([FromBody] RestaurantApiModel model)
        {
            try
            {
                var validations = _restaurantApiModelValidator.Validate(model);
                if (!validations.IsValid)
                    return BadRequest(ValidationHelper.FormatValidations(validations));

                var result = await _manager.PostRestaurantAsync(model);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IList<string>), 400)]
        public async Task<IActionResult> PatchRestaurantAsync([FromBody] RestaurantApiModel model)
        {
            try
            {
                var validations = _restaurantApiModelValidator.Validate(model);
                if (!validations.IsValid)
                    return BadRequest(ValidationHelper.FormatValidations(validations));

                var result = await _manager.PatchRestaurantAsync(model);
                if(result)
                    return Ok();

                return NotFound();
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
