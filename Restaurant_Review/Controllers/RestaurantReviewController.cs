using System;
using System.Web;
using Restaurant_Review.Data.Repository;
using Restaurant_Review.Extensions.MVC;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Restaurant_Review.Models;

namespace Restaurant_Review.Controllers
{
    /// <summary>
    /// review controller class
    /// </summary>
    public class RestaurantReviewController : BaseController
    {
        private readonly ReviewRepository _reviewRepository;
        private readonly string _userName = String.Empty;
        /// <summary>
        /// CTOR
        /// </summary>
        public RestaurantReviewController()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
            {
                _userName = user.UserName;
            }
            _reviewRepository = new ReviewRepository(ConnString, _userName);
        }
        // GET: RestaurantReview
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// get reviews
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.Route("review")]
        [AllowJsonGet]
        public JsonResult Get()
        {
            return Json(_reviewRepository.GetAll());
        }
        /// <summary>
        /// get reviews by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [System.Web.Mvc.Route("review/GetReviewsByUserName")]
        [System.Web.Mvc.HttpPost]
        public JsonResult GetReviewsByUserName([FromBody]string userName)
        {
            return Json(_reviewRepository.GetReviewsByUserName(userName));
        }
        /// <summary>
        /// save review
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("review")]
        public JsonResult Post([FromBody]Reviews res)
        {
            var result = 0;
            res.DateCreated = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                result = _reviewRepository.Add(res);
            }
            return Json(_reviewRepository.GetById(result));
        }
        /// <summary>
        /// update review
        /// </summary>
        /// <param name="id"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        [System.Web.Mvc.Route("review/{id}")]
        [AllowJsonGet]
        public JsonResult Put(int id, [FromBody]Reviews res)
        {
            res.IDReview = id;
            res.DateCreated = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                _reviewRepository.Update(res);
            }
            return Json(_reviewRepository.GetById(id));
        }
        /// <summary>
        /// delete review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Mvc.Route("review/{id}")]
        public JsonResult Delete(int id)
        {
            return Json(_reviewRepository.Delete(id));
        }



    }
}