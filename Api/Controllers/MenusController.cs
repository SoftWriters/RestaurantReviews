using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Softwriters.RestaurantReviews.Data;
using Softwriters.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softwriters.RestaurantReviews.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusController : ControllerBase
    {
        private readonly DataContext _context;

        public MenusController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            return await _context.Menus.ToListAsync();
        }

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

        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu Menu)
        {
            _context.Menus.Add(Menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMenu), new { id = Menu.Id }, Menu);
        }

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

        private bool MenuExists(long id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}