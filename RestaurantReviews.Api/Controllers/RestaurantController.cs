using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Restaurant>>> GetAllAsync()
        {
            return new List<Restaurant> {
                new Restaurant { Name = "McDonald's" },
                new Restaurant { Name = "Wendy's" },
                new Restaurant { Name = "Arby's" }
            };
        }
    }
}