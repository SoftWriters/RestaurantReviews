using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantReviews.Domain;
using RestaurantReviews.DataAccess;

namespace RestaurantReviews.Controllers
{
    public class ReviewController : ApiController
    {
        private readonly IRestaurantReviewData _restaurantReviewData;

        public ReviewController(IRestaurantReviewData restaurantReviewData)
        {
            this._restaurantReviewData = restaurantReviewData;
        }

        [HttpGet]
        [Route("api/Reviews/GetReviews")]
        public IHttpActionResult GetReviews(int userId)
        {
            var result = _restaurantReviewData.GetReviews(userId);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/Reviews/PostReview")]
        public IHttpActionResult PostReview(Review newReview)
        {
            var result = _restaurantReviewData.AddReview(newReview);
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/Reviews/DeleteReview")]
        public IHttpActionResult DeleteReview(int reviewId)
        {
            var result = _restaurantReviewData.DeleteReview(reviewId);
            return Ok(result);
        }

        
    }
}
