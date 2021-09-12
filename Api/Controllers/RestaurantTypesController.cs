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
    public class RestaurantTypesController : ControllerBase
    {
        private readonly ReviewsContext _context;

        public RestaurantTypesController(ReviewsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantType>>> GetRestaurantTypes()
        {
            return await _context.RestaurantTypes.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RestaurantType>> GetRestaurantType(int id)
        {
            var RestaurantType = await _context.RestaurantTypes.FindAsync(id);

            if (RestaurantType == null)
            {
                return NotFound();
            }

            return RestaurantType;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRestaurantType(int id, RestaurantType RestaurantType)
        {
            if (id != RestaurantType.Id)
            {
                return BadRequest();
            }

            _context.Entry(RestaurantType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantTypeExists(id))
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

        [HttpPost]
        public async Task<ActionResult<RestaurantType>> PostRestaurantType(RestaurantType RestaurantType)
        {
            _context.RestaurantTypes.Add(RestaurantType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurantType), new { id = RestaurantType.Id }, RestaurantType);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRestaurantType(int id)
        {
            var RestaurantType = await _context.RestaurantTypes.FindAsync(id);

            if (RestaurantType == null)
            {
                return NotFound();
            }

            _context.RestaurantTypes.Remove(RestaurantType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantTypeExists(long id)
        {
            return _context.RestaurantTypes.Any(e => e.Id == id);
        }
    }
}