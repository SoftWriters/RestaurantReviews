using RestaurantReviews.Common;
using RestaurantReviews.Domain;
using RestaurantReviews.Entity;
using RestaurantReviews.Web.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding.Binders;

namespace RestaurantReviews.Web.Api.Controllers
{
    public class ReviewsController : AuthorizedBaseController
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
        public Task<IEnumerable<Review>> Post([FromBody]FilterParam filter,
            [FromUri(BinderType = typeof(TypeConverterModelBinder))] int? page,
            [FromUri(BinderType = typeof(TypeConverterModelBinder))] int? pagesize)
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
        public Task<IEnumerable<Review>> Get(int userId,
            [FromUri(BinderType = typeof(TypeConverterModelBinder))] int? page,
            [FromUri(BinderType = typeof(TypeConverterModelBinder))]int? pagesize)
        {
            var filter = new DbFilter<Review>() { Field = "UserId", Operator = OperatorEnum.Equal, Value = userId };
            return _reviewRepository.GetReviewsAsync(page ?? 1, pagesize ?? 1000, filter);
        }
    }
}
