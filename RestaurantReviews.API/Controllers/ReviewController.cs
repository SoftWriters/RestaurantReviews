using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Interfaces.Factories;
using RestaurantReviews.Interfaces.Repository;
using RestaurantReviews.Models;

namespace RestaurantReviews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _repository;

        public ReviewController(IDataFactory factory)
        {
            _repository = factory.ReviewRepo;
        }

        // GET api/restaurants
        [HttpGet]
        public ActionResult<IEnumerable<Review>> Get()
        {
            return Ok(_repository.GetAll());
        }

        // GET api/restaurants/5
        [HttpGet("{id}")]
        public ActionResult<Review> Get(int id)
        {
            return Ok(_repository.GetById(id));
        }

        // GET api/restaurants/user/5
        [HttpGet("user/{userId}")]
        public ActionResult<Restaurant> GetByUserId(int userId)
        {
            return Ok(_repository.GetByUserId(userId));
        }

        // POST api/restaurants
        [HttpPost]
        public void Post([FromBody] Review review)
        {
            _repository.Create(review);
        }

        // PUT api/restaurants/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Review review)
        {
        }

        // DELETE api/restaurants/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
