using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantReviews.Domain;
using RestaurantReviews.DataAccess;

namespace RestaurantReviews.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly IRestaurantReviewData _restaurantReviewData;

        public RestaurantController(IRestaurantReviewData restaurantReviewData)
        {
            this._restaurantReviewData = restaurantReviewData;
        }

        [HttpGet]
        [Route("api/Restaurants/GetRestaurants")]
        public IHttpActionResult Restaurants(int cityId)
        {
            var result = _restaurantReviewData.GetRestaurantsByCity(cityId);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/Restaurants/AddRestaurant")]
        public IHttpActionResult AddRestaurant(Restaurant restaurant)
        {
            var result = _restaurantReviewData.AddRestaurant(restaurant);
            return Ok(result);
        }
    }
}
