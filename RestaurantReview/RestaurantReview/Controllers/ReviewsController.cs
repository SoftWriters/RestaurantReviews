using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReview.DAL;
using RestaurantReview.Models;

namespace RestaurantReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        // GET api/values
        [HttpGet("{user}")]
        
        public ActionResult<List<Review>> Get(string user)
        {
            return new ReviewsDAL().GetAllReviews().FindAll(review => review.User.UserName.Equals(user));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Review review)
        {
            new ReviewsDAL().PostReview(review);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            new ReviewsDAL().DeleteReview(id);
        }
    }
}