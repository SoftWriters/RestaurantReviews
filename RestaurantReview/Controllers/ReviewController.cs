using RestaurantReview;
using RestaurantReview.BusinessLogic.Models;
using RestaurantReview.Data.Entities;
using reviewReview.BusinessLogic.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReviewReview.Controllers
{
    [Authorize]
    [RoutePrefix("api/reviews")]
    public class ReviewController : BaseApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]ReviewContext review)
        {
            try
            {
                ReviewLogic reviewLogic = new ReviewLogic();
                HttpStatusCode resultCode;
                string errorMessage;
                Review result;
                if (reviewLogic.TryAddReview(review, out resultCode, out errorMessage, out result))
                {
                    return Ok(result);
                }

                else
                {
                    return ReturnFailure(resultCode, errorMessage);
                }
            }

            catch (Exception ex)
            {
                return ReturnTotalFailure();
            }
        }

        [HttpGet]
        [Route("{reviewID}")]
        public IHttpActionResult GetReview(int reviewID)
        {
            try
            {
                ReviewLogic reviewLogic = new ReviewLogic();
                HttpStatusCode resultCode;
                string errorMessage;
                Review result;
                if (reviewLogic.TryGetReview(reviewID, out resultCode, out errorMessage, out result))
                {
                    return Ok(result);
                }

                else
                {
                    return ReturnFailure(resultCode, errorMessage);
                }
            }

            catch (Exception ex)
            {
                return ReturnTotalFailure();
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetReviews()
        {
            try
            {
                ReviewLogic reviewLogic = new ReviewLogic();
                HttpStatusCode resultCode;
                string errorMessage;
                List<Review> result;
                if (reviewLogic.TryGetReviews(out resultCode, out errorMessage, out result))
                {
                    return Ok(result);
                }

                else
                {
                    return ReturnFailure(resultCode, errorMessage);
                }
            }

            catch (Exception ex)
            {
                return ReturnTotalFailure();
            }
        }

        [HttpPut]
        [Route("{reviewID}")]
        public IHttpActionResult Put(int reviewID, [FromBody]ReviewContext review)
        {
            try
            {
                ReviewLogic reviewLogic = new ReviewLogic();
                HttpStatusCode resultCode;
                string errorMessage;
                Review result;
                if (reviewLogic.TryUpdateReview(reviewID, review, out resultCode, out errorMessage, out result))
                {
                    return Ok(result);
                }

                else
                {
                    return ReturnFailure(resultCode, errorMessage);
                }
            }

            catch (Exception ex)
            {
                return ReturnTotalFailure();
            }
        }

        [HttpDelete]
        [Route("{reviewID}")]
        public IHttpActionResult Delete(int reviewID)
        {
            try
            {
                ReviewLogic reviewLogic = new ReviewLogic();
                HttpStatusCode resultCode;
                string errorMessage;
                if (reviewLogic.TryDeleteReview(reviewID, out resultCode, out errorMessage))
                {
                    return Ok(reviewID);
                }

                else
                {
                    return ReturnFailure(resultCode, errorMessage);
                }

            }

            catch (Exception ex)
            {
                return ReturnTotalFailure();
            }
        }
    }
}
