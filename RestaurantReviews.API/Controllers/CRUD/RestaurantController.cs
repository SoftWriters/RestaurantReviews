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
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBaseRestaurantReviews
    {
        #region Constructors

        public RestaurantController(ILoggerManager loggerManager, IMapper mapper, IRepositoryWrapper repositoryWrapper)
            : base(loggerManager, mapper, repositoryWrapper)
        {
        }

        #endregion Constructors

        #region Actions

        /// <summary>
        /// Get all restaurants from the data repository
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            try
            {
                var schools = await _repositoryWrapper.Restaurant.GetAllRestaurants();
                _loggerManager.LogInfo($"Returned all schools from database.");
                return Ok(schools);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetAllSchools action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get a restaurant from the data repository by its unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetRestaurantById")]
        public async Task<IActionResult> GetRestaurantById(Guid id)
        {
            try
            {
                var restaurant = await _repositoryWrapper.Restaurant.GetRestaurantById(id);
                if (restaurant.IsEmptyObject())
                {
                    _loggerManager.LogError($"Restaurant with id: {id}, was not found in db.");
                    return NotFound();
                }
                else
                {
                    _loggerManager.LogInfo($"Returned restaurant with id: {id}");
                    return Ok(restaurant);
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetRestaurantById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Posts a restaurant to the data respository.  Name must be unique.
        /// </summary>
        /// <param name="restaurantDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromQuery] RestaurantDto restaurantDto)
        {
            try
            {
                if (restaurantDto == null)
                {
                    _loggerManager.LogError("Restaurant object sent from client is null.");
                    return BadRequest("Restaurant object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid RestaurantDto object sent from client.");
                    return BadRequest("Invalid RestaurantDto model object");
                }
                var restaurant = _mapper.Map<Restaurant>(restaurantDto);
                await _repositoryWrapper.Restaurant.CreateRestaurant(restaurant);
                return CreatedAtRoute("GetRestaurantById", new { id = restaurant.Id }, restaurant);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside CreateRestaurant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Update a restaurant stored in the data store.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restaurantDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateRestaurant{id}", Name = "UpdateRestaurant")]
        public async Task<IActionResult> UpdateRestaurant(Guid id, [FromQuery] RestaurantDto restaurantDto)
        {
            try
            {
                if (restaurantDto == null)
                {
                    _loggerManager.LogError("Restaurant object sent from client is null.");
                    return BadRequest("Restaurant object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var dbRestaurant = await _repositoryWrapper.Restaurant.GetRestaurantById(id);
                if (dbRestaurant.IsEmptyObject())
                {
                    _loggerManager.LogError($"Restaurant with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var restaurant = _mapper.Map<Restaurant>(restaurantDto);
                await _repositoryWrapper.Restaurant.UpdateRestaurant(dbRestaurant, restaurant);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside UpdateRestaurant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Delete a restuarant from the data respository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestauruant(Guid id)
        {
            try
            {
                var restaurant = await _repositoryWrapper.Restaurant.GetRestaurantById(id);
                if (restaurant.IsEmptyObject())
                {
                    _loggerManager.LogError($"Restaurant with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                if ((await _repositoryWrapper.Review.GetReviewsByRestaurant(id)).Any())
                {
                    _loggerManager.LogError($"Cannot delete restaurant with id: {id}. It has related reviews. Delete those reviews first");
                    return BadRequest("Cannot delete restaurant. It has related reviews. Delete those reviews first");
                }
                await _repositoryWrapper.Restaurant.DeleteRestaurant(restaurant);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside DeleteRestaurant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion Actions
    }
}