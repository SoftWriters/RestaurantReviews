using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntReviews.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RestaurantReviewsController : ControllerBase
    {
        private static readonly string[] Cities = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<RestaurantReviewsController> _logger;

        public RestaurantReviewsController(ILogger<RestaurantReviewsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Cities[rng.Next(Cities.Length)]
            })
            .ToArray();
        }
    }
}
