using RestaurantReviewsAPI.Helpers;
using RestaurantReviewsAPI.Models;
using RestaurantReviewsAPI.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace RestaurantReviewsAPI.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly ILogger<ReviewsController> _logger;
        private readonly AppDbContext _dbContext;

        public ReviewsController(ILogger<ReviewsController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get a list of reviews by user.
        /// </summary>
        /// <remarks>GET: api/Reviews?userId={id}</remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewInfoDTO>>> ReadReviews([FromQuery] long? userId)
        {
            try
            {
                if (userId == null)
                {
                    _logger.LogWarning("Missing User");
                    return BadRequest("ErrorUserRequired");
                }

                var MobileUser = await _dbContext.MobileUsers.FindAsync(userId.Value);
                if (MobileUser == null)
                {
                    _logger.LogWarning("Invalid User ({0})", userId.Value);
                    return BadRequest("ErrorInvalidUser");
                }

                List<ReviewInfoDTO> reviews = await GetReviewsByUserAsync(userId.Value);

                _logger.LogInformation("Returning Reviews of User ({0})", userId.Value);

                // Success!
                return Ok(reviews);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed fetching Reviews for User ({0})", userId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<List<ReviewInfoDTO>> GetReviewsByUserAsync(long userId)
        {
            return await _dbContext.Reviews
                                    .Include(i => i.MobileUser)
                                    .Include(i => i.Rating)
                                    .Include(i => i.Restaurant.City)
                                    .Where(w => w.MobileUser.Id == userId)
                                    .Where(w => w.Deleted == false)
                                    .OrderByDescending(ob => ob.CreateDT)
                                    .Select(s => DTOHelper.ConvertToDTO(s))
                                    .ToListAsync();
        }

        /// <summary>
        /// Get review by id.
        /// </summary>
        /// <remarks>GET: api/Reviews/{0}</remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewInfoDTO>> ReadReview(long id)
        {
            try
            {
                /* For this excercise, I will NOT return the object to this direct call if marked as deleted in db. If required to  
                 * include deleted, I'd alter the DTO class to include a Deleted boolean and Nullable<DeletedDT> properties */
                var Review = await _dbContext.Reviews
                                                .Include(i => i.MobileUser)
                                                .Include(i => i.Rating)
                                                .Include(i => i.Restaurant)
                                                .Include(i => i.Restaurant.City)
                                                .FirstOrDefaultAsync(i => i.Id == id && i.Deleted == false);

                if (Review == null)
                {
                    _logger.LogWarning("Invalid Review ({0})", id);
                    return NotFound();
                }

                ReviewInfoDTO dtoReviewInfo = DTOHelper.ConvertToDTO(Review);
                _logger.LogInformation("Returning Review ({0})", id);

                // Success!
                return Ok(dtoReviewInfo);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed fetching Review", id);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create new review.
        /// </summary>
        /// <remarks>POST: api/Reviews</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewInfoDTO>> CreateReview([FromBody] ReviewNewDTO newReview)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid Model");
                    return BadRequest("ErrorMissingParams");
                }

                var ReviewRating = await _dbContext.Ratings.FindAsync(newReview.RatingId); 
                if (ReviewRating == null)
                {
                    _logger.LogWarning("Invalid Rating ({0})", newReview.RatingId);
                    return BadRequest("ErrorInvalidRating");
                }

                var ReviewMobileUser = await _dbContext.MobileUsers.FindAsync(newReview.UserId);
                if (ReviewMobileUser == null)
                {
                    _logger.LogWarning("Invalid User ({0})", newReview.UserId);
                    return BadRequest("ErrorInvalidUser");
                }

                var ReviewRestaurant = await _dbContext.Restaurants.Include(i => i.City).FirstOrDefaultAsync(i => i.Id == newReview.RestaurantId && i.Deleted == false);
                if (ReviewRestaurant == null)
                {
                    _logger.LogWarning("Invalid Restaurant ({0})", newReview.RestaurantId);
                    return BadRequest("ErrorInvalidRestaurant");
                }

                /* I am only allowing each user to post one review per restaurant */
                if (_dbContext.Reviews.Where(w => w.RestaurantID == newReview.RestaurantId && w.MobileUserID == newReview.UserId && w.Deleted == false).Any())
                {
                    _logger.LogWarning("Attempted second review.");
                    return BadRequest("ErrorOnlyOneReview");
                }
                   
                var NewReview = new Review
                {
                    RatingId = newReview.RatingId,
                    Rating = ReviewRating,
                    Comment = newReview.Comment,
                    RestaurantID = newReview.RestaurantId,
                    Restaurant = ReviewRestaurant,
                    MobileUserID = newReview.UserId,
                    MobileUser = ReviewMobileUser
                };

                await _dbContext.Reviews.AddAsync(NewReview);
                await _dbContext.SaveChangesAsync();

                ReviewInfoDTO dtoReviewInfo = DTOHelper.ConvertToDTO(NewReview);

                _logger.LogInformation("Created Review ({0})", dtoReviewInfo.ReviewId);

                // Success!
                return CreatedAtAction(nameof(ReadReview), new { id = dtoReviewInfo.ReviewId }, dtoReviewInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed creating review.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete review by id.
        /// </summary>
        /// <remarks>DELETE: api/Reviews/{0}</remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReview(long id)
        {
            try
            {
                var review = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id && r.Deleted == false);
                if (review == null)
                {
                    _logger.LogWarning("Invalid or Deleted Review ({0})", id);
                    return NotFound();
                }

                review.Deleted = true;
                review.DeleteDT = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Deleted Review ({0})", id);

                // Success!
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed deleting Review {0}", id);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
    }
}
