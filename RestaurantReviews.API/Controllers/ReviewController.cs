using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Interfaces.Business;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewManager _reviewManager;

        public ReviewController(IReviewManager reviewManager)
        {
            _reviewManager = reviewManager;
        }

        // GET api/review
        [HttpGet]
        public ActionResult<IEnumerable<Review>> Get()
        {
            return Ok(_reviewManager.GetAll());
        }

        // GET api/review/5
        [HttpGet("{id}")]
        public ActionResult<Review> Get(int id)
        {
            return Ok(_reviewManager.GetById(id));
        }

        // GET api/review/user/5
        [HttpGet("user/{userId}")]
        public ActionResult<Review> GetByUserId(int userId)
        {
            return Ok(_reviewManager.GetByUserId(userId));
        }

        // POST api/review
        [HttpPost]
        public void Post([FromBody] Review review)
        {
            _reviewManager.Create(review);
        }

        // DELETE api/review/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _reviewManager.Delete(id);
        }
    }
}
