using System.Web;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Restaurant_Review.Data.Repository;
using Restaurant_Review.Extensions.MVC;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Restaurant_Review.Models;

namespace Restaurant_Review.Controllers
{
    public class ReviewUserController : BaseController
    {
        private readonly ReviewRepository _reviewRepository;
        public ReviewUserController()
        {
            string uName = string.Empty;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
            {
                uName = user.UserName;
            }
            _reviewRepository = new ReviewRepository(ConnString, uName);
        }

        // GET: ReviewUser
        public ActionResult Index()
        {
            return View();
        }

        [AllowJsonGet]
        public JsonResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = _reviewRepository.GetReviewsByUsers();
            return Json(result.ToDataSourceResult(request));
        }

        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, Reviews res)
        {
            if (res != null && ModelState.IsValid)
            {
                _reviewRepository.Add(res);
            }

            return Json(new[] { res }.ToDataSourceResult(request, ModelState));
        }

        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, Reviews res)
        {
            if (res != null && ModelState.IsValid)
            {
                _reviewRepository.Update(res);
            }

            return Json(new[] { res }.ToDataSourceResult(request, ModelState));
        }

        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, Reviews res)
        {
            if (res != null)
            {
                _reviewRepository.Delete(res.IDReview);
            }

            return Json(new[] { res }.ToDataSourceResult(request, ModelState));
        }
    }
}