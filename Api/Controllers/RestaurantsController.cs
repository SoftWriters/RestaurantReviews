using Microsoft.AspNetCore.Mvc;
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
    public class RestaurantsController : ControllerBase
    {
        private readonly ReviewsContext _context;

        public RestaurantsController(ReviewsContext context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return await _context.Restaurants.ToListAsync();
        }

        // GET: api/Restaurants/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var Restaurant = await _context.Restaurants.FindAsync(id);

            if (Restaurant == null)
            {
                return NotFound();
            }

            return Restaurant;
        }

        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Update
        // PUT: api/Restaurants/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant Restaurant)
        {
            if (id != Restaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(Restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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
        // POST: api/Restaurants
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant Restaurant)
        {
            _context.Restaurants.Add(Restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurant), new { id = Restaurant.Id }, Restaurant);
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Restaurants/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var Restaurant = await _context.Restaurants.FindAsync(id);

            if (Restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(Restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool RestaurantExists(long id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }
    }
}