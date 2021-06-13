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
            //Workaround since ASP.NET doesn't seem to respect [JsonIgnore]
            //Convert to generic Restaurant type for Json serialization
            return _controller.FindRestaurants(query).Select(r => new Restaurant(r));
        }

        [HttpGet]
        [Route("restaurants/{restaurantId}")]
        public IRestaurant GetRestaurant(Guid restaurantId)
        {
            //Workaround since ASP.NET doesn't seem to respect [JsonIgnore]
            //Convert to generic Restaurant type for Json serialization
            return new Restaurant(_controller.GetRestaurant(restaurantId));
        }

        [HttpGet]
        [Route("reviews/{reviewId}")]
        public IRestaurantReview GetReview(Guid reviewId)
        {
            //Workaround since ASP.NET doesn't seem to respect [JsonIgnore]
            //Convert to generic Review type for Json serialization
            return new RestaurantReview(_controller.GetReview(reviewId));
        }

        [HttpGet]
        [Route("restaurants/{restaurantId}/reviews")]
        public IEnumerable<IRestaurantReview> GetReviews(Guid restaurantId)
        {
            //Workaround since ASP.NET doesn't seem to respect [JsonIgnore]
            //Convert to generic Restaurant type for Json serialization
            return _controller.GetReviewsForRestaurant(restaurantId).Select(r => new RestaurantReview(r));
        }

        [HttpGet]
        [Route("reviews")]
        public IEnumerable<IRestaurantReview> GetReviewsByUserId([FromQuery] Guid userId)
        {
            //Workaround since ASP.NET doesn't seem to respect [JsonIgnore]
            //Convert to generic Restaurant type for Json serialization
            return _controller.GetReviewsForUser(userId).Select(r => new RestaurantReview(r));
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
