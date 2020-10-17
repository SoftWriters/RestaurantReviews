using RestaurantReviewsService.PortServices;
using RestaurantReviewsService.ViewModels;
using System.Web.Http;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace RestaurantReviewsService.Controllers
{
    [RoutePrefix("api/UserReviews")]
    public class UserReviewsController : ApiController
    {
        private IRestaurantPortService _restaurantPortService;

        public UserReviewsController()
        {
            _restaurantPortService = new RestaurantPortService();
        }

        [HttpPost]
        public void PostNewReview([FromBody]UserReviewViewModel userReviewViewModel)
        {
            _restaurantPortService.PostRestaurantReview(userReviewViewModel);
        }
    }
}
