using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Infrastructure;
using RestaurantReviews.Domain;
using RestaurantReviews.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantReviews.API.Controllers
{
    /**
    *
    *1. Get a list of restaurants by city
    2. Post a restaurant that is not in the database
    3. Post a review for a restaurant
    4. Get of a list of reviews by user
    5. Delete a review
    */
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {

        private readonly IReviewRepository _reviewRepository;
        private readonly IIdentityService _identityService;

        public ReviewsController(IReviewRepository reviewRepository, IIdentityService identityService)
        {
            _reviewRepository = reviewRepository;
            _identityService = identityService;
        }

        [Route("gerreviewsbyuser/{userid}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewsByUser(Guid userId)
        {
            Guid userIdToGet = userId == Guid.Empty ? _identityService.GetUserIdentity() : userId;

            var user = await _reviewRepository.FindUserAsync(userIdToGet);
            if (user == null)
                return NotFound();

            return (new List<Review>(await _reviewRepository.GetReviewsByUserAsync(userIdToGet)).ConvertAll(a=>Assembler.MapToDTO(a)));

        }


        [Route("searchusers/{searchstring}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> SearchUsers(string searchString)
        {
            return (new List<User>(await _reviewRepository.SearchUsersAsync(searchString)).ConvertAll(a => Assembler.MapToDTO(a)));
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> AddReview([FromBody] ReviewDTO reviewDTO)
        {
            if (reviewDTO is null)
            {
                return BadRequest();
            }

            var review = Assembler.MapToModel(reviewDTO);
            review.CreatedUserId = _identityService.GetUserIdentity();
            await _reviewRepository.AddAsync(review);

            return CreatedAtAction(nameof(AddReview), new { id = review.ReviewId }, null);

        }

        [HttpDelete("{reviewId:int}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteReview(Guid reviewId)
        {

            if (reviewId == Guid.Empty)
            {
                return BadRequest();
            }

            var reviewToDelete = await _reviewRepository.FindAsync(reviewId);

            if (reviewToDelete is null)
            {
                return NotFound();
            }

            await _reviewRepository.DeleteAsync(reviewId);
            return NoContent();
        }
    }
}