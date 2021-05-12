using System;
using System.Threading.Tasks;
using DTOs;
using DTOs.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestaurantReviewsAPI.Controllers
{
    public abstract class AbstractContoller : ControllerBase
    {
      protected async Task<IActionResult> toHttpResponse(Func<Task> serviceCall, ILogger logger) {
        try
        {
          await serviceCall.Invoke();
          return Ok();
        }
        catch (ValidationException ex) { 
          logger.LogError(ex, "Payload Received was Invalid");
          return UnprocessableEntity(new BasicResponse { Status = "Failure: Payload Invalid", FailureMessage = ex.Message });
        }
        catch (Exception ex)
        {
          logger.LogError(ex, "Error While Proccessing Request");
          return StatusCode(500);
        }
      }

      protected async Task<IActionResult> toHttpResponseWithPayload<T>(Func<Task<T>> serviceCall, ILogger logger) {
        try {
          var result = await serviceCall.Invoke();
          return Ok(result);
        }
        catch (ValidationException ex) {
          logger.LogError(ex, "Payload Received was Invalid");
          return UnprocessableEntity(new BasicResponse { Status = "Failure: Payload Invalid", FailureMessage = ex.Message });
        }
        catch (Exception ex)
        {
          logger.LogError(ex, "Error While Proccessing Request");
          return StatusCode(500);
        }
      }
    }
}