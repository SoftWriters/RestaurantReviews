using RestaurantReviews.Domain.Codes;
using RestaurantReviewsAPI.Authorization;
using RestaurantReviewsAPI.Controllers.Base;
using RestaurantReviewsAPI.Extensions;
using RestaurantReviewsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RestaurantReviewsAPI.Controllers
{
    [Authorize]
    [IdentityBasicAuthentication]
    [RoutePrefix("api/v1/reviews")]
    public class ReviewAPIController : BaseAPIController
    {
        [Route("{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(long id)
        {
            try
            {
                var currentUserId = CurrentUserId;
                if (currentUserId == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

                var service = ServiceFactory
                    .ReviewService(currentUserId.Value);

                var serviceResponse = await service
                    .DeleteReview(id);

                return serviceResponse.ToHttpResponse(Request);
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}