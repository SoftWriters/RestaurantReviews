using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Api.DataAccess;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantQuery _restaurantQuery;
        private readonly IInsertRestaurant _insertRestaurant;

        public RestaurantController(IRestaurantQuery restaurantQuery,
            IInsertRestaurant insertRestaurant)
        {
            _restaurantQuery = restaurantQuery;
            _insertRestaurant = insertRestaurant;
        }
        
        /// <summary>
        /// Returns a list of restaurants for the given city and state. If not provided,
        /// gives the entire list of restaurants we have reviews for.
        /// </summary>
        /// <param name="city">The City where the Restaurant is located</param>
        /// <param name="state">The State where the Restaurant is located</param>
        /// <returns>
        /// A collection of Restaurant objects matching the given city and state.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Restaurant>>> GetListAsync(string city=null, 
            string state=null)
        {
            if ((city == null || state == null ) && city != state)
            {
                return BadRequest("If city or state is provided, both must be given.");
            }
            
            return Ok(await _restaurantQuery.GetRestaurants(city, state));
        }
        
        /// <summary>
        /// Returns the restaurant at the given internal identifier.
        /// </summary>
        /// <param name="id">The internal identifier of the restaurant.</param>
        /// <returns>The Restaurant</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Restaurant>> GetAsync(long id)
        {
            if (id <= 0)
            {
                return BadRequest("id must be greater than 0");
            }
            
            var restaurant = await _restaurantQuery.GetRestaurant(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }
        
        /// <summary>
        /// Adds a new restaurant, if it does not exist.
        /// </summary>
        /// <param name="restaurant">The restaurant to add.</param>
        /// <returns>The restaurant, with the internal Id added.</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Restaurant>> PostAsync(NewRestaurant restaurant)
        {
            var existing = await _restaurantQuery.GetRestaurant(restaurant.Name,
                restaurant.City, restaurant.State);
            if (existing != null)
            {
                return Conflict(
                    $"A restaurant called {existing.Name} in {existing.City}, {existing.State} already exists as Id={existing.Id}.");
            }

            var id = await _insertRestaurant.Insert(restaurant);

            return Created(nameof(GetAsync), Restaurant.FromNew(id, restaurant));
        }
    }
}