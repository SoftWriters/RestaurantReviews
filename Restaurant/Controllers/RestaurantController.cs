using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    [RoutePrefix("API")]
    public class RestaurantController : ApiController
    {
        ResturantDAL objRest = new ResturantDAL();

        [HttpPost]
        [Route("GetRestaurant")]
        public Response GetRestuarants(Request obj)
        {
            return objRest.GetRestaurants(obj);
        }
        [HttpPost]
        [Route("SaveRestaurant")]
        public Response SavetRestuarants(SaveRest obj)
        {
            return objRest.SaveRestaurants(obj);
        }
    }
}
