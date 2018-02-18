using RestaurantRating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRating.Controllers
{
    public class ReviewsByUserController : ApiController
    {
        [HttpGet]
        public IEnumerable<Users> GetReviewsByUser([FromUri]string user)
        {
            IList<Users> us = null;

            using (var ctx = new RestaurantDBContext())
            {

                us = ctx.Users.Where(e => e.UserName == user).ToList<Users>();

            }
            if (us == null)
            {
                var message = string.Format("Review for user = {0} not found", user);
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            return us;

        }
    }
}
