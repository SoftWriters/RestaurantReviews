using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReview.Models;

namespace RestaurantReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<List<Restaurant>> Get()
        {
            return new List<Restaurant>()
            {
               new Restaurant{
                   RestaurantId = 1,
                   City = "brooklyn",
                   Name = "Martys"
               },
               new Restaurant{
                   RestaurantId = 2,
                   City = "brooklyn",
                   Name = "Martys"
               },
               new Restaurant{
                   RestaurantId = 3,
                   City = "chicago",
                   Name = "Martys"
               },
               new Restaurant{
                   RestaurantId = 4,
                   City = "boston",
                   Name = "Martys"
               },
               new Restaurant{
                   RestaurantId = 5,
                   City = "pittsburgh",
                   Name = "Martys"
               },
               new Restaurant{
                   RestaurantId = 6,
                   City = "chicago",
                   Name = "Martys"
               },
               new Restaurant{
                   RestaurantId = 7,
                   City = "boston",
                   Name = "Martys"
               },
               new Restaurant{
                   RestaurantId = 8,
                   City = "chicago",
                   Name = "Martys"
               },
               new Restaurant{
                   RestaurantId = 9,
                   City = "pittsburgh",
                   Name = "Martys"
               }
            }.FindAll(restaurant => restaurant.City.Equals("brooklyn"));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
