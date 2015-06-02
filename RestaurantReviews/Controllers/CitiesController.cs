using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RestaurantReviews.Models;

namespace RestaurantReviews.Controllers
{
    [RoutePrefix("api/cities")]
    public class CitiesController : ApiController
    {
        private RestaurantReviewsEntities db = new RestaurantReviewsEntities();

        #region non-REST-standard methods

        // GET: api/Cities/5/restaurants
        [Route("{id:int}/restaurants")]
        [ResponseType(typeof(List<Restaurant>))]
        public IHttpActionResult GetRestaurants(int id)
        {
            return Ok(db.Restaurants.Where(x => x.CityId == id));
        }

        #endregion

        #region REST-standard methods

        // GET: api/Cities
        public IHttpActionResult GetCities()
        {
            return Ok(db.Cities);
        }

        // GET: api/Cities/5
        [ResponseType(typeof(City))]
        public IHttpActionResult GetCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/Cities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCity(int id, City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != city.Id)
            {
                return BadRequest();
            }

            db.Entry(city).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        [ResponseType(typeof(City))]
        public IHttpActionResult PostCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cities.Add(city);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CityExists(city.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [ResponseType(typeof(City))]
        public IHttpActionResult DeleteCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            db.Cities.Remove(city);
            db.SaveChanges();

            return Ok(city);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(int id)
        {
            return db.Cities.Count(e => e.Id == id) > 0;
        }

        #endregion

    }
}