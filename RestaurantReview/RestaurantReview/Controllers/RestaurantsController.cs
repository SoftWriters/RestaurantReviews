using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReview.DAL;
using RestaurantReview.Models;
using RestaurantReview.Services;

namespace RestaurantReview.Controllers
{
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
        public ActionResult<List<Restaurant>> Get(string city)
        {
            return new RestaurantsDAL(connection.AWSconnstring()).GetRestaurants()
                                                              .FindAll(restaurant => restaurant.City.Equals(city));
        }

        // POST api/Restaurants - must send in a restaurant body with it
        [HttpPost]
        public void Post([FromBody] Restaurant restaurant)
        {
            new RestaurantsDAL(connection.AWSconnstring()).PostRestaurant(restaurant);
        }
    }
}
