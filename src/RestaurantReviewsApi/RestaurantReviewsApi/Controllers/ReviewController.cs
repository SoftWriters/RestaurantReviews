using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReviewsApi.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(ILogger<ReviewController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ReviewApiModel), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetReview(Guid reviewId)
        {
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ReviewApiModel>), 200)]
        public IActionResult GetReviews(Guid? restaurantId, string userName)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult PostReview([FromBody] ReviewApiModel model)
        {
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(Guid reviewId)
        {
            return Ok();
        }
    }
}
