using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantReviews.Entities;
using RestaurantReviews.Entities.Logic;
using RestaurantReviews.API.Models;

namespace RestaurantReviews.API.Controllers
{
    /// <summary>
    /// Restaurant Controller
    /// </summary>
    public class RestaurantController : ApiController
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Retrieves a Restaurant instance.
        /// </summary>
        /// <param name="restaurantId">The ID of the Restaurant to retrieve.</param>
        /// <returns>The Restaurant instance with the matching ID.</returns>
        [HttpGet]
        [Route("restaurants/{restaurantId}")]
        public IHttpActionResult GetRestaurant(long restaurantId)
        {
            try
            {
                return Ok(RestaurantManager.GetRestaurant(restaurantId));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }
        /// <summary>
        /// Returns all Restaurants in a given city and region/state.
        /// </summary>
        /// <param name="city">City of interest.</param>
        /// <param name="region">Corresponding region of interest.</param>
        /// <returns>A list of Restaurants.</returns>
        [HttpGet]
        [Route("restaurants")]
        public IHttpActionResult GetRestaurantsByCityRegion(string city, string region)
        {
            try
            {
                return Ok(RestaurantManager.GetRestaurantsByCityRegion(city, region));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Creates a new Restaurant.
        /// </summary>
        /// <param name="restaurant">Contains the properties used to create an instance of a Restaurant.</param>
        /// <returns>The newly created Restaurant.</returns>
        [HttpPost]
        [Route("restaurants")]
        public IHttpActionResult CreateRestaurant(RestaurantModel restaurant)
        {
            try
            {
                return Ok(RestaurantManager.CreateRestaurant(restaurant.Name));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Updates an existing Restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the Restaurant to udpate.</param>
        /// <param name="restaurant">Contains the properties used to create an instance of a Restaurant.</param>
        /// <returns>An updated instance of a Restaurant.</returns>
        [HttpPut]
        [Route("restaurants/{restaurantId}")]
        public IHttpActionResult UpdateRestaurant(long restaurantId, RestaurantModel restaurant)
        {
            try
            {
                return Ok(RestaurantManager.UpdateRestaurant(restaurantId, restaurant.Name));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Deletes an existing Restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the Restaurant to delete.</param>
        /// <returns>200 OK if successful.</returns>
        [HttpDelete]
        [Route("restaurants/{restaurantId}")]
        public IHttpActionResult DeleteRestaurant(long restaurantId)
        {
            try
            {
                RestaurantManager.DeleteRestaurant(restaurantId);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }
    }
}