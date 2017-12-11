using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Models;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        private readonly RestaurantReviewContext context_;

        public ReviewsController(RestaurantReviewContext context)
        {
            context_ = context;
        }    

        [HttpGet("{id}")]
        public Review Get(long id)
        {
            return (from review in context_.Reviews 
                where review.ReviewId == id 
                select review).DefaultIfEmpty(null).SingleOrDefault();
        }

        // GET api/reviews
        [HttpGet()]
        public IEnumerable<Review> Get(long? restaurantid = null, int? minrating = null, string username = "")
        {
            if (minrating != null)
            {
                return (from review in context_.Reviews 
                    where review.Rating >= minrating
                    select review).ToArray();
            }
            if (!String.IsNullOrEmpty(username))
            {
                return (from review in context_.Reviews 
                    where review.UserName == username 
                    select review).ToArray();
            }
            else if (restaurantid != null)
            {
                return (from review in context_.Reviews 
                    where review.RestaurantId == restaurantid 
                    select review).ToArray();
            }
            else
            {
                return (from review in context_.Reviews 
                    select review).ToArray();
            }
        }

        // POST api/reviews
        [HttpPost]
        public void Post([FromBody]Review new_review)
        {
            context_.Add(new_review);
            context_.SaveChanges();
        }

        // PUT api/reviews/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Review update_review)
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