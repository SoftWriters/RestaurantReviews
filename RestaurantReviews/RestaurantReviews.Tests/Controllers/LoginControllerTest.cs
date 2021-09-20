using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Controllers;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using System.Web;
using System.Web.Mvc;

namespace RestaurantReviews.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void Index()
        {
            DBCaller.IsTest = true;
            HttpContext.Current = TestHelper.TestHttpContext();

            LoginController controller = new LoginController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(SessionHelper.UserID, 0);
        }
    }
}
