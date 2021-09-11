using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softwriters.RestaurantReviews.Models;
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
            return new List<Review>()
            {
                new() {Date = DateTime.Now, Description = "Awesome", Stars = 5},
                new() { Date = DateTime.Now.AddMonths(-1), Description = "OK", Stars = 3},
                new() { Date = DateTime.Now.AddMonths(-2), Description = "Lousy", Stars = 1}
            };
        }
    }
}
