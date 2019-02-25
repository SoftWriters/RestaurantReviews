using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.API.Dtos;
using RestaurantReviews.Data.Contracts.Logging;
using RestaurantReviews.Data.Contracts.Repositories;
using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.Extensions;

namespace RestaurantReviews.API.Controllers.CRUD
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBaseRestaurantReviews
    {
        #region Constructors

        public UserController(ILoggerManager loggerManager, IMapper mapper, IRepositoryWrapper repositoryWrapper)
            : base(loggerManager, mapper, repositoryWrapper)
        {
        }

        #endregion Constructors

        #region Actions

        /// <summary>
        /// Gets all the users from the data repository
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _repositoryWrapper.User.GetAllUsers();
                _loggerManager.LogInfo($"Returned all users from database.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets a User by its unique Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _repositoryWrapper.User.GetUserById(id);
                if (user.IsEmptyObject())
                {
                    _loggerManager.LogError($"User with id: {id}, was not found in db.");
                    return NotFound();
                }
                else
                {
                    _loggerManager.LogInfo($"Returned user with id: {id}");
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Creates a user in the data respository. Unique by email address (ToDo)
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromQuery] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    _loggerManager.LogError("UserDto object sent from client is null.");
                    return BadRequest("UserDto model object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid school object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var user = _mapper.Map<User>(userDto);
                await _repositoryWrapper.User.CreateUser(user);
                return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates a User in the data respository, by its unique Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser{id}", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromQuery] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    _loggerManager.LogError("UserDto object sent from client is null.");
                    return BadRequest("UserDto model object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid UserDto object sent from client.");
                    return BadRequest("Invalid UserDto model object");
                }
                var dbUser = await _repositoryWrapper.User.GetUserById(id);
                if (dbUser.IsEmptyObject())
                {
                    _loggerManager.LogError($"User with id: {id}, wasn't been found in db.");
                    return NotFound();
                }
                var user = _mapper.Map<User>(userDto);
                await _repositoryWrapper.User.UpdateUser(dbUser, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a user from the data repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _repositoryWrapper.User.GetUserById(id);
                if (user.IsEmptyObject())
                {
                    _loggerManager.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                if ((await _repositoryWrapper.Review.GetReviewsByUser(id)).Any())
                {
                    _loggerManager.LogError($"Cannot delete user with id: {id}. It has related reviews. Delete those reviews first");
                    return BadRequest("Cannot delete user. It has related reviews. Delete those reviews first");
                }
                await _repositoryWrapper.User.DeleteUser(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion Actions
    }
}