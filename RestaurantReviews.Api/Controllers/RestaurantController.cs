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

        public RestaurantController(IRestaurantQuery restaurantQuery)
        {
            _restaurantQuery = restaurantQuery;
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
            
            return await _restaurantQuery.GetRestaurants(city, state);
        }
    }
}