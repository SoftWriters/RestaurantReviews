
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantReviews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("restaurants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAsync()
        {
            var restaurants = await _unitOfWork.Restaurants.ListAllAsync();
            // Return Status Code 200
            return Ok(restaurants);
        }

        [HttpPost]
        [Route("restaurants")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Restaurant>> PostAsync([FromBody] RestaurantDTO dto)
        {
            if (dto == null)
            {
                // Return Status Code 400
                return BadRequest("Missing Restaurant DTO");
            }

            var restaurant = await _unitOfWork.Restaurants.CreateAsync(dto);
            if (restaurant == null)
            {
                // Return Status Code 400
                return BadRequest("Unable to Create Restaurant");
            }
            await _unitOfWork.CompleteAsync();

            // Return Status Code 201
            return Created("/restaurants/" + restaurant.RestaurantID, restaurant);
        }

        [HttpGet]
        [Route("restaurants/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAsync(long id)
        {
            var restaurant = await _unitOfWork.Restaurants.ReadAsync(id);
            if (restaurant == null)
            {
                // Return Status Code 404
                return NotFound("No Record Found for Restaurant ID " + id);
            }

            // Return Status Code 200
            return Ok(restaurant);
        }

        [HttpGet]
        [Route("restaurants/search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetRestaurantsByCity([FromQuery(Name = "city")] string city)
        {
            var reviews = await _unitOfWork.Restaurants.ListRestaurantsByCityAsync(city);

            // Return Status Code 200
            return Ok(reviews);
        }
    }
}
