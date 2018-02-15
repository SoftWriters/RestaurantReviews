using RestaurantReviewsAPI.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RestaurantReviewsAPI.Controllers.Base
{
    public class BaseAPIController : ApiController
    {
        protected long? CurrentUserId { get { return RequestContext.Principal.Identity.GetUserId(); } }
    }
}