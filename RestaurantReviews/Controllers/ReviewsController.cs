using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data;
using RestaurantReviews.Services;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return Ok(await _reviewService.GetAllReviews());
        }
        // GET: api/Reviews/5
        [HttpGet("byuser/{userId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByUserId(string userId)
        {
            return Ok(await _reviewService.GetAllByUserId(userId));
        }
        // GET: api/Reviews/5
        [HttpGet("byrestaurant/{id}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByRestaurant(int id)
        {
            return Ok(await _reviewService.GetAllByRestaurant(id));
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _reviewService.GetReview(id);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
                return BadRequest();

            _reviewService.UpdateReview(id,review);

            try
            {
                await _reviewService.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_reviewService.ReviewExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Reviews
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            _reviewService.CreateReview(review);
            await _reviewService.SaveChanges();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            var review = await _reviewService.GetReview(id);
            if (review == null)
            {
                return NotFound();
            }

            _reviewService.DeleteReview(review);
            await _reviewService.SaveChanges();

            return review;
        }

    }
}
