using RestaurantReviewsService.PortServices;
using RestaurantReviewsService.ViewModels;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace RestaurantReviewsService.Controllers
{
    [RoutePrefix("api/RestaurantReview")]
    [EnableCors(origins: "http://localhost:5421/", headers: "*", methods: "*")]
    public class RestaurantReviewController : ApiController
    {
        private IRestaurantPortService _restaurantPortService;

        public RestaurantReviewController()
        {
            _restaurantPortService = new RestaurantPortService();
        }

        // example api/RetaurantReview
        [HttpGet]
        public IList<RestaurantViewModel> GetAllRestaurants()
        {
            return _restaurantPortService.GetAllRestaurants();
        }

        [HttpGet]
        [Route("{name}")]
        // example api/RetaurantReview/Twin Peaks/
        public IList<RestaurantViewModel> GetReviewsForCity(string name)
        {
            return _restaurantPortService.GetRestaurantReviewsByCity(name);
        }        

        [HttpPost]
        public void PostNewRestaurant([FromBody] RestaurantViewModel data)
        {
            _restaurantPortService.AddRestaurant(data);
        }      
    }
}