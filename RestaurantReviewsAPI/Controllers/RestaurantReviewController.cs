using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RestaurantReviewsAPI.Models;

namespace RestaurantReviewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantReviewController : ControllerBase
    {
        private readonly ILogger<RestaurantReviewController> _logger;

        public RestaurantReviewController(ILogger<RestaurantReviewController> logger)
        {
            _logger = logger;
        }

        private readonly RestaurantReviewContext _context;

        public RestaurantReviewController(RestaurantReviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantReview>>> GetRestaurantReviews()
        {
            return await _context.RestaurantReviewItems
                .Select(x => x)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public IEnumerable<RestaurantReview> GetRestaurantReview(){
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantReview>>> GetRestaurantReviewsByUser(string user)
        {
            return await _context.RestaurantReviewItems
                .Select(x => x)
                .Where(u => u.PostedByUser == user) 
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantReview>> PostRestaurantReview(RestaurantReview restaurantReview)
        {
            
            _context.RestaurantReviewItems.Add(restaurantReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurantReviews), new { id = restaurantReview.Id }, restaurantReview);
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantReview>> CreateRestaurantReview(RestaurantReview newRestaurantReview)
        {
            var restaurantReview = new RestaurantReview
            {
                DatePosted = DateTime.Now,
                City = newRestaurantReview.City,
                State = newRestaurantReview.State,
                Review = newRestaurantReview.Review,
                PostedByUser = newRestaurantReview.PostedByUser
            };

            _context.RestaurantReviewItems.Add(restaurantReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRestaurantReview),
                new { id = restaurantReview.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(Guid id, RestaurantReview restaurantReview)
        {
            if (id != restaurantReview.Id)
            {
                return BadRequest();
            }

            var restaurantReviewUpdate = await _context.RestaurantReviewItems.FindAsync(id);
            if (restaurantReviewUpdate == null)
            {
                return NotFound();
            }

            restaurantReviewUpdate.City = restaurantReview.City;
            restaurantReviewUpdate.State = restaurantReview.State;
            restaurantReviewUpdate.Review = restaurantReview.Review;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!RestaurantReviewExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool RestaurantReviewExists(Guid id) =>
            _context.RestaurantReviewItems.Any(e => e.Id == id);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantReview(long id)
        {
            var restaurantReview = await _context.RestaurantReviewItems.FindAsync(id);
            if (restaurantReview == null)
            {
                return NotFound();
            }

            _context.RestaurantReviewItems.Remove(restaurantReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
