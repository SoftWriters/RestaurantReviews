using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Interfaces.Business;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantManager _restaurantManager;

        public RestaurantController(IRestaurantManager restaurantManager)
        {
            _restaurantManager = restaurantManager;
        }

        // GET api/restaurants
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> Get()
        {
            return Ok(_restaurantManager.GetAll());
        }

        // GET api/restaurants/5
        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get(int id)
        {
            return Ok(_restaurantManager.GetById(id));
        }

        // POST api/restaurants
        [HttpPost]
        public void Post([FromBody] Restaurant restaurant)
        {
            _restaurantManager.Create(restaurant);
        }
    }
}
