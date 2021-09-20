using RestaurantReviews.Helpers;
using System.Web.Mvc;

namespace RestaurantReviews.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            SessionHelper.Clear();
            return View();
        }
    }
}