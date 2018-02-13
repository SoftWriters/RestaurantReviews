using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantReviews.Data.Models;
using RestaurantReviews.Data.Models.Domain;
using RestaurantReviews.Domain.Codes;
using RestaurantReviewsAPI.Const;
using RestaurantReviewsAPI.Services;

namespace RestaurantReviewsAPI.Controllers
{
    [RoutePrefix("api/v1/restaurants")]
    public class RestaurantsController : ApiController
    {
        private readonly ServiceFactory _serviceFactory;

        public RestaurantsController()
        {
            _serviceFactory = new ServiceFactory();
        }
        
        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]Restaurant restaurant)
        {
            var service = _serviceFactory
                .RestaurantService;

            try
            {
                var serviceResponse = await service
                    .AddRestaurant(restaurant);

                if(serviceResponse.OpCode == OpCodes.InvalidOperation)
                    return Request.CreateResponse(ExtendedHttpStatusCodes.UnprocessableEntity, serviceResponse.ValidationErrors);
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