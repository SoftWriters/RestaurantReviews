using RestaurantReviews.Data;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RestaurantReviews.WebApi.Controllers
{
    public class ReviewController : ApiController
    {
        public Service.Interfaces.IReviewService _Service { get; set; }

        public ReviewController()
        {
            _Service = new Service.ReviewService();
        }

        // GET api/Review
        [HttpGet]
        public List<Review> GetAll()
        {
            return _Service.GetAll();
        }

        // Get api/Restaurant/userid
        [HttpGet]
        public List<Review> GetByUserID(string userId)
        {
            if (userId == "all")
            {
                return GetAll();
            }

            return _Service.GetByUserID(Guid.Parse(userId));
        }

        // GET api/Review/all/5
        [HttpGet]
        public Review GetByID(string category, Guid id)
        {
            if (category != "all")
            {
                throw new NotImplementedException();
            }

            return _Service.GetByID(id);
        }

        // POST api/Review
        public void Post([FromBody]Review review)
        {
            _Service.Save(review);
        }

        // DELETE api/Review/5
        public void Delete(Guid id)
        {
            _Service.Delete(id);
        }
    }
}