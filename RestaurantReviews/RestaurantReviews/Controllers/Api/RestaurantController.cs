using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace RestaurantReviews.Controllers.Api
{
    public class RestaurantController : ApiController
    {
        // PUT api/Restaurant
        [HttpPut]
        public int Put([FromBody]RestaurantModel restaurant)
        {
            restaurant.UserID = SessionHelper.UserID;
            return RestaurantManager.UpdateRestaurant(restaurant);
        }
    }
}