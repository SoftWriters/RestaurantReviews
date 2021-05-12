using System.Threading.Tasks;
using DTOs;
using DTOs.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace RestaurantReviewsAPI.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : AbstractContoller
    {
      private readonly IReviewRepo _reviewRepository;
      private readonly ILogger<ReviewController> _logger;
      private readonly IValidator<ReviewDTO> _payloadValidator;

    public ReviewController(IReviewRepo reviewRepo, ILogger<ReviewController> logger)
      {
        _reviewRepository = reviewRepo;
        _logger = logger;
        _payloadValidator = new ReviewValidator();
      } 

      [HttpGet]
      public async Task<IActionResult> getReviews([FromQuery] long? userId, [FromQuery] long? restaurantId, [FromQuery] bool sortLowToHigh)
      {
        if(!userId.HasValue && !restaurantId.HasValue)
        {
          return BadRequest(
            new BasicResponse { 
              Status = "Bad Request",
              FailureMessage = "Must provide either the userId or restaurantId or both in the query parameters"
            }
          );
        }

        return await toHttpResponseWithPayload(() => _reviewRepository.GetReviews(userId,restaurantId,sortLowToHigh), _logger);
      }

      [Route("add")]
      [HttpPost]
      public async Task<IActionResult> AddReview([FromBody] ReviewDTO newReview)
      {
        Task validateThenAdd()
        {
          _payloadValidator.ValidateData(newReview);
          return _reviewRepository.AddReview(newReview);
        }

        return await toHttpResponse(() => validateThenAdd(), _logger);
      }
      
      [Route("delete/{reviewId}")]
      [HttpDelete]
      public async Task<IActionResult> DeleteReview(long reviewId)
      {
        var result = await _reviewRepository.DeleteReview(reviewId);

        return result
          ? StatusCode(200, new BasicResponse { Status = "Success" })
          : StatusCode(404, new BasicResponse { Status = "Failure", FailureMessage=string.Format("Review with id {0} does not exist", reviewId)});
      }
    }
}