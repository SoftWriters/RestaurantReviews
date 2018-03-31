using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantReviews.Entities;
using RestaurantReviews.Entities.Logic;
using RestaurantReviews.API.Models;
using RestaurantReviews.Entities.Data;

namespace RestaurantReviews.API.Controllers
{
    public class RestaurantAddressController : ApiController
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets a specific address for a restaurant
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="restaurantaddressId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("restaurants/{restaurantId}/addresses/{restaurantaddressId}")]
        public IHttpActionResult GetRestaurantAddress(long restaurantId, long restaurantaddressId)
        {
            try
            {
                return Ok(RestaurantAddressManager.GetRestaurantAddress(restaurantId, restaurantaddressId));
            }
            catch(RetrievalException rex)
            {
                logger.Error(rex);
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Gets all addresses for a restaurant.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("restaurants/{restaurantId}/addresses")]
        public IHttpActionResult GetRestaurantAddresses(long restaurantId)
        {
            try
            {
                return Ok(RestaurantAddressManager.GetRestaurantAddresses(restaurantId));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Creates an address for a restaurant.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("restaurants/{restaurantId}/addresses")]
        public IHttpActionResult CreateRestaurantAddress(long restaurantId, RestaurantAddressModel address)
        {
            try
            {
                return Ok(RestaurantAddressManager.CreateRestaurantAddress(restaurantId, address.Street1, address.Street2, address.City, address.Region, address.PostalCode));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Updates a specific address for a restaurant.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="restaurantaddressId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("restaurants/{restaurantId}/addresses/{restaurantaddressId}")]
        public IHttpActionResult UpdateRestaurantAddress(long restaurantId, long restaurantaddressId, RestaurantAddressModel address)
        {
            try
            {
                return Ok(RestaurantAddressManager.UpdateRestaurantAddress(restaurantId, restaurantaddressId, address.Street1, address.Street2, address.City, address.Region, address.PostalCode));
            }
            catch (PersistanceException pex)
            {
                logger.Error(pex);
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Deletes an address from a restaurant.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="restaurantaddressId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("restaurants/{restaurantId}/addresses/{restaurantaddressId}")]
        public IHttpActionResult DeleteRestaurantAddress(long restaurantId, long restaurantaddressId)
        {
            try
            {
                RestaurantAddressManager.DeleteRestaurantAddress(restaurantId, restaurantaddressId);
                return Ok();
            }
            catch(PersistanceException pex)
            {
                logger.Error(pex);
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }
    }
}
