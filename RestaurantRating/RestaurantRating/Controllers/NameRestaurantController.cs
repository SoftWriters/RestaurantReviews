using RestaurantRating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRating.Controllers
{
    public class NameRestaurantController : ApiController
    {
        public IEnumerable<String> GetRestName()
        {

            IList<String> us = null;
            using (var ctx = new RestaurantDBContext())
            {
                us = ctx.Restaurants.Select(x => x.RestaurantName).Distinct().ToList<String>();

            }

            if (us == null)
            {
                var message = string.Format("Restaurant not found");
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            return us;
        }
    }
}
