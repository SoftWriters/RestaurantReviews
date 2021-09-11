﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Softwriters.RestaurantReviews.Data.DataContext;
using Softwriters.RestaurantReviews.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softwriters.RestaurantReviews.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewsContext _context;

        public ReviewsController(ReviewsContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        // GET: api/Reviews/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var Review = await _context.Reviews.FindAsync(id);

            if (Review == null)
            {
                return NotFound();
            }

            return Review;
        }

        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Update
        // PUT: api/Reviews/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutReview(int id, Review Review)
        {
            if (id != Review.Id)
            {
                return BadRequest();
            }

            _context.Entry(Review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Create
        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review Review)
        {
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = Review.Id }, Review);
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Reviews/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var Review = await _context.Reviews.FindAsync(id);

            if (Review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(Review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool ReviewExists(long id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}