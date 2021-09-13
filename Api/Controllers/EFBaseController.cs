using Microsoft.AspNetCore.Mvc;
using Softwriters.RestaurantReviews.Data;

namespace Softwriters.RestaurantReviews.Api.Controllers
{
    [ApiController]
    public abstract class EFBaseController<T1, TFilter> : ControllerBase
        where T1 : class, new()
        where TFilter : class, new()
    {
        private readonly DataContext _context;

        public EFBaseController(DataContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<T1>>> GetCities()
        //{
        //    return await _context.Cities.ToListAsync();
        //}

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<City>> GetCity(int id)
        //{
        //    var city = await _context.Cities.FindAsync(id);

        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    return city;
        //}

        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> PutCity(int id, City city)
        //{
        //    if (id != city.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(city).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CityExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //[HttpPost]
        //public async Task<ActionResult<City>> PostCity(City city)
        //{
        //    _context.Cities.Add(city);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
        //}

        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> DeleteCity(int id)
        //{
        //    var city = await _context.Cities.FindAsync(id);

        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Cities.Remove(city);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CityExists(long id)
        //{
        //    return _context.Cities.Any(e => e.Id == id);
        //}
        //}
    }
}