using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Results
{
    public class CreateActionResult : IActionResult
    {
        private readonly CreateResponse _result;

        internal CreateActionResult(CreateResponse result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result);
            if (_result.Status == CreateResponseStatus.ValidationError.ToString())
            {
                objectResult.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (_result.Status == CreateResponseStatus.Failure.ToString())
            {
                objectResult.StatusCode = StatusCodes.Status500InternalServerError;
            }

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
