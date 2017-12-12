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
        }

        [HttpGet]
        public IEnumerable<Restaurant> Get(string city = "") 
        {            
            if (!String.IsNullOrEmpty(city))
            {
                return (from rest in context_.Restaurants
                    where String.Equals(city, rest.City, StringComparison.OrdinalIgnoreCase)
                    select rest).ToArray();
            }
            else 
            {
                return (from rest in context_.Restaurants select rest).ToArray();
            }
        }

        [HttpGet("{id}")]
        public Restaurant Get(long id)
        {
            return (from rest in context_.Restaurants 
                where rest.RestaurantId == id 
                select rest).DefaultIfEmpty(null).SingleOrDefault();
        }

        // POST api/restaurants
        [HttpPost]
        public Restaurant Post([FromBody]Restaurant new_restaurant)
        {
            context_.Restaurants.Add(new_restaurant);
            context_.SaveChanges();
            return new_restaurant;
        }

        // PUT api/restaurants/5
        [HttpPut("{id}")]
        public Restaurant Put(int id, [FromBody]Restaurant update_restaurant)
        {
            var old_restaurant= (from restaurant in context_.Restaurants
                where restaurant.RestaurantId == id
                select restaurant).DefaultIfEmpty(null).SingleOrDefault();

            if (old_restaurant != null)
            {
                old_restaurant.Name = update_restaurant.Name;
                old_restaurant.City = update_restaurant.City;
                old_restaurant.Country = update_restaurant.Country;
                old_restaurant.Street = update_restaurant.Street;
                old_restaurant.State = update_restaurant.State;
                old_restaurant.Zip = update_restaurant.Zip;
                context_.Restaurants.Update(old_restaurant);
                context_.SaveChanges();
            }

            return old_restaurant;
        }

        // DELETE api/restaurants/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var to_delete = (from restaurant in context_.Restaurants
                where restaurant.RestaurantId == id
                select restaurant).DefaultIfEmpty(null).SingleOrDefault();

            if (to_delete != null)
            {
                context_.Restaurants.Remove(to_delete);
                context_.SaveChanges();
            }
        }
    }
}