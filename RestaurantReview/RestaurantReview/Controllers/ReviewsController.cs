using Microsoft.AspNetCore.Mvc;
using RestaurantReview.DAL;
using RestaurantReview.Models;
using RestaurantReview.Services;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> 0e003586d4895633c82957f8d50d4fc4e29eed18

namespace RestaurantReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        public IConn connection;

        public ReviewsController(IConn conn)
        {
            this.connection = conn;
        }

        // GET api/Reviews/{username}
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var usermatch = new UserDAL(connection.AWSconnstring()).GetUser(username);
<<<<<<< HEAD
            if (!usermatch.IsSuccessful) return StatusCode(404, "User was not found");
            var list = new ReviewsDAL(connection.AWSconnstring()).GetAllReviews()
                                                          .FindAll(review => review.User.UserName.ToLower().Equals(username.ToLower()));

=======
            if(!usermatch.IsSuccessful) return StatusCode(404, "User was not found");
            var list = new ReviewsDAL(connection.AWSconnstring()).GetAllReviews()
                                                          .FindAll(review => review.User.UserName.ToLower().Equals(username.ToLower()));
            
>>>>>>> 0e003586d4895633c82957f8d50d4fc4e29eed18
            if (list.Count >= 1) { return Ok(list); } else { return StatusCode(404, "There are no results for this user"); }
        }

        // POST api/Reviews - must send in a Review Json object
        [HttpPost]
        public void Post([FromBody] Review review)
        {
            new ReviewsDAL(connection.AWSconnstring()).PostReview(review);
        }

        [HttpPut]
        public void Put([FromBody] UpdateReview updateReview)
        {
            new ReviewsDAL(connection.AWSconnstring()).UpdateReview(updateReview);
        }

        // DELETE api/Reviews/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            new ReviewsDAL(connection.AWSconnstring()).DeleteReview(id);
        }
    }
}