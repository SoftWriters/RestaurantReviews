using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantReviews.Data.Models;
using RestaurantReviews.Domain.Codes;
using RestaurantReviews.Domain.Const;
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
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string name = null, string stateCode = null, string stateName = null, string city = null, int skip = 0, int take = QueryConstants.DefaultPageSize)
        {
            try
            {
                var currentUserId = CurrentUserId;
                if (currentUserId == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

                var service = ServiceFactory
                    .RestaurantService(currentUserId.Value);

                var results = await service
                    .QueryRestaurants(name, stateCode, stateName, city, skip, take);

                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

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