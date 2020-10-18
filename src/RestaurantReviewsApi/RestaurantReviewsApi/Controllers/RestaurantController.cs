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
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IRestaurantManager _manager;

        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(RestaurantApiModel), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetRestaurant(Guid restaurantId)
        {
            return Ok();
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(ICollection<RestaurantApiModel>), 200)]
        public IActionResult SearchRestaurants([FromBody] RestaurantSearchApiModel model)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult PostRestaurant([FromBody] RestaurantApiModel model)
        {
            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(200)]
        public IActionResult PatchRestaurant([FromBody] RestaurantApiModel model)
        {
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRestaurant(Guid restaurantId)
        {
            return Ok();
        }
    }
}
