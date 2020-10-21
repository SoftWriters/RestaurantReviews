using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data;
using RestaurantReviews.Services;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return Ok(await _restaurantService.GetAllRestaurants());
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurant(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }
        // GET: api/Restaurants/ByCity/Pittsburgh
        [HttpGet("bycity/{city}")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantByCity(string city)
        {
            return Ok(_restaurantService.GetAllByCity(city));
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }
            _restaurantService.UpdateRestaurant(id, restaurant);

            try
            {
                await _restaurantService.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_restaurantService.RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Restaurants
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            _restaurantService.CreateRestaurant(restaurant);
               await _restaurantService.SaveChanges();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Restaurant>> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurant(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            _restaurantService.DeleteRestaurant(restaurant);
            await _restaurantService.SaveChanges();

            return restaurant;
        }

    }
}
