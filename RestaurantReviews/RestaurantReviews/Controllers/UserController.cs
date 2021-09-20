using RestaurantReviews.DataAccess;
using RestaurantReviews.Models;
using System.Web.Mvc;

namespace RestaurantReviews.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(int id = 0)
        {
            UserModel user = (id == 0) ? new UserModel() : UserManager.GetUser(id); 
            return View(user);
        }
    }
}