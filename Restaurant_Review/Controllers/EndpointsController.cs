using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Review.Controllers
{
    public class EndpointsController : Controller
    {
        // GET: Endpoints
        public ActionResult Index()
        {
            return View();
        }
    }
}