using RestaurantReviews.Common;
using RestaurantReviews.Domain;
using RestaurantReviews.Entity;
using RestaurantReviews.Web.Api.Models;
using RestaurantReviews.Web.Api.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantReviews.Web.Api.Controllers
{
    [SimpleBearerTokenAuthFilterAttribute]
    public class ReviewsController : ApiController
    {
        private IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        /// <summary>
        /// Search reviews with a filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        // POST: api/Reviews
        [Route("api/Reviews/Searches")]
        public Task<IEnumerable<Review>> Post([FromBody]FilterParam filter, int? page = 1, int? pagesize = 2)
        {
            return _reviewRepository.GetReviewsAsync(page??1, pagesize??1000, filter?.ToDbFilter<Review>());
        }

        /// <summary>
        /// Get review by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Reviews/5
        public Task<Review> Get(int id)
        {
            return _reviewRepository.GetReviewAsync(id);
        }

        /// <summary>
        /// Create review
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Reviews
        public Task<Review> Post([FromBody] Review value)
        {
            return _reviewRepository.CreateReviewAsync(value.RestaurantId, value.Heading, value.Content, value.Rating);
        }

        /// <summary>
        /// Delete review by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Reviews/5
        public Task Delete(int id)
        {
            return _reviewRepository.DeleteReviewAsync(id);
        }

        /// <summary>
        /// Get reviews created by user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [Route("api/Users/{userId}/Reviews")]
        public Task<IEnumerable<Review>> Get(int userId, int? page, int? pagesize)
        {
            var filter = new DbFilter<Review>() { Field = "UserId", Operator = OperatorEnum.Equal, Value = userId };
            return _reviewRepository.GetReviewsAsync(page ?? 1, pagesize ?? 1000, filter);
        }
    }
}
