using System.Web.Mvc;

namespace RestaurantReviews.WebServices.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return RedirectToAction("Index", "Help");
        }
    }
}
