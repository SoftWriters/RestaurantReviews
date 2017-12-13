using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Models;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantReviews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        private readonly RestaurantReviewContext context_;

        public ReviewsController(RestaurantReviewContext context)
        {
            context_ = context;
        }    

        [AllowAnonymous]
        [HttpGet("{id}")]
        public Review Get(long id)
        {
            return (from review in context_.Reviews 
                where review.ReviewId == id 
                select review).DefaultIfEmpty(null).SingleOrDefault();
        }

        // GET api/reviews
        [AllowAnonymous]
        [HttpGet()]
        public IEnumerable<Review> Get(long? restaurantid = null, int? minrating = null, string username = "")
        {
            var qry = from review in context_.Reviews 
                    select review;
                    
            if (minrating != null)
            {
                qry = qry.Where(review => review.Rating >= minrating);
            }

            if (!String.IsNullOrEmpty(username))
            {
                qry = qry.Where(review => review.UserName == username);
            }
            
            if (restaurantid != null)
            {
                qry = qry.Where(review => review.RestaurantId == restaurantid);
            }
            
            return qry.ToArray(); //final DB qry happenns here
        }

        // POST api/reviews
        [HttpPost]
        public Review Post([FromBody]Review new_review)
        {
            context_.Add(new_review);
            context_.SaveChanges();
            return new_review;
        }

        // PUT api/reviews/5
        [HttpPut("{id}")]
        public Review Put(int id, [FromBody]Review update_review)
        {
            var old_review = (from review in context_.Reviews
                where review.ReviewId == id
                select review).DefaultIfEmpty(null).SingleOrDefault();

            if (old_review != null)
            {
                old_review.Rating = update_review.Rating;
                old_review.Description = update_review.Description;
                old_review.RestaurantId = update_review.RestaurantId;
                old_review.UserName = update_review.UserName;
                context_.SaveChanges();
            }

            return old_review;
        }

        // DELETE api/reviews/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var to_delete = (from review in context_.Reviews
                where review.ReviewId == id
                select review).DefaultIfEmpty(null).SingleOrDefault();

            if (to_delete != null)
            {
                context_.Reviews.Remove(to_delete);
                context_.SaveChanges();
            }
        }
    }
}