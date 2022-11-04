using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantReviews.Repositories;
using RestaurantReviews.WebServices.Helpers;
using RestaurantReviews.WebServices.Models;

namespace RestaurantReviews.WebServices.Controllers
{
    public class ReviewsController : ApiController
    {
        private IReviewRepository reviewRepository;
        private const int pageSize = 10;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<HttpResponseMessage> DeleteReview(int id)
        {
            await reviewRepository.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<IHttpActionResult> GetReview(int id)
        {
            var review = await reviewRepository.GetReview(id);

            if (review == null)
                return NotFound();

            return Ok(Parser.EntityToModel(review));
        }

        public async Task<IHttpActionResult> PostReview(ReviewModel review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await reviewRepository.Add(Parser.ModelToEntity(review));

            return CreatedAtRoute("MyApi", new { id }, Parser.EntityToModel(await reviewRepository.GetReview(id)));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                reviewRepository.Dispose();

            base.Dispose(disposing);
        }

    }
}