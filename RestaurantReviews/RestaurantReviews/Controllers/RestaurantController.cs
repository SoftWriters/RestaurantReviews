using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;
using System.Web.Mvc;

namespace RestaurantReviews.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Index(string city = null)
        {
            if (!SessionHelper.HasUser())
                return Redirect("/Login");

            return View(RestaurantManager.GetRestaurantViewModel(city));
        }

        // GET: Review/Update
        public ActionResult Update(int id = 0)
        {
            if (!SessionHelper.HasUser())
                return Redirect("/Login");

            RestaurantInfoModel restaurant = (id == 0) ? new RestaurantInfoModel() : RestaurantManager.GetRestaurant(id);
            return View(restaurant);
        }
    }
}