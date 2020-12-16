using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic;
using RestaurantReviews.Logic.Model;
using RestaurantReviews.Logic.Model.Review.Create;
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

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CreateResponse), 200)]
        [ProducesResponseType(typeof(CreateResponse), 400)]
        [ProducesResponseType(typeof(CreateResponse), 409)]
        public async Task<CreateActionResult> Create(CreateReviewRequest request)
        {
            var result = await logic.CreateReview(request);
            return new CreateActionResult(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(CreateResponse), 200)]
        [ProducesResponseType(typeof(CreateResponse), 400)]
        public async Task<DeleteActionResult> Delete(string id)
        {
            var result = await logic.DeleteReview(new Logic.Model.Review.Delete.DeleteReviewRequest() { ReviewId = id });
            return new DeleteActionResult(result);
        }
    }
}
