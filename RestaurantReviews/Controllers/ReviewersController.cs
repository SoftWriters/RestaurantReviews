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
    [RoutePrefix("api/reviewers")]
    public class ReviewersController : ApiController
    {
        private RestaurantReviewsEntities db = new RestaurantReviewsEntities();

        #region non-REST-standard methods

        // GET: api/Reviewers/5/reviews
        // Returns: list of reviews for this reviewer
        [Route("{id:int}/reviews")]
        [ResponseType(typeof(List<Review>))]
        public IHttpActionResult GetReviews(int id)
        {
            return Ok(db.Reviews.Where(x => x.ReviewerId == id));
        }

        #endregion

        #region REST-standard methods

        // GET: api/Reviewers
        public IQueryable<Reviewer> GetReviewers()
        {
            return db.Reviewers;
        }

        // GET: api/Reviewers/5
        [ResponseType(typeof(Reviewer))]
        public IHttpActionResult GetReviewer(int id)
        {
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return NotFound();
            }

            return Ok(reviewer);
        }

        // PUT: api/Reviewers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReviewer(int id, Reviewer reviewer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reviewer.Id)
            {
                return BadRequest();
            }

            db.Entry(reviewer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewerExists(id))
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

        // POST: api/Reviewers
        [ResponseType(typeof(Reviewer))]
        public IHttpActionResult PostReviewer(Reviewer reviewer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reviewers.Add(reviewer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReviewerExists(reviewer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reviewer.Id }, reviewer);
        }

        // DELETE: api/Reviewers/5
        [ResponseType(typeof(Reviewer))]
        public IHttpActionResult DeleteReviewer(int id)
        {
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return NotFound();
            }

            db.Reviewers.Remove(reviewer);
            db.SaveChanges();

            return Ok(reviewer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReviewerExists(int id)
        {
            return db.Reviewers.Count(e => e.Id == id) > 0;
        }

        #endregion

    }
}