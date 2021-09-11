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
    public class MenusController : ControllerBase
    {
        private readonly ReviewsContext _context;

        public MenusController(ReviewsContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            return await _context.Menus.ToListAsync();
        }

        // GET: api/Menus/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var Menu = await _context.Menus.FindAsync(id);

            if (Menu == null)
            {
                return NotFound();
            }

            return Menu;
        }

        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Update
        // PUT: api/Menus/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutMenu(int id, Menu Menu)
        {
            if (id != Menu.Id)
            {
                return BadRequest();
            }

            _context.Entry(Menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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
        // POST: api/Menus
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu Menu)
        {
            _context.Menus.Add(Menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMenu), new { id = Menu.Id }, Menu);
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Menus/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var Menu = await _context.Menus.FindAsync(id);

            if (Menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(Menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool MenuExists(long id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}