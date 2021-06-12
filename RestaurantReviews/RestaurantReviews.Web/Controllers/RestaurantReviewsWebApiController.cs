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
    [ApiController]
    [Route("api/restaurants")]
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
        public IEnumerable<Restaurant> GetRestaurants(RestaurantsQuery query)
        {
            var restaurants = _controller.FindRestaurants(query);

            return new List<Restaurant>();
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
