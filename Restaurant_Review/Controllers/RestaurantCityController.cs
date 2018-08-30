using System;
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
    /// <summary>
    /// used for MVC View of Restaurant by City
    /// </summary>
    public class RestaurantCityController : BaseController
    {
        private readonly RestaurantRepository _restaurantRepository;
        private readonly string _userName = string.Empty;
        /// <summary>
        /// CTOR
        /// </summary>
        public RestaurantCityController()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
            {
                _userName = user.UserName;
            }
            _restaurantRepository = new RestaurantRepository(ConnString, _userName);
        }

        public ActionResult Index()
        {
            return View();
        }
       
        [AllowJsonGet]
        public JsonResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = _restaurantRepository.GetAllWithAddress();
            return Json(result.ToDataSourceResult(request));
        }

        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, Restaurant res)
        {
            res.DateCreated = DateTime.UtcNow;
            res.DateUpdated = DateTime.UtcNow;
            res.IDUserCreated = _userName;
            if (ModelState.IsValid)
            {
                _restaurantRepository.Add(res);
            }

            return Json(new[] { res }.ToDataSourceResult(request, ModelState));
        }

        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, Restaurant res)
        {
            res.DateCreated = res.DateCreated;
            res.DateUpdated = DateTime.UtcNow;
            res.IDUserUpdated = _userName;
            if (ModelState.IsValid)
            {
                _restaurantRepository.Update(res);
            }

            return Json(new[] { res }.ToDataSourceResult(request, ModelState));
        }

        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, Restaurant res)
        {
            if (res != null)
            {
                _restaurantRepository.Delete(res.IDRestaurant);
            }

            return Json(new[] { res }.ToDataSourceResult(request, ModelState));
        }
    }
}