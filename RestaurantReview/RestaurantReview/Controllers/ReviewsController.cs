using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        [HttpGet("{user}")]

        public ActionResult<List<Review>> Get(string user)
        {
            return new ReviewsDAL(connection.AWSconnstring()).GetAllReviews()
                                                          .FindAll(review => review.User.UserName.Equals(user));
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