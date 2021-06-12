using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantReviewsController : ControllerBase
    {
        private readonly ILogger<RestaurantReviewsController> _logger;

        public RestaurantReviewsController(ILogger<RestaurantReviewsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("addresses")]
        public IEnumerable<string> GetAddresses()
        {
            return new[] { "Hi" };
        }
    }
}
