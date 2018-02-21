using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestaurantReview.Models;

namespace RestaurantReview
{
    public class RestaurantsController : ApiController
    {
        private RestaurantReviewContext db = new RestaurantReviewContext();

        // GET: api/Restaurants
        public IQueryable<Restaurants> GetRestaurants()
        {
            return db.Restaurants;
        }
        // Get the restaurants name
        // GET: api/Restaurants/5
        [ResponseType(typeof(Restaurants))]
        public async Task<IHttpActionResult> GetRestaurants(string name)
        {
            Restaurants restaurants = await db.Restaurants.FindAsync(name);
            if (restaurants == null)
            {
                return NotFound();
            }

            return Ok(restaurants);
        }
        // Update the restaurant
        // PUT: api/Restaurants/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRestaurants(string name, Restaurants restaurants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (name != restaurants.RestaurantName)
            {
                return BadRequest();
            }

            db.Entry(restaurants).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                string id = null;
                if (!RestaurantsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // Show the retaurants
        // POST: api/Restaurants
        [ResponseType(typeof(Restaurants))]
        public async Task<IHttpActionResult> PostRestaurants(Restaurants restaurants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(restaurants);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { name = restaurants.RestaurantName }, restaurants);
        }
        // Delete the restaurant
        // DELETE: api/Restaurants/5
        [ResponseType(typeof(Restaurants))]
        public async Task<IHttpActionResult> DeleteRestaurants(string name)
        {
            Restaurants restaurants = await db.Restaurants.FindAsync(name);
            if (restaurants == null)
            {
                return NotFound();
            }

            db.Restaurants.Remove(restaurants);
            await db.SaveChangesAsync();

            return Ok(restaurants);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RestaurantsExists(string name)
        {
            return db.Restaurants.Count(e => e.RestaurantName == name) > 0;
        }
    }
}