using RestaurantReviews.Domain.Codes;
using RestaurantReviews.Domain.Models;
using RestaurantReviewsAPI.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace RestaurantReviewsAPI.Extensions
{
    public static class HttpRequestMessageExts
    {
        public static HttpResponseMessage ToHttpResponse(this OperationResponse serviceResponse, HttpRequestMessage Request)
        {
            if (serviceResponse.OpCode == OpCodes.InsufficientPermissions)
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            if (serviceResponse.OpCode == OpCodes.ResourceNotFound)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            if (serviceResponse.OpCode == OpCodes.InvalidOperation)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            if (serviceResponse.OpCode == OpCodes.UnauthorizedOperation)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            if (serviceResponse.OpCode == OpCodes.Success)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }
    }
}