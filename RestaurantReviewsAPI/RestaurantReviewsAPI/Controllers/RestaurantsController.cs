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
        
        [Route("")]
        [HttpPost]
        [ValidateModel]
        public async Task<HttpResponseMessage> Post([FromBody]Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var service = ServiceFactory
                .RestaurantService;

            try
            {
                var serviceResponse = await service
                    .AddRestaurant(restaurant);

                if(serviceResponse.OpCode == OpCodes.InvalidOperation)
                    return Request.CreateResponse(ExtendedHttpStatusCodes.UnprocessableEntity, serviceResponse.Message);
                if(serviceResponse.OpCode == OpCodes.Success)
                    Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}