using RestaurantRating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRating.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<String> GetUsers()
        {

            IList<String> us = null;
            using (var ctx = new RestaurantDBContext())
            {
                us = ctx.Users.Select(x => x.UserName).Distinct().ToList<String>();

            }

            if (us == null)
            {
                var message = string.Format("User not found");
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            return us;
        }



        [HttpPost]
        public HttpResponseMessage PostReview([FromBody]Users us)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (var ctx = new RestaurantDBContext())
                    {
                        ctx.Users.Add(us);
                        ctx.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, us);
                        message.Headers.Location = new Uri(Request.RequestUri + us.Id.ToString());
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


        public HttpResponseMessage DeleteReview(int id)
        {
            try
            {
                using (var ctx = new RestaurantDBContext())
                {
                    Users us = ctx.Users.SingleOrDefault(b => b.Id == id);

                    if (us == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with id =" + id.ToString() + "not found to delete");
                    }
                    else
                    {
                        ctx.Users.Remove(us);
                        ctx.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
