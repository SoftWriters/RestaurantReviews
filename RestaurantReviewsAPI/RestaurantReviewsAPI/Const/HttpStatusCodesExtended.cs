using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RestaurantReviewsAPI.Const
{
    public static class ExtendedHttpStatusCodes
    {
        public const HttpStatusCode UnprocessableEntity = (HttpStatusCode) 422;
    }
}