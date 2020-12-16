using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Results
{
    public class SearchActionResult<T> : IActionResult
    {
        private readonly SearchResponse<T> _result;

        internal SearchActionResult(SearchResponse<T> result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result);
            if (_result.Status == SearchResponseStatus.ValidationError.ToString())
            {
                objectResult.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (_result.Status == SearchResponseStatus.Failure.ToString())
            {
                objectResult.StatusCode = StatusCodes.Status500InternalServerError;
            }

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
