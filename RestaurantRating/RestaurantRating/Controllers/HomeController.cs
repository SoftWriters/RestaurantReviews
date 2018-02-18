using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using RestaurantRating.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;



namespace RestaurantRating.Controllers
{
    public class HomeController : Controller
    {


        HttpClient client;
        HttpClient cl;
        HttpClient cldel;

        //The URL of the WEB API Service

        string url = "http://localhost:33356/api/";


        //The HttpClient Class, this will be used for performing 

        //HTTP Operations, GET, POST, PUT, DELETE

        //Set the base address and the Header Formatter

        public HomeController()
        {

            client = new HttpClient();


            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public ActionResult Index()
        {
            RestaurantDBContext db = new RestaurantDBContext();
            db.Restaurants.ToList();
            return View();
        }


        public IEnumerable<String> GetCity()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("restaurant").Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<String>>().Result;
                return null;
            }
            catch
            {
                return null;
            }

        }

        public ActionResult RestaurantListByCity()
        {
            //var model = new CityModel
            //{

            //    Cities = GetCity().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.City })
            //};
            //ViewBag.Restaurants = GetCity().Select(x => new SelectListItem { Value = x.City.ToString(), Text = x.City.ToString() });
            SelectList list = new SelectList(GetCity());
            ViewBag.Restaurants = list;

            return View();


        }

        [HttpPost]
        public ActionResult GetRestaurantFromCity(FormCollection form)
        {
            string city1 = form["MyCity"].ToString();
            string url = "http://localhost:33356/api/restaurantbycity/GetRestaurantByCity?city=" + city1;


            IEnumerable<Restaurant> rest = null;
            //HTTP GET
            var responseTask = client.GetAsync(url);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Restaurant>>();
                readTask.Wait();
                rest = readTask.Result;

            }
            else //web api sent error response 
            {

                rest = Enumerable.Empty<Restaurant>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            return View(rest);


        }


        [HttpGet]
        public ActionResult CreateRestaurant()
        {

            return View();

        }


        [HttpPost]
        [ActionName("CreateRestaurant")]
        public ActionResult CreateRestaurant_Post([Bind(Include = "RestaurantName, City")]Restaurant rest)
        {

            //HTTP POST
            var postTask = client.PostAsJsonAsync<Restaurant>("restaurant", rest);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("RestaurantSuccess");
            }


            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(rest);
        }

        public ActionResult RestaurantSuccess()
        {

            return View();

        }

        public IEnumerable<String> GetNameRest()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("namerestaurant").Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<String>>().Result;
                return null;
            }
            catch
            {
                return null;
            }

        }


        [HttpGet]
        public ActionResult CreateReview()
        {
            SelectList list1 = new SelectList(GetNameRest());
            ViewBag.NameRest = list1;
            return View();
        }


        [HttpPost]
        [ActionName("CreateReview")]
        public ActionResult CreateReview_Post([Bind(Include = "NameOfRest, UserName, Reviews")]Users us)
        {

            //HTTP POST
            var postTask = client.PostAsJsonAsync<Users>("users", us);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("ReviewSuccess");
            }


            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(us);
        }

        public ActionResult ReviewSuccess()
        {
            return View();

        }

        public IEnumerable<String> GetAllUser()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("users").Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<String>>().Result;
                return null;
            }
            catch
            {
                return null;
            }

        }
        public ActionResult DetailsReview()
        {
            //ViewBag.FindUser = GetAllUser().Select(x => new SelectListItem { Value = x.NameOfRest.ToString(), Text = x.UserName.ToString() });
            SelectList list = new SelectList(GetAllUser());
            ViewBag.FindUser = list;
            return View();
        }

        [HttpPost]
        public ActionResult GetReview(FormCollection form)
        {
            IEnumerable<Users> us = null;

            string user1 = form["username"].ToString();
            string url = "http://localhost:33356/api/reviewsbyuser/GetReviewsByUser?user=" + user1;
            cl = new HttpClient();
            cl.BaseAddress = new Uri(url);

            cl.DefaultRequestHeaders.Accept.Clear();

            cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HTTP GET
            var responseTask = cl.GetAsync(url);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Users>>();
                readTask.Wait();
                us = readTask.Result;

            }
            else //web api sent error response 
            {

                us = Enumerable.Empty<Users>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            return View(us);


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            string url = "http://localhost:33356/api/users/DeleteReview?id=" + id;
            cldel = new HttpClient();


            cldel.BaseAddress = new Uri(url);

            cldel.DefaultRequestHeaders.Accept.Clear();

            cldel.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HTTP DELETE
            var deleteTask = cldel.DeleteAsync(url);
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("DeleteSuccess");
            }
            else
            { return RedirectToAction("DeleteFailure"); }


        }

        public ActionResult DeleteSuccess()
        {
            return View();

        }

        public ActionResult DeleteFailure()
        {
            return View();

        }



    }
}
