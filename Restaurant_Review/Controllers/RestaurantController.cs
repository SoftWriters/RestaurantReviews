using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Restaurant_Review.Data.Repository;
using Restaurant_Review.Extensions.MVC;
using Restaurant_Review.Models;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Restaurant_Review.Controllers
{
    /// <summary>
    /// restaurant controller class
    /// </summary>
    public class RestaurantController : BaseController
    {
        private readonly RestaurantRepository _restaurantRepository;
        /// <summary>
        /// CTOR
        /// </summary>
        public RestaurantController()
        {
            string uName = string.Empty;
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
            {
                uName = user.UserName;
            }
            _restaurantRepository = new RestaurantRepository(ConnString, uName);
        }
     
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>syte
        /// get all restaurants
        /// </summary>
        /// <remarks>
        /// get a list of restaurants
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [System.Web.Mvc.Route("restaurant")]
        [AllowJsonGet]
        public JsonResult Get()
        {
            return Json(_restaurantRepository.GetAllWithAddress());
        }
        /// <summary>
        /// get restaurant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Mvc.Route("restaurant/{id}")]
        [AllowJsonGet]
        public JsonResult Get(int id)
        {
            return Json(_restaurantRepository.GetById(id));
        }
        /// <summary>
        /// get restaurant by city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [System.Web.Mvc.Route("restaurant/GetByCity/{city}")]
        [AllowJsonGet]
        public JsonResult GetByCity(string city)
        {
            return Json(_restaurantRepository.GetByCity(city.Trim()));
        }
        /// <summary>
        /// save restaurant
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("restaurant")]
        public JsonResult Post([FromBody]Restaurant res)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                result = _restaurantRepository.Add(res);
            }
            return Json(_restaurantRepository.GetById(result));
        }
        /// <summary>
        /// update restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        [System.Web.Mvc.Route("restaurant/{id}")]
        [System.Web.Mvc.HttpPut]
        public JsonResult Put(int id, [FromBody]Restaurant res)
        {
            res.IDRestaurant = id;
            if (ModelState.IsValid)
            {
                _restaurantRepository.Update(res);
            }
            return Json(_restaurantRepository.GetById(id));
        }
        /// <summary>
        /// get restaurant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Mvc.Route("restaurant/{id}")]
        [System.Web.Mvc.HttpDelete]

        public JsonResult Delete(int id)
        {
            return Json(_restaurantRepository.Delete(id));
        }

    

    }
}