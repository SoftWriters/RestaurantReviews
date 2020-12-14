using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic;
using RestaurantReviews.Logic.Model.Restaurant.Post;
using RestaurantReviews.Logic.Model.Restaurant.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantLogic logic;

        public RestaurantController(IRestaurantLogic logic)
        {
            this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        }

        [HttpPost]
        [Route("query")]
        public async Task<ActionResult<RestaurantQueryResponse>> Query(RestaurantQueryRequest request)
        {
            var result = await logic.QueryRestaurant(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<PostRestaurantResponse>> Create(PostRestaurantRequest request)
        {
            var result = await logic.CreateRestaurant(request);
            return Ok(result);
        }
    }
}
