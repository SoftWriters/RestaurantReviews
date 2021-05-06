using RestaurantReviewsAPI.Helpers;
using RestaurantReviewsAPI.Models.DataTransferObjects;
using RestaurantReviewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace RestaurantReviewsAPI.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantsController : ControllerBase
    {
        private readonly ILogger<RestaurantsController> _logger;
        private readonly AppDbContext _dbContext;

        public RestaurantsController(ILogger<RestaurantsController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get a list of restaurants by city.
        /// </summary>
        /// <remarks>GET: api/Restaurants?cityId={id}</remarks>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RestaurantInfoDTO>>> ReadRestaurants([FromQuery] long? cityId)
        {
            try
            {

                if (cityId == null)
                {
                    _logger.LogWarning("Missing City");
                    return BadRequest("ErrorCityRequired");
                }

                var FilterCity = await _dbContext.Cities.FindAsync(cityId.Value);
                if (FilterCity == null)
                {
                    _logger.LogWarning("Invalid City ({0})", cityId.Value);
                    return BadRequest("ErrorInvalidCity");
                }

                // making this a method as project scope may expand to include additional filters, etc
                var restaurants = await GetRestaurantsByCityAsync(cityId.Value);

                _logger.LogInformation("Restaurants returned for City ({0})", cityId.Value);

                // Success!
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed fetching Restaurants from City ({0})", cityId.Value);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<List<RestaurantInfoDTO>> GetRestaurantsByCityAsync(long cityId)
        {
            /* Fetching all non-deleted restaurants by City and converting into DTOs.*/
            return await _dbContext.Restaurants
                                    .Include(i => i.City) 
                                    .Where(w => w.City.Id == cityId)
                                    .Where(w => w.Deleted == false) // exclude deleted
                                    .OrderBy(ob => ob.Name)
                                    .Select(s => DTOHelper.ConvertToDTO(s)) // LINQ-to-Objects for DTO
                                    .ToListAsync();
        }

        /// <summary>
        /// Get restaurant by id.
        /// </summary>
        /// <remarks>GET: api/Restaurants/{0}</remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RestaurantInfoDTO>> ReadRestaurant(long id)
        {
            try
            {
                /* For this excercise, I will NOT return the object to this direct call if marked as deleted in db. If required to  
                 * include deleted, I'd alter the DTO class to include a Deleted boolean and Nullable<DeletedDT> properties */
                var Restaurant = await _dbContext.Restaurants.Include(i => i.City).FirstOrDefaultAsync(i => i.Id == id && i.Deleted == false);
                if (Restaurant == null)
                {
                    _logger.LogWarning("Invalid Restaurant ({0})", id);
                    return NotFound();
                }

                var RestaurantInfo = DTOHelper.ConvertToDTO(Restaurant);
                _logger.LogInformation("Restaurant ({0}) returned.", id);

                // Success!
                return Ok(RestaurantInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed fetching Restaurant ({0})'", id);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create new restaurant.
        /// </summary>
        /// <remarks>POST: api/Restaurants</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RestaurantInfoDTO>> CreateRestaurant([FromBody] RestaurantNewDTO newRestaurant)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid Model");
                    return BadRequest("ErrorMissingParams");
                }

                var RestaurantCity = await _dbContext.Cities.FindAsync(newRestaurant.CityId);
                if (RestaurantCity == null)
                {
                    _logger.LogWarning("Invalid City ({0})", newRestaurant.CityId);
                    return BadRequest("ErrorInvalidCity");
                }

                /* For this exercise, we are not allowing duplicate restaurant names within the same city */
                if(_dbContext.Restaurants.Where(w => w.Name == newRestaurant.Name).Where(w => w.City.Id == newRestaurant.CityId).Any())
                {
                    _logger.LogWarning("Attempted Duplicate Restaurant");
                    return BadRequest("ErrorDuplicateName");
                }

                // Create and populate from DTO
                var NewRestaurant = new Restaurant
                {
                    Name = newRestaurant.Name,
                    CityID = RestaurantCity.Id,
                    City = RestaurantCity
                };

                // Insert into table
                await _dbContext.Restaurants.AddAsync(NewRestaurant);

                // Save
                await _dbContext.SaveChangesAsync();

                // Convert to DTO, including the new generated Id
                var NewRestaurantInfo = DTOHelper.ConvertToDTO(NewRestaurant);

                _logger.LogInformation("Restaurant ({0}) created.", NewRestaurantInfo.RestaurantId);

                // Success!
                return CreatedAtAction(nameof(ReadRestaurant), new { id = NewRestaurantInfo.RestaurantId }, NewRestaurantInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed creating restaurant.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
