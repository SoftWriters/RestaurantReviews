using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Models;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantsController : Controller
    {
        private readonly RestaurantReviewContext context_;

        public RestaurantsController(RestaurantReviewContext context)
        {
            context_ = context;

            if (context_.Restaurants.Count() == 0)
            {
                context_.Restaurants.Add(new Restaurant { Name = "Item1" });
                context_.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Restaurant> Get()
        {
            return (from rest in context_.Restaurants 
                select rest).ToArray();
        }

        [HttpGet("{id}")]
        public IEnumerable<Restaurant> Get(long id)
        {
            return (from rest in context_.Restaurants 
                where rest.RestaurantID == id 
                select rest).ToArray();
        }

        [HttpGet("{city}")]
        public IEnumerable<Restaurant> Get(string city)
        {
            return (from rest in context_.Restaurants 
                where rest.City == city 
                select rest).ToArray();
        }
    }
}