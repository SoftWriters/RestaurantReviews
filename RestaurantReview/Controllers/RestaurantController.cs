using RestaurantReview.BusinessLogic.Controllers;
using RestaurantReview.BusinessLogic.Models;
using RestaurantReview.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantReview.Controllers
{
    [Authorize]
    [RoutePrefix("api/restaurants")]
    public class RestaurantController : BaseApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]RestaurantContext restaurant)
        {
            RestaurantLogic restaurantLogic = new RestaurantLogic();
            HttpStatusCode resultCode;
            string errorMessage;
            Restaurant result;
            if (restaurantLogic.TryAddRestaurant(restaurant, out resultCode, out errorMessage, out result))
            {
                return Ok(result);
            }

            else
            {
                return ReturnFailure(resultCode, errorMessage);
            }
        }

        [HttpGet]
        [Route("{restaurantID}")]
        public IHttpActionResult GetRestaurant(int restaurantID)
        {
            try
            {
                RestaurantLogic restaurantLogic = new RestaurantLogic();
                HttpStatusCode resultCode;
                string errorMessage;
                Restaurant result;

                if (restaurantLogic.TryGetRestaurant(restaurantID, out resultCode, out errorMessage, out result))
                {
                    return Ok(result);
                }

                else
                {
                    return ReturnFailure(resultCode, errorMessage);
                }
            }

            catch (Exception ex)
            {
                return ReturnTotalFailure();
            }
        }

        //[HttpGet]
        //[Route("{restaurantName}")]
        //public IHttpActionResult GetRestaurant(string restaurantName)
        //{
        //    try
        //    {
        //        RestaurantLogic restaurantLogic = new RestaurantLogic();
        //        HttpStatusCode resultCode;
        //        string errorMessage;
        //        Restaurant result;
        //        if (restaurantLogic.TryGetRestaurant(restaurantName, out resultCode, out errorMessage, out result))
        //        {
        //            return Ok(result);
        //        }

        //        else
        //        {
        //            return ReturnFailure(resultCode, errorMessage);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        return ReturnTotalFailure();
        //    }
        //}

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetRestaurants()
        {
            RestaurantLogic restaurantLogic = new RestaurantLogic();
            HttpStatusCode resultCode;
            string errorMessage;
            List<Restaurant> result;
            if (restaurantLogic.TryGetRestaurants(out resultCode, out errorMessage, out result))
            {
                return Ok(result);
            }

            else
            {
                return ReturnFailure(resultCode, errorMessage);
            }
        }

        [HttpPut]
        [Route("{restaurantID}")]
        public IHttpActionResult Put(int restaurantID, [FromBody]RestaurantContext restaurant)
        {
            RestaurantLogic restaurantLogic = new RestaurantLogic();
            HttpStatusCode resultCode;
            string errorMessage;
            Restaurant result;
            if (restaurantLogic.TryUpdateRestaurant(restaurantID, restaurant, out resultCode, out errorMessage, out result))
            {
                return Ok(result);
            }

            else
            {
                return ReturnFailure(resultCode, errorMessage);
            }
        }

        [HttpDelete]
        [Route("{restaurantID}")]
        public IHttpActionResult Delete(int restaurantID)
        {
            RestaurantLogic restaurantLogic = new RestaurantLogic();
            HttpStatusCode resultCode;
            string errorMessage;
            if (restaurantLogic.TryDeleteRestaurant(restaurantID, out resultCode, out errorMessage))
            {
                return Ok(restaurantID);
            }

            else
            {
                return ReturnFailure(resultCode, errorMessage);
            }
        }
    }
}
