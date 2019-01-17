using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace RestaurantReviews.Web.Api.ExceptionHandling
{
    public class DuplicateKeyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is SqlException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "Duplicate item"
                };
                throw new HttpResponseException(resp);
            }
        }

    }
}