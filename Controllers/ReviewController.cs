
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantReviews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAsync()
        {
            var reviews = await _unitOfWork.Reviews.ListAllAsync();
            // Return Status Code 200
            return Ok(reviews);
        }

        [HttpPost]
        [Route("reviews")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Review>> PostAsync([FromBody] ReviewDTO dto)
        {
            if (dto == null)
            {
                // Return Status Code 400
                return BadRequest("Missing Review DTO");
            }

            var review = await _unitOfWork.Reviews.CreateAsync(dto);
            if (review == null)
            {
                // Return Status Code 400
                return BadRequest("Unable to Create Review");
            }
            await _unitOfWork.CompleteAsync();

            // Return Status Code 201
            return Created("/reviews/" + review.ReviewID, review);
        }

        [HttpGet]
        [Route("reviews/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAsync(long id)
        {
            var review = await _unitOfWork.Reviews.ReadAsync(id);
            if (review == null)
            {
                // Return Status Code 404
                return NotFound("No Record Found for Review ID " + id);
            }

            // Return Status Code 200
            return Ok(review);
        }

        [HttpDelete]
        [Route("reviews/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            var review = await _unitOfWork.Reviews.DeleteAsync(id);
            if (review == null)
            {
                // Return Status Code 404
                return NotFound("No Record Found for Review ID " + id);
            }
            await _unitOfWork.CompleteAsync();

            // Return Status Code 204
            return new NoContentResult();
        }

        [HttpGet]
        [Route("reviews/search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetReviewsByUser([FromQuery(Name = "userid")] long userId)
        {
            var reviews = await _unitOfWork.Reviews.ListReviewsByUserAsync(userId);

            // Return Status Code 200
            return Ok(reviews);
        }
    }
}
