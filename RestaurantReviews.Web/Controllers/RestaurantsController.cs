using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReviews.Web.Data.Entities;
using RestaurantReviews.Web.Models;
using RestaurantReviews.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRestaurantRepository _repository;

        public RestaurantsController(ILogger<RestaurantsController> logger, IRestaurantRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRestaurantsAsync([FromQuery]string city)
        {
            try
            {
                var restaurants = await _repository.GetRestaurantsAsync(city);

                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRestaurantAsync([FromBody] RestaurantPostRequest request)
        {
            try
            {
                var restaurant = await _repository.CreateRestaurantAsync(request.Name, request.Address, request.City, request.State, request.ZipCode);
                
                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
