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

        // GET: api/RestaurantTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantType>>> GetRestaurantTypes()
        {
            return await _context.RestaurantTypes.ToListAsync();
        }

        // GET: api/RestaurantTypes/1
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

        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Update
        // PUT: api/RestaurantTypes/1
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
        #endregion

        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Create
        // POST: api/RestaurantTypes
        [HttpPost]
        public async Task<ActionResult<RestaurantType>> PostRestaurantType(RestaurantType RestaurantType)
        {
            _context.RestaurantTypes.Add(RestaurantType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurantType), new { id = RestaurantType.Id }, RestaurantType);
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/RestaurantTypes/1
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
        #endregion

        private bool RestaurantTypeExists(long id)
        {
            return _context.RestaurantTypes.Any(e => e.Id == id);
        }
    }
}