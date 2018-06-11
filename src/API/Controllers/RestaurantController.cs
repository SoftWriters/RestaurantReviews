/******************************************************************************
 * Name: RestaurantController.cs
 * Purpose: API operations on Restaurant(s)
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Service;
using RestaurantReviews.API.Model.DTO;

namespace RestaurantReviews.API.Controllers
{
    [Produces("application/json")]
    [Route("Restaurant")]
     public class RestaurantController : APIBaseController
    {
        IRestaurantService restaurantService = null;

        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        // GET: Restaurant/City?city=Pittsburgh
        [HttpGet("City", Name = "GetRestaurantByCity")]
        public APIResponseDTO Get(string city)
        {
            try
            {
                RestaurantModelList restraunts = this.restaurantService.GetRestaurants(city);
                return GetDataDTO(restraunts);
            }
            catch (Exception ex)
            {
                return GetErrorDTO(ex);
            }
        }

        // GET: Restaurant/5
        [HttpGet("{id}", Name = "GetRestaurant")]
        public APIResponseDTO Get(int id)
        {
            try
            {
                RestaurantModelDTO restaurant = this.restaurantService.GetRestaurantById(id);
                return GetDataDTO(restaurant);
            }
            catch (Exception ex)
            {
                return GetErrorDTO(ex);
            }
            
        }

        // POST: Restaurant
        [HttpPost]
        public APIResponseDTO Post([FromBody]RestaurantAPIRequestDTO request)
        {
            try
            {
                RestaurantModelDTO newRestaurant = request.Data;
                newRestaurant = this.restaurantService.AddRestaurant(newRestaurant);
                return GetDataDTO(newRestaurant);
            }
            catch (Exception ex)
            {
                return GetErrorDTO(ex);
            }
        }
    }
}
