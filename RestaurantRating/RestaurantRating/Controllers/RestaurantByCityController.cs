using RestaurantRating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRating.Controllers
{
    public class RestaurantByCityController : ApiController
    {
        [HttpGet]
        public IEnumerable<Restaurant> GetRestaurantByCity([FromUri]string city)
        {
            IList<Restaurant> rest = null;

            using (var ctx = new RestaurantDBContext())
            {
                rest = ctx.Restaurants.Where(e => e.City == city).ToList<Restaurant>();
            }
            if (rest == null)
            {
                var message = string.Format("Restaurant in city = {0} not found", city);
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            return rest;

        }
    }
}
