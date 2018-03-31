using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantReviews.Entities;
using RestaurantReviews.Entities.Logic;
using RestaurantReviews.API.Models;
using RestaurantReviews.Entities.Data;

namespace RestaurantReviews.API.Controllers
{
    public class ReviewController : ApiController
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Creates a review for a restaurant.  Assumes that user is logged in and provides the user id in the Review model.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant to create the review for.</param>
        /// <param name="review">The Review data.</param>
        /// <returns>An instance of the Review that was created.</returns>
        [HttpPost]
        [Route("restaurants/{restaurantId}/reviews")]
        public IHttpActionResult CreateReview(long restaurantId, ReviewModel review)
        {
            try
            {
                return Ok(ReviewManager.CreateReview(restaurantId, review.MemberId, review.Body));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Retrieves reviews for a given Restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the Restaurant to retrieve Review instances for.</param>
        /// <returns>A collection of Reviews specific to the given Restaurant.</returns>
        [HttpGet]
        [Route("restaurants/{restaurantId}/reviews")]
        public IHttpActionResult GetRestaurantReviews(long restaurantId)
        {
            try
            {
                return Ok(ReviewManager.GetReviewsByRestaurant(restaurantId));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Retrieves reviews that were created by a specific Member.
        /// </summary>
        /// <param name="memberId">The ID of the Member to retrieve reviews for.</param>
        /// <returns>A collection of Reviews specific to the given Member.</returns>
        [HttpGet]
        [Route("members/{memberId}/reviews")]
        public IHttpActionResult GetMemberReviews(long memberId)
        {
            try
            {
                return Ok(ReviewManager.GetReviewsByMember(memberId));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Deletes a Review.
        /// </summary>
        /// <param name="reviewId">The ID of the Review to delete.</param>
        /// <returns>Ok if successful.</returns>
        [HttpDelete]
        [Route("reviews/{reviewId}")]
        public IHttpActionResult DeleteReview(long reviewId)
        {
            try
            {
                ReviewManager.DeleteReview(reviewId);
                return Ok();
            }
            catch (PersistanceException pex)
            {
                logger.Error(pex);
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }
    }
}
