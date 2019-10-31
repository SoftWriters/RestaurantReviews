using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestaurantReview.DAL;
using RestaurantReview.Models;
using RestaurantReview.Services;
using System;

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
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception();
            }
            catch
            {

            }
            var dal = new RestaurantsDAL(connection.AWSconnstring()).GetRestaurants();
            if (dal.Count >= 1) { return Ok(dal); } else { return NotFound("There are no results for this city"); }
        }

        // GET api/Restaurants
        [HttpGet("{city}")]
        public IActionResult Get(string city)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception();
            }
            catch
            {

            }
            var dal = new RestaurantsDAL(connection.AWSconnstring()).GetRestaurants()
                                                            .FindAll(restaurant => restaurant.City.ToLower().Equals(city.ToLower()));
            if (dal.Count >= 1) { return Ok(dal); } else { return NotFound("There are no results for this city"); }
        }

        // POST api/Restaurants - must send in a restaurant body with it
        [HttpPost]
        public IActionResult Post([FromBody] Restaurant restaurant)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception();
            }
            catch
            {

            }
            var dal = new RestaurantsDAL(connection.AWSconnstring()).PostRestaurant(restaurant);
            if (dal.IsSuccessful) { return (Ok(dal.toreturn)); } else { return StatusCode(404, dal.toreturn); }
        }
    }
}