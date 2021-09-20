using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace RestaurantReviews.Controllers.Api
{
    public class ReviewController : ApiController
    {
        // PUT api/Review
        [HttpPut]
        public int Put([FromBody]ReviewModel review)
        {
            review.UserID = SessionHelper.UserID;
            return ReviewManager.UpdateReview(review);
        }

        // DELETE api/Review
        [HttpDelete]
        public int Delete([FromBody]int reviewID)
        {
            return ReviewManager.DeleteReview(reviewID, SessionHelper.UserID);
        }
    }
}