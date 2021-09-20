using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Controllers;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using System.Web;
using System.Web.Mvc;

namespace RestaurantReviews.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Index()
        {
            DBCaller.IsTest = true;
            HttpContext.Current = TestHelper.TestHttpContext();

            UserController controller = new UserController();
            ViewResult result = controller.Index(0) as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
