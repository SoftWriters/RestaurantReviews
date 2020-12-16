using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Results
{
    public class DeleteActionResult : IActionResult
    {
        private readonly DeleteResponse _result;

        internal DeleteActionResult(DeleteResponse result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result)
            {
                StatusCode = StatusCodes.Status200OK
            };

            if (_result.Status == DeleteResponseStatus.ValidationError)
            {
                objectResult.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (_result.Status == DeleteResponseStatus.NotFound)
            {
                // Note:  My personal believe is that 404 signals a configuration error, so I use 400 to indicate that the specified resource was not found
                objectResult.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (_result.Status == DeleteResponseStatus.Failure)
            {
                objectResult.StatusCode = StatusCodes.Status500InternalServerError;
            }

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
