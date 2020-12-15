using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic;
using RestaurantReviews.Logic.Model;
using RestaurantReviews.Logic.Model.Restaurant.Create;
using RestaurantReviews.Logic.Model.Restaurant.Search;
using RestaurantReviews.Web.Results;
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
        [Route("search")]
        public async Task<SearchActionResult<SearchRestaurant>> Search(SearchRestaurantRequest request)
        {
            var result = await logic.SearchRestaurants(request);
            return new SearchActionResult<SearchRestaurant>(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<CreateResponse>> Create(CreateRestaurantRequest request)
        {
            var result = await logic.CreateRestaurant(request);
            return Ok(result);
        }
    }
}
