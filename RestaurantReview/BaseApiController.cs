using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace RestaurantReview
{
    public class BaseApiController : ApiController
    {
        #region helpers

        public IHttpActionResult ReturnFailure(HttpStatusCode resultCode, string errorMessage)
        {
            switch (resultCode)
            {
                case HttpStatusCode.InternalServerError:
                    return InternalServerError(new Exception(errorMessage));

                case HttpStatusCode.BadRequest:
                    return BadRequest(errorMessage);

                default:
                    return InternalServerError(new Exception(errorMessage));
            }
        }

        public IHttpActionResult ReturnTotalFailure()
        {
            return InternalServerError(new Exception("Application error has occured. Please email \"correalf01@gmail.com\" with this request time for further support."));
        }

        #endregion
    }
}