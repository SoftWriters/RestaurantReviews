using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantReviews.Data.Models;
using RestaurantReviews.Domain.Codes;
using RestaurantReviewsAPI.Attributes;
using RestaurantReviewsAPI.Authorization;
using RestaurantReviewsAPI.Const;
using RestaurantReviewsAPI.Services;

namespace RestaurantReviewsAPI.Controllers
{
    [Authorize]
    [IdentityBasicAuthentication]
    [RoutePrefix("api/v1/restaurants")]
    public class RestaurantsController : ApiController
    {
        protected long? CurrentUserId { get { return RequestContext.Principal.Identity.GetUserId(); } }

        [Route("")]
        [HttpPost]
        [ValidateModel]
        public async Task<HttpResponseMessage> Post([FromBody]Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var currentUserId = CurrentUserId;
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var service = ServiceFactory
                .RestaurantService(currentUserId.Value);

            try
            {
                var serviceResponse = await service
                    .AddRestaurant(restaurant);

                if(serviceResponse.OpCode == OpCodes.InvalidOperation)
                    return Request.CreateResponse(ExtendedHttpStatusCodes.UnprocessableEntity, serviceResponse.Message);
                if(serviceResponse.OpCode == OpCodes.Success)
                    return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [Route("{restaurantid}/reviews")]
        public async Task<HttpResponseMessage> Post(long restaurantid, [FromBody]Review review)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var currentUserId = CurrentUserId;
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            try
            {
                var service = ServiceFactory
                    .RestaurantService(currentUserId.Value);
                var serviceResponse = await service
                    .AddReviewToRestaurant(restaurantid, review);

                if (serviceResponse.OpCode == OpCodes.InvalidOperation)
                    return Request.CreateResponse(ExtendedHttpStatusCodes.UnprocessableEntity, serviceResponse.Message);
                if (serviceResponse.OpCode == OpCodes.Success)
                    return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}