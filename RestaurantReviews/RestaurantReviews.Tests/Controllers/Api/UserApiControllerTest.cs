using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Controllers.Api;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;
using System.Web;

namespace RestaurantReviews.Tests.Controllers
{
    [TestClass]
    public class UserApiControllerTest
    {
        [TestMethod]
        public void Login()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();
            HttpContext.Current = TestHelper.TestHttpContext();

            SessionHelper.Clear();
            UserController controller = new UserController();
            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");

            // Check failure of invalid logins
            int result = controller.Login(new LoginModel() { Username = "first_user", Password = "second_password" });
            Assert.AreEqual(result, 0);
            result = controller.Login(new LoginModel() { Username = "second_user", Password = "first_password" });
            Assert.AreEqual(result, 0);

            // Check that session is updated on valid login
            result = controller.Login(new LoginModel() { Username = "second_user", Password = "second_password" });
            UserModel sessionUser = SessionHelper.GetUser();
            Assert.AreEqual(sessionUser.ID, secondUserID);
            Assert.AreEqual(sessionUser.Username, "second_user");
            Assert.AreEqual(sessionUser.FirstName, "Second");
            Assert.AreEqual(sessionUser.LastName, "User2");

            // Check that session is not updated on invalid login
            result = controller.Login(new LoginModel() { Username = "first_user", Password = "second_password" });
            Assert.AreEqual(result, 0);
            sessionUser = SessionHelper.GetUser();
            Assert.AreEqual(sessionUser.ID, secondUserID);
            Assert.AreEqual(sessionUser.Username, "second_user");
            Assert.AreEqual(sessionUser.FirstName, "Second");
            Assert.AreEqual(sessionUser.LastName, "User2");
        }

        [TestMethod]
        public void Put()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();
            HttpContext.Current = TestHelper.TestHttpContext();

            SessionHelper.Clear();
            UserController controller = new UserController();

            // Signup - Session as new user
            int firstUserID = controller.Put(new UserModel { ID = 0, FirstName = "First", LastName = "User1", Username = "first_user", Password = "first_Password" });
            Assert.AreNotEqual(firstUserID, 0);
            UserModel sessionUser = SessionHelper.GetUser();
            Assert.AreEqual(sessionUser.ID, firstUserID);
            Assert.AreEqual(sessionUser.Username, "first_user");
            Assert.AreEqual(sessionUser.FirstName, "First");
            Assert.AreEqual(sessionUser.LastName, "User1");

            SessionHelper.Clear();
            int secondUserID = controller.Put(new UserModel { ID = 0, FirstName = "Second", LastName = "User2", Username = "second_user", Password = "second_password" });
            Assert.AreNotEqual(secondUserID, 0);
            Assert.AreEqual(SessionHelper.UserID, secondUserID);

            // Invalid update - not logged in as user
            int result = controller.Put(new UserModel { ID = firstUserID, FirstName = "First Updated", LastName = "User1 Updated", Username = "first_user_updated", Password = "first_password_updated" });
            Assert.AreEqual(result, -1);
            
            // Check that session is updated on user update
            result = controller.Put(new UserModel { ID = secondUserID, FirstName = "Second Updated", LastName = "User2 Updated", Username = "second_user_updated", Password = "second_password_updated" });
            Assert.AreEqual(result, secondUserID);
            sessionUser = SessionHelper.GetUser();
            Assert.AreEqual(sessionUser.ID, secondUserID);
            Assert.AreEqual(sessionUser.Username, "second_user_updated");
            Assert.AreEqual(sessionUser.FirstName, "Second Updated");
            Assert.AreEqual(sessionUser.LastName, "User2 Updated");
        }
    }
}
