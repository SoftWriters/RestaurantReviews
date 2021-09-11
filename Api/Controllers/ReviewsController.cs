using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softwriters.RestaurantReviews.Api.Models;
using System;
using System.Collections.Generic;

namespace Softwriters.RestaurantReviews.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(ILogger<ReviewsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Review> Get()
        {
            var reviews = new List<Review>();
            var r1 = new Review { Date = DateTime.Now, Description = "Awesome" };
            var r2 = new Review { Date = DateTime.Now.AddMonths(-1), Description = "OK" };
            var r3 = new Review { Date = DateTime.Now.AddMonths(-2), Description = "Lousy" };

            reviews.Add(r1);
            reviews.Add(r2);
            reviews.Add(r3);

            return reviews;
        }
    }
}
