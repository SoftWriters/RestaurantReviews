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
    public class CriticsController : ControllerBase
    {
        private readonly ReviewsContext _context;

        public CriticsController(ReviewsContext context)
        {
            _context = context;
        }

        // GET: api/Critics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Critic>>> GetCritics()
        {
            return await _context.Critics.ToListAsync();
        }

        // GET: api/Critics/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Critic>> GetCritic(int id)
        {
            var Critic = await _context.Critics.FindAsync(id);

            if (Critic == null)
            {
                return NotFound();
            }

            return Critic;
        }

        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Update
        // PUT: api/Critics/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCritic(int id, Critic critic)
        {
            if (id != critic.Id)
            {
                return BadRequest();
            }

            _context.Entry(critic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriticExists(id))
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
        // POST: api/Critics
        [HttpPost]
        public async Task<ActionResult<Critic>> PostCritic(Critic critic)
        {
            _context.Critics.Add(critic);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCritic), new { id = critic.Id }, critic);
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Critics/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCritic(int id)
        {
            var critic = await _context.Critics.FindAsync(id);

            if (critic == null)
            {
                return NotFound();
            }

            _context.Critics.Remove(critic);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool CriticExists(long id)
        {
            return _context.Critics.Any(e => e.Id == id);
        }
    }
}