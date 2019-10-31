using Microsoft.AspNetCore.Mvc;
using RestaurantReview.DAL;
using RestaurantReview.Models;
using RestaurantReview.Services;

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

            if (!usermatch.IsSuccessful) return StatusCode(404, "User was not found");
            var list = new ReviewsDAL(connection.AWSconnstring()).GetAllReviews()
                                                          .FindAll(review => review.User.UserName.ToLower().Equals(username.ToLower()));

            if (list.Count > 0) { return Ok(list); } else { return StatusCode(404, "There are no results for this user"); }
        }

        // POST api/Reviews - must send in a Review Json object
        [HttpPost]
        public IActionResult Post([FromBody] PostReview review)
        {
            var dal = new ReviewsDAL(connection.AWSconnstring()).PostReview(review);
            if (dal.IsSuccessful) { return (Ok(dal.toreturn)); } else { return StatusCode(304, dal.toreturn); }
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateReview updateReview)
        {
            var dal = new ReviewsDAL(connection.AWSconnstring()).UpdateReview(updateReview);
            if (dal.IsSuccessful) { return (Ok(dal.toreturn)); } else { return StatusCode(304, dal.toreturn); }

        }

        // DELETE api/Reviews/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool IsSuccessful = new ReviewsDAL(connection.AWSconnstring()).DeleteReview(id);
            if (IsSuccessful) { return (Ok()); } else { return StatusCode(404); }
        }


    }
}