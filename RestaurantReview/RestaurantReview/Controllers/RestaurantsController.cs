using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestaurantReview.DAL;
using RestaurantReview.Models;
using RestaurantReview.Services;

namespace RestaurantReview.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        public IConn connection;

        public RestaurantsController(IConn conn)
        {
            this.connection = conn;
        }

        // GET api/Restaurants
        [HttpGet("{city}")]
        public IActionResult Get(string city)
        {
            var dal = new RestaurantsDAL(connection.AWSconnstring()).GetRestaurants()
                                                              .FindAll(restaurant => restaurant.City.ToLower().Equals(city.ToLower()));
            if (dal.Count >= 1) { return Ok(dal); } else { return StatusCode(404, "There are no results for this city"); }

        }

        // POST api/Restaurants - must send in a restaurant body with it
        [HttpPost]
        public IActionResult Post([FromBody] Restaurant restaurant)
        {
            var dal = new RestaurantsDAL(connection.AWSconnstring()).PostRestaurant(restaurant);
            if (dal.IsSuccessful) { return (Ok(dal.toreturn)); } else { return StatusCode(304, dal.toreturn); }
        }
    }
}