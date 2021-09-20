using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Controllers;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;
using System.Web;
using System.Web.Mvc;

namespace RestaurantReviews.Tests.Controllers
{
    [TestClass]
    public class RestaurantControllerTest
    {
        [TestMethod]
        public void Index()
        {
            DBCaller.IsTest = true;
            HttpContext.Current = TestHelper.TestHttpContext();
            SessionHelper.SetUser(TestHelper.TestUser);

            RestaurantController controller = new RestaurantController();
            ViewResult result = controller.Index(null) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Update()
        {
            DBCaller.IsTest = true;
            HttpContext.Current = TestHelper.TestHttpContext();
            SessionHelper.SetUser(TestHelper.TestUser);

            RestaurantController controller = new RestaurantController();
            ViewResult result = controller.Update(0) as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
