using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;
using System.Web.Mvc;

namespace RestaurantReviews.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index(int userID = 0, int restaurantID = 0)
        {
            if (!SessionHelper.HasUser())
                return Redirect("/Login");

            return View(ReviewManager.GetReviewViewModel(userID, restaurantID));
        }

        // GET: Review/Update
        public ActionResult Update(int id = 0, int restaurantID = 0)
        {
            if (!SessionHelper.HasUser())
                return Redirect("/Login");

            return View(ReviewManager.GetReviewUpdateViewModel(id, restaurantID));
        }
    }
}