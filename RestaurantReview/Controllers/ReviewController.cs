using RestaurantReview.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using RestaurantReview.Service;
using System.Web.Mvc;

namespace RestaurantReview.Web.Controllers
{
    //more meaningful returns would be added, but that is out of the scope of this assignment
    //input checking/error catching would be done and passed through the api
    //user permissions would be added as well, user id is left to cover that
    [Authorize]
    public class ReviewController : Controller
    {
        [Route("restaurants/add", Name = "RestaurantAdd")]
        [HttpPost]
        public ActionResult RestaurantAdd(int cityid, string name)
        {
            RestaurantService r = new RestaurantService();

            Restaurant model = new Restaurant();

            model.CityID = cityid;
            model.Name = name;

            return Json(r.Add(model, 0));
        }

        [Route("restaurants/getbycity/{cityid}", Name = "RestaurantsGetByCity")]
        [HttpPost]
        public ActionResult RestaurantsGetByCity(int cityid)
        {
            RestaurantService r = new RestaurantService();

            return Json(r.GetByCityId(cityid).ToList());
        }

        [Route("reviews/add", Name = "ReviewAdd")]
        [HttpPost]
        public ActionResult ReviewAdd(int restaurantId, int userId, int rating, string comments)
        {
            ReviewService r = new ReviewService();

            RestaurantReview.BL.Model.Review model = new RestaurantReview.BL.Model.Review();

            model.RestaurantID = restaurantId;
            model.UserID = userId;
            model.Rating = rating;
            model.Comments = comments;

            return Json(r.Add(model, 0));
        }

        [Route("reviews/delete", Name = "ReviewDelete")]
        [HttpPost]
        public ActionResult ReviewDelete(int id)
        {
            ReviewService r = new ReviewService();

            RestaurantReview.BL.Model.Review model = new RestaurantReview.BL.Model.Review();

            model.Id = id;

            r.Delete(model, 0);

            return Json(0);
        }

        [Route("reviews/getbyuser/{userid}", Name = "ReviewsGetByUser")]
        [HttpPost]
        public ActionResult ReviewsGetByUser(int userid)
        {
            ReviewService r = new ReviewService();

            return Json(r.GetByUserId(userid).ToList());
        }

    }
}
