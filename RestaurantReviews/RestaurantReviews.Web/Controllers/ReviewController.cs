using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic;
using RestaurantReviews.Logic.Model.Review.Query;
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
        [Route("query")]
        public async Task<ActionResult<ReviewQueryResponse>> Query(ReviewQueryRequest request)
        {
            var result = await logic.QueryReview(request);
            return Ok(result);
        }
    }
}
