using System;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Models;
using RestaurantReviews.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;

        public UserController(ILogger<RestaurantController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <remarks>This method adds a user to the database. This was created for testing purposes. No constraints on the table. </remarks>
        /// <param name="user"></param>
        /// <returns>HttpStatusCode 201 when the user is created. </returns>
        /// <response code="201">The user has been successfully created.</response>
        [Route("Add")]
        [ProducesResponseType(typeof(Restaurant), 201)]
        [HttpPost]
        public IActionResult AddNewUser([FromBody]User user)
        {
            try
            {
                UserFacade facade = new UserFacade();
                facade.AddUser(user);
                return StatusCode(201, "User successfully created. ");
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest("Invalid Request. ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Please contact support for additional information. ");
            }
        }
    }
}
