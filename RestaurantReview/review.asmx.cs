using RestaurantReview.BL.Model;
using RestaurantReview.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RestaurantReview.Web
{
    /// <summary>
    /// Summary description for Review
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ReviewWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public int RestaurantAdd(int cityid, string name)
        {
            RestaurantService r = new RestaurantService();

            Restaurant model = new Restaurant();

            model.CityID = cityid;
            model.Name = name;

            return r.Add(model, 0);
        }

        [WebMethod]
        public List<Restaurant> RestaurantsGetByCity(int cityid)
        {
            RestaurantService r = new RestaurantService();

            return r.GetByCityId(cityid).ToList();
        }

        [WebMethod]
        public int ReviewAdd(int restaurantId, int userId, int rating, string comments)
        {
            ReviewService r = new ReviewService();

            RestaurantReview.BL.Model.Review model = new RestaurantReview.BL.Model.Review();

            model.RestaurantID = restaurantId;
            model.UserID = userId;
            model.Rating = rating;
            model.Comments = comments;

            return r.Add(model, 0);
        }

        [WebMethod]
        public int ReviewDelete(int id)
        {
            ReviewService r = new ReviewService();

            RestaurantReview.BL.Model.Review model = new RestaurantReview.BL.Model.Review();

            model.Id = id;

            r.Delete(model, 0);

            return 0;
        }

        [WebMethod]
        public List<Review> ReviewsGetByUser(int userid)
        {
            ReviewService r = new ReviewService();

            return r.GetByUserId(userid).ToList();
        }
    }
}
