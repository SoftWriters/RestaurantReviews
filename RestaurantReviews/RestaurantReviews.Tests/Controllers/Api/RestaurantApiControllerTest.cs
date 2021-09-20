using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Controllers.Api;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;
using System.Collections.Generic;
using System.Web;

namespace RestaurantReviews.Tests.Controllers
{
    [TestClass]
    public class RestaurantApiControllerTest
    {
        [TestMethod]
        public void Put()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();
            HttpContext.Current = TestHelper.TestHttpContext();

            UserController userController = new UserController();
            RestaurantController controller = new RestaurantController();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");

            // Insert - not logged in - no insert
            SessionHelper.Clear();
            int result = controller.Put(new RestaurantModel { ID = 0, Name = "First Restaurant", City = "First City", Description = "First Description" });
            Assert.AreEqual(result, -1);
            List<RestaurantInfoModel> restaurants = RestaurantManager.GetAllRestaurants();
            Assert.IsNotNull(restaurants);
            Assert.AreEqual(restaurants.Count, 0);

            // Inserts as first user
            userController.Login(new LoginModel() { Username = "first_user", Password = "first_password" });
            int firstRestaurantID = controller.Put(new RestaurantModel { ID = 0, Name = "First Restaurant", City = "First City", Description = "First Description" });
            int secondRestaurantID = controller.Put(new RestaurantModel { ID = 0, Name = "Second Restaurant", City = "Second City", Description = "Second Description" });

            // Inserts as second user
            SessionHelper.Clear();
            userController.Login(new LoginModel() { Username = "second_user", Password = "second_password" });
            int thirdRestaurantID = controller.Put(new RestaurantModel { ID = 0, Name = "Third Restaurant", City = "First City", Description = "Third Description" });
            int fourthRestaurantID = controller.Put(new RestaurantModel { ID = 0, Name = "Fourth Restaurant", City = "Second City", Description = "Fourth Description" });

            restaurants = RestaurantManager.GetAllRestaurants();
            Assert.IsNotNull(restaurants);
            Assert.AreEqual(restaurants.Count, 4);

            // Invalid update - wrong user - don't update
            SessionHelper.Clear();
            userController.Login(new LoginModel() { Username = "first_user", Password = "first_password" });
            result = controller.Put(new RestaurantModel
            {
                ID = thirdRestaurantID,
                Name = "Third Restaurant Updated",
                City = "Another City",
                Description = "Third Description Updated"
            });
            Assert.AreEqual(result, -1);
            RestaurantInfoModel restaurant = RestaurantManager.GetRestaurant(thirdRestaurantID);
            Assert.AreEqual(restaurant.ID, thirdRestaurantID);
            Assert.AreEqual(restaurant.UserID, secondUserID);
            Assert.AreEqual(restaurant.Name, "Third Restaurant");
            Assert.AreEqual(restaurant.City, "First City");
            Assert.AreEqual(restaurant.Description, "Third Description");
            Assert.AreEqual(restaurant.UserDisplayName, "Second User2");

            // Valid update
            SessionHelper.Clear();
            userController.Login(new LoginModel() { Username = "second_user", Password = "second_password" });

            result = controller.Put(new RestaurantModel
            { 
                ID = thirdRestaurantID, 
                Name = "Third Restaurant Updated", 
                City = "Another City", 
                Description = "Third Description Updated"
            });
            Assert.AreEqual(result, thirdRestaurantID);
            restaurant = RestaurantManager.GetRestaurant(thirdRestaurantID);
            Assert.AreEqual(restaurant.ID, thirdRestaurantID);
            Assert.AreEqual(restaurant.UserID, secondUserID);
            Assert.AreEqual(restaurant.Name, "Third Restaurant Updated");
            Assert.AreEqual(restaurant.City, "Another City");
            Assert.AreEqual(restaurant.Description, "Third Description Updated");
            Assert.AreEqual(restaurant.UserDisplayName, "Second User2");

            // Non-updated restaurant (same as previously inserted data)
            restaurant = RestaurantManager.GetRestaurant(secondRestaurantID);
            Assert.AreEqual(restaurant.ID, secondRestaurantID);
            Assert.AreEqual(restaurant.UserID, firstUserID);
            Assert.AreEqual(restaurant.Name, "Second Restaurant");
            Assert.AreEqual(restaurant.City, "Second City");
            Assert.AreEqual(restaurant.Description, "Second Description");
            Assert.AreEqual(restaurant.UserDisplayName, "First User1");
        }
    }
}
