using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Models;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly RestaurantContext _context;

        public RestaurantController(RestaurantContext context)
        {
            _context = context;

            if (_context.Restaurants.Count() == 0)
            {
                _context.Restaurants.Add(new Restaurant { Name = "Item1" });
                _context.SaveChanges();
            }
        }       
    }
}