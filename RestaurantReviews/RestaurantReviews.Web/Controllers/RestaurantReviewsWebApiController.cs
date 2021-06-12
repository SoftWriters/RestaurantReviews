using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestaurantReviews.Controller;
using RestaurantReviews.Core.DataTypes;
using RestaurantReviews.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestaurantReviews.Web.Controllers
{
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
