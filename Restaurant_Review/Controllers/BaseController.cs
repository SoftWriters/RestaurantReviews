using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Review.Extensions.MVC;

namespace Restaurant_Review.Controllers
{
    public class BaseController : Controller
    {
        public readonly string ConnString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
       
    }
}