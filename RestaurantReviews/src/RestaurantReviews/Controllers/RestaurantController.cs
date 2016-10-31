using System;
using System.Linq;
using RestaurantReviews.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using RestaurantReviews.Services;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(ILogger<RestaurantController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns restaurants within a certain city, zip code or a radius of certain coordinates. 
        /// </summary>
        /// <remarks></remarks>
        /// <param name="request"></param>
        /// <returns>List of restaurants within a certain area.</returns>
        /// <response code="200">The request was successful. </response>
        /// <response code="404">No restaurants match the search criteria supplied. </response>
        [HttpGet]
        [ProducesResponseType(typeof(Restaurant), 200)]
        [ProducesResponseType(typeof(Restaurant), 404)]
        [Route("GetByCity")]
        [Route("GetByZip")]
        public IActionResult GetByAddressProperties([FromUri]RestaurantRequest request)
        {
            try
            {
                _logger.LogInformation("Request received for restaurants by address properties. ");
                var restaurants = new RestaurantFacade().GetAllRestaurantsByAddress(request).ToList();
                if (restaurants.Count == 0 || restaurants == null)
                {
                    return NotFound("No restaurants match your search criteria. ");
                }
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Please contact support for additional information. ");
            }
        }

        /// <summary>
        /// Add a restaurant to the database.  
        /// </summary>
        /// <remarks>If the restaurant is added to the database http status code 201 is returned. 
        /// If the restaurant already exists http status code 409 (conflict) is returned.</remarks>
        /// <param name="restaurant">Constructed Restaurant.</param>
        /// <returns>HttpStatusCode.Created(201) if the restaurant is successfully created.</returns>
        /// <response code="201">The restaurant has been succesfully created. </response>
        /// <response code="409">The restaurant record already exists. </response>
        [HttpPost]
        [ProducesResponseType(typeof(Restaurant), 201)]
        [ProducesResponseType(typeof(Restaurant), 409)]
        [Route("Add")]
        public IActionResult AddRestaurant([FromBody]Restaurant restaurant)
        {
            try
            {
                _logger.LogInformation($"Add method called on the Restaurant controller. Name of restuarant: {restaurant.Name} city: {restaurant.ContactInformation.Address.City} zipcode: {restaurant.ContactInformation.Address.ZipCode}");
                RestaurantFacade facade = new RestaurantFacade();
                var existingRestaurant = facade.GetExistingRestaurant(restaurant);
                if (existingRestaurant != null)
                {
                    return StatusCode(409, existingRestaurant);
                }
                facade.AddRestaurant(restaurant);
                return StatusCode(201, "Restaurant successfully added to the database.");
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest($"Invalid request.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Please contact support for additional information. ");
            }
        }
    }
}