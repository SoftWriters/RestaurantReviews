using RestaurantReviewsAPI.Authorization;
using RestaurantReviewsAPI.Controllers.Base;
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
    [RoutePrefix("api/v1/users")]
    public class UsersAPIController : BaseAPIController
    {
        /// <summary>
        /// Fetches a list of all the reviews written by a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("{userId}/reviews")]
        public async Task<HttpResponseMessage> GetReviews(long userId)
        {
            try
            {
                var currentUserId = CurrentUserId;
                if (currentUserId == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

                var service = ServiceFactory
                    .UserService(CurrentUserId.Value);

                var results = await service
                    .GetReviewsBy(userId);

                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}