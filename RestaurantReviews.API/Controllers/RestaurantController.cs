using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Interfaces.Factories;
using RestaurantReviews.Interfaces.Repository;
using RestaurantReviews.Models;

namespace RestaurantReviews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _repository;

        public RestaurantController(IDataFactory factory)
        {
            _repository = factory.RestaurantRepo;
        }

        // GET api/restaurants
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> Get()
        {
            return Ok(_repository.GetAll());
        }

        // GET api/restaurants/5
        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get(int id)
        {
            return Ok(_repository.GetById(id));
        }

        // POST api/restaurants
        [HttpPost]
        public void Post([FromBody] Restaurant restaurant)
        {
            _repository.Create(restaurant);
        }
    }
}
