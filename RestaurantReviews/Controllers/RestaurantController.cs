using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RestaurantReviews.Api.Model;
using RestaurantReviews.Data;

namespace RestaurantReviews.Api.Controllers
{
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        [HttpGet("restaurant/{restaurantId}")]
        public IActionResult GetUserById(Guid restaurantId)
        {
            Restaurant restaurant = RestaurantDAO.GetRestaurantById(restaurantId);

            if (restaurant != null)
                return Ok(restaurant);
            else
                return NotFound();
        }

        [HttpGet("restaurant/list/{city}")]
        public IActionResult GetRestaurantsByCity(string city)
        {
            List<Restaurant> restaurants = RestaurantDAO.GetRestaurantsByCity(city);

            if (restaurants != null)
                return Ok(restaurants);
            else
                return NotFound();
        }

        [HttpPost("restaurant/add")]
        public IActionResult Add(Restaurant resturantIn)
        {
            Restaurant restaurant = RestaurantDAO.Add(resturantIn);

            if (restaurant != null)
                return Created(new Uri($"restaurant/create/{restaurant.RestaurantId}", UriKind.Relative), restaurant);
            else
                return Conflict("Already Exists");
        }

        [HttpPost("restaurant/create/{name}/{streetAddress}/{city}/{region}/{country}/{postalCode}")]
        public IActionResult Add(string name, string streetAddress, string city, string region, string country, string postalCode)
        {
            Restaurant restaurant = RestaurantDAO.Add(name, streetAddress, city, region, country, postalCode);

            if (restaurant != null)
                return Created(new Uri($"restaurant/create/{restaurant.RestaurantId}", UriKind.Relative), restaurant);
            else
                return Conflict("Already Exists");
        }

        [HttpDelete("restaurant/delete/{restaurantId}")]
        public IActionResult Delete(Guid restaurantId)
        {
            if (RestaurantDAO.Delete(restaurantId))
                return Ok();
            else
                return NotFound();
        }
    }
}