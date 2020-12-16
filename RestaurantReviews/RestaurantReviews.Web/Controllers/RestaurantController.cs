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

        /// <summary>
        /// Searches for restaurants based on the specified criteria
        /// </summary>
        [HttpPost]
        [Route("search")]
        [ProducesResponseType(typeof(SearchResponse<SearchRestaurant>), 200)]
        public async Task<SearchActionResult<SearchRestaurant>> Search(SearchRestaurantRequest request)
        {
            var result = await logic.SearchRestaurants(request);
            return new SearchActionResult<SearchRestaurant>(result);
        }

        /// <summary>
        /// Creates a new restaurant
        /// </summary>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CreateResponse), 200)]
        [ProducesResponseType(typeof(CreateResponse), 400)]
        [ProducesResponseType(typeof(CreateResponse), 409)]
        public async Task<CreateActionResult> Create(CreateRestaurantRequest request)
        {
            var result = await logic.CreateRestaurant(request);
            return new CreateActionResult(result);
        }
    }
}
