using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic;
using RestaurantReviews.Logic.Model;
using RestaurantReviews.Logic.Model.Review.Search;
using RestaurantReviews.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IRestaurantLogic logic;

        public ReviewController(IRestaurantLogic logic)
        {
            this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        }

        [HttpPost]
        [Route("search")]
        [ProducesResponseType(typeof(SearchResponse<SearchReview>), 200)]
        public async Task<SearchActionResult<SearchReview>> Search(SearchReviewRequest request)
        {
            var result = await logic.SearchReviews(request);
            return new SearchActionResult<SearchReview>(result);
        }
    }
}
