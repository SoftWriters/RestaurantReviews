using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        public ReviewController(IReviewRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        [Route("GetByUser")]
        [HttpGet]
        public IList<Review> GetByUser([FromQuery]int userId)
        {
            return _reviewRepo.ReadReviewsByUser(userId);
        }

        [Route("GetAllReviews")]
        [HttpGet]
        public IList<Review> GetAllReviews()
        {
            return _reviewRepo.ReadAllReviews();
        }

        [HttpPost]
        public UpdateResult Post([FromBody] Review review)
        {
            var result = new UpdateResult();
            try
            {
                _reviewRepo.CreateReview(review);
            }
            catch(Exception ex)
            {
                result.success = false;
                result.errors.Add(ex.Message);
            }
            return result;
        }

        [HttpDelete("{id}")]
        public UpdateResult Delete(int id)
        {
            var result = new UpdateResult();
            try
            {
                _reviewRepo.DeleteReview(id);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errors.Add(ex.Message);
            }
            return result;
        }
    }
}
