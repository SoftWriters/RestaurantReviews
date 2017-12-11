using RestaurantRating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRating.Controllers
{
    public class RestaurantController : ApiController
    {

        public IEnumerable<String> GetRestaurant()
        {

            IList<String> us = null;
            using (var ctx = new RestaurantDBContext())
            {
                us = ctx.Restaurants.Select(m => m.City).Distinct().ToList<String>();

            }

            if (us == null)
            {
                var message = string.Format("City not found");
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            return us;
        }

        [HttpPost]
        public HttpResponseMessage PostRestaurant([FromBody]Restaurant rest)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (var ctx = new RestaurantDBContext())
                    {

                        ctx.Restaurants.Add(new Restaurant()
                        {
                            RestaurantName = rest.RestaurantName,
                            City = rest.City

                        });

                        ctx.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, rest);
                        message.Headers.Location = new Uri(Request.RequestUri + rest.RestaurantName);
                        return message;
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Model");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
       
    }
}
