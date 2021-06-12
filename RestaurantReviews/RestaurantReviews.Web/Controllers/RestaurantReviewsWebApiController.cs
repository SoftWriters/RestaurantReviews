using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestaurantReviews.Controller;
using RestaurantReviews.Core.DataTypes;
using RestaurantReviews.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace RestaurantReviews.Web.Controllers
{
    //These should probably return IResultAction or one of those patterns
    [ApiController]
    [Route("api")]
    public class RestaurantReviewsWebApiController : ControllerBase, IDisposable
    {
        private IRestaurantReviewController _controller;

        public RestaurantReviewsWebApiController(IWebHostEnvironment hostingEnvironment, IOptions<RestaurantReviewConfig> config)
        {
            string dbFilePath = Path.Combine(hostingEnvironment.ContentRootPath, config.Value.DatabaseFilePath);
            _controller = new RestaurantReviewsController(dbFilePath);
        }

        [HttpPost]
        [Route("restaurants")]
        public void AddRestaurant([FromBody] Restaurant restaurant)
        {
            _controller.AddRestaurant(restaurant);
        }

        [HttpDelete]
        [Route("restaurants/{restaurantId}")]
        public void DeleteRestaurant(Guid restaurantId)
        {
            _controller.DeleteRestaurant(restaurantId);
        }

        [HttpPost]
        [Route("reviews")]
        public void AddReview([FromBody] RestaurantReview review)
        {
            _controller.AddReview(review);
        }

        [HttpDelete]
        [Route("reviews/{reviewId}")]
        public void DeleteReview(Guid reviewId)
        {
            _controller.DeleteReview(reviewId);
        }

        [HttpGet]
        [Route("restaurants")]
        public IEnumerable<IRestaurant> GetRestaurants([FromQuery] RestaurantsQuery query)
        {
            return _controller.FindRestaurants(query);
        }

        [HttpGet]
        [Route("restaurants/{restaurantId}")]
        public IRestaurant GetRestaurant(Guid restaurantId)
        {
            return _controller.GetRestaurant(restaurantId);
        }

        [HttpGet]
        [Route("reviews/{reviewId}")]
        public IRestaurantReview GetReview(Guid reviewId)
        {
            return _controller.GetReview(reviewId);
        }

        [HttpGet]
        [Route("restaurants/{restaurantId}/reviews")]
        public IEnumerable<IRestaurantReview> GetReviews(Guid restaurantId)
        {
            return _controller.GetReviewsForRestaurant(restaurantId);
        }

        [HttpGet]
        [Route("reviews")]
        public IEnumerable<IRestaurantReview> GetReviewsByUserId([FromQuery] Guid userId)
        {
            return _controller.GetReviewsForUser(userId);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _controller?.Dispose();
                _controller = null;
            }
        }

    }
}
