using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReview.DAL;
using RestaurantReview.Models;

namespace RestaurantReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        // GET api/values
        [HttpGet("{city}")]
        public ActionResult<List<Restaurant>> Get(string city)
        {
            return new RestaurantsDAL().GetRestaurants()
                                       .FindAll(restaurant => restaurant.City.Equals(city));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Restaurant restaurant)
        {
            new RestaurantsDAL().PostRestaurant(restaurant);
        }
    }
}
