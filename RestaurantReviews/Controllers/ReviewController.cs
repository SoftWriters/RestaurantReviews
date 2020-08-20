using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RestaurantReviews.Api.Model;
using RestaurantReviews.Data;
using RestaurantReviews.Model;

namespace RestaurantReviews.Api.Controllers
{
    [ApiController]
    public class ReviewController : ControllerBase
    {
        [HttpGet("review/user/{userId}")]
        public List<Review> GetReviewsByUser(Guid userId)
        {
            return ReviewDAO.GetReviewsByUser(userId);
        }

        [HttpPost("review/add")]
        public IActionResult Add(Review reviewIn)
        {
            Review review = ReviewDAO.Add(reviewIn);

            if (review != null)
                return Created(new Uri($"review/add/{review.ReviewId}", UriKind.Relative), review);
            else
                return Conflict("Already Exists");
        }

        [HttpPost("review/create")]
        public IActionResult Add(DateTime reviewDate, Guid restaurantId, Guid userId, Rating rating, string comments)     
        {
            Review review = ReviewDAO.Add(reviewDate, restaurantId, userId, rating, comments);

            if (review != null)
                return Created(new Uri($"review/create/{review.ReviewId}", UriKind.Relative), review);
            else
                return Conflict("Already Exists");
        }

        [HttpDelete("review/delete/{reviewId}")]
        public IActionResult Delete(Guid reviewId)
        {
            if (ReviewDAO.Delete(reviewId))
                return Ok();
            else
                return NotFound();
        }
    }
}
