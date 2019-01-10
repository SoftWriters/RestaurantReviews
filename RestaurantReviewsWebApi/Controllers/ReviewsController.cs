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
using RestaurantReviewsEntities;

namespace RestaurantReviewsWebApi.Controllers
{
    public class ReviewsController : ApiController
    {
        private readonly RestaurantReviewsEntities.RestaurantReviewsEntities Db = new RestaurantReviewsEntities.RestaurantReviewsEntities();

        // GET: api/Reviews
        public IQueryable<Review> GetReviews()
        {
            return Db.Reviews;
        }
         
        // GET: api/Reviews  Get Review by users
        public IQueryable<Review> GetReviews(int idUser)
        {
            return Db.Reviews.Where(p => p.User.Id == idUser);
        }

        // GET: api/Reviews/5
        [ResponseType(typeof(Review))]
        public async Task<IHttpActionResult> GetReview(int id)
        {
            Review review = await Db.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // PUT: api/Reviews/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReview(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != review.Id)
            {
                return BadRequest();
            }

            Db.Entry(review).State = EntityState.Modified;

            try
            {
                await Db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        [ResponseType(typeof(Review))]
        public async Task<IHttpActionResult> PostReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Db.Reviews.Add(review);

            try
            {
                await Db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReviewExists(review.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [ResponseType(typeof(Review))]
        public async Task<IHttpActionResult> DeleteReview(int id)
        {
            Review review = await Db.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            Db.Reviews.Remove(review);
            await Db.SaveChangesAsync();

            return Ok(review);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReviewExists(int id)
        {
            return Db.Reviews.Count(e => e.Id == id) > 0;
        }
    }
}