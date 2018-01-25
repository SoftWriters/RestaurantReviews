using System.Web.Mvc;

namespace RestaurantReviews.Web.Controllers
{
    public class HomeController : RestaurantReviewsControllerBase
    {
        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}