using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EateryviewApi.Models;

namespace EateryviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EateryviewController : Controller
    {

        private readonly EateryviewContext EV_Context;

        public EateryviewController(EateryviewContext ctxt)
        {
            EV_Context = ctxt;
        }

        //Get all Restaurants
        // GET: api/Eateryview
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return await EV_Context.Restaurants.ToListAsync();
        }

        //Get all restaurants in the named city
        // GET: api/Eateryview
        [HttpGet("city/{city}")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetCityRestaurants(string city)
        {
            List<Restaurant> all_restaurants = await EV_Context.Restaurants.ToListAsync();
            List<Restaurant> by_city = new List<Restaurant>();
            foreach (Restaurant r in all_restaurants)
            {
                if (r.City == city) by_city.Add(r);
            }

            return by_city;
        }

        //Get all reviews
        // GET: api/Eateryview/review
        [HttpGet("review/")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        { 
            return await EV_Context.Reviews.ToListAsync();
        }

        //Get all reviews by the supplied user
        // GET: api/Eateryview/review/user
        [HttpGet("review/user/{user}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(string user)
        {
            List<Review> all_reviews = await EV_Context.Reviews.ToListAsync();
            List<Review> eatery_reviews = new List<Review>();
            foreach (Review rev in all_reviews)
            {
                if (rev.UserName == user) eatery_reviews.Add(rev);
            }

            return eatery_reviews;
        }

        //Get all reviews for the supplied restaurant
        // GET: api/Eateryview/review/eatery
        [HttpGet("review/eatery/{eatery_id}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetEateryReviews(long eatery_id)
        {
            List<Review> all_reviews = await EV_Context.Reviews.ToListAsync();
            List<Review> eatery_reviews = new List<Review>();
            foreach (Review rev in all_reviews)
            {
                if (rev.RestaurantId == eatery_id) eatery_reviews.Add(rev);
            }

            return eatery_reviews;
        }

        //Add a new restaurant
        // POST: api/Eateryview
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant new_restaurant)
        {
            EV_Context.Restaurants.Add(new_restaurant);
            await EV_Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurants), new { id = new_restaurant.Id }, new_restaurant);
        }

        //Add a new review if the restauarant exists.  The restaurant's avg rating will be recalculated/stored
        // POST: api/Eateryview/review
        [HttpPost("review/")]
        public async Task<ActionResult<Restaurant>> PostReview(Review new_review)
        {
            double avg = 0;
            long cnt = 0;
            Restaurant r = await EV_Context.Restaurants.FindAsync(new_review.RestaurantId);
            if (r == null)
            {
                return NotFound();
            }

            EV_Context.Reviews.Add(new_review);
            await EV_Context.SaveChangesAsync();

            List<Review> all_reviews = await EV_Context.Reviews.ToListAsync();
            foreach (Review rev in all_reviews)
            {
                if (rev.RestaurantId == new_review.RestaurantId)
                {
                    avg += rev.Stars;
                    cnt++;
                }
            }
            if (cnt > 0)
            {
                avg = avg / cnt;
            }
            else
            {
                avg = 0;
            }
            r.AvgStars = avg;
            EV_Context.Restaurants.Update(r);
            await EV_Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviews), new { user = new_review.UserName }, new_review);
        }

        //Remove a review
        // DELETE: api/Eateryview/{id}
        [HttpDelete("review/{id}")]
        public async Task<IActionResult> DeleteReview(long id)
        { 
            Review r = await EV_Context.Reviews.FindAsync(id);

            if (r == null)
            {
                return NotFound();
            }

            EV_Context.Reviews.Remove(r);
            await EV_Context.SaveChangesAsync();

            return NoContent();
        }
    }
}
