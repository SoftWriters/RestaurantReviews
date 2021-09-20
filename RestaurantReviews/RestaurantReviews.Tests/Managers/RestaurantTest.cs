using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Tests.Managers
{
    [TestClass]
    public class RestaurantTest
    {
        [TestMethod]
        public void Index()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            List<RestaurantInfoModel> restaurants = RestaurantManager.GetAllRestaurants();

            Assert.IsNotNull(restaurants);
            Assert.AreEqual(restaurants.Count, 0);
        }

        [TestMethod]
        public void Insert()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");

            int firstRestaurantID = RestaurantManager.InsertRestaurant(firstUserID, "First Restaurant", "First City", "First Description");
            int secondRestaurantID = RestaurantManager.InsertRestaurant(firstUserID, "Second Restaurant", "Second City", "Second Description");
            List<RestaurantInfoModel> restaurants = RestaurantManager.GetAllRestaurants();
            Assert.IsNotNull(restaurants);
            Assert.AreEqual(restaurants.Count, 2);

            int thirdRestaurantID = RestaurantManager.InsertRestaurant(secondUserID, "Third Restaurant", "First City", "Third Description");
            int fourthRestaurantID = RestaurantManager.InsertRestaurant(secondUserID, "Fourth Restaurant", "Second City", "Fourth Description");
            restaurants = RestaurantManager.GetAllRestaurants();
            Assert.IsNotNull(restaurants);
            Assert.AreEqual(restaurants.Count, 4);

            RestaurantInfoModel restaurant = RestaurantManager.GetRestaurant(firstRestaurantID);
            Assert.AreEqual(restaurant.ID, firstRestaurantID);
            Assert.AreEqual(restaurant.UserID, firstUserID);
            Assert.AreEqual(restaurant.Name, "First Restaurant");
            Assert.AreEqual(restaurant.City, "First City");
            Assert.AreEqual(restaurant.Description, "First Description");
            Assert.AreEqual(restaurant.UserDisplayName, "First User1");

            restaurant = RestaurantManager.GetRestaurant(secondRestaurantID);
            Assert.AreEqual(restaurant.ID, secondRestaurantID);
            Assert.AreEqual(restaurant.UserID, firstUserID);
            Assert.AreEqual(restaurant.Name, "Second Restaurant");
            Assert.AreEqual(restaurant.City, "Second City");
            Assert.AreEqual(restaurant.Description, "Second Description");
            Assert.AreEqual(restaurant.UserDisplayName, "First User1");

            restaurant = RestaurantManager.GetRestaurant(thirdRestaurantID);
            Assert.AreEqual(restaurant.ID, thirdRestaurantID);
            Assert.AreEqual(restaurant.UserID, secondUserID);
            Assert.AreEqual(restaurant.Name, "Third Restaurant");
            Assert.AreEqual(restaurant.City, "First City");
            Assert.AreEqual(restaurant.Description, "Third Description");
            Assert.AreEqual(restaurant.UserDisplayName, "Second User2");

            restaurant = RestaurantManager.GetRestaurant(fourthRestaurantID);
            Assert.AreEqual(restaurant.ID, fourthRestaurantID);
            Assert.AreEqual(restaurant.UserID, secondUserID);
            Assert.AreEqual(restaurant.Name, "Fourth Restaurant");
            Assert.AreEqual(restaurant.City, "Second City");
            Assert.AreEqual(restaurant.Description, "Fourth Description");
            Assert.AreEqual(restaurant.UserDisplayName, "Second User2");

            restaurants = RestaurantManager.GetRestaurants(0, "First City");
            Assert.IsNotNull(restaurants);
            Assert.AreEqual(restaurants.Count, 2);
            Assert.AreEqual(restaurants[0].ID, firstRestaurantID);
            Assert.AreEqual(restaurants[1].ID, thirdRestaurantID);

            restaurants = RestaurantManager.GetRestaurants(0, "Second City");
            Assert.IsNotNull(restaurants);
            Assert.AreEqual(restaurants.Count, 2);
            Assert.AreEqual(restaurants[0].ID, secondRestaurantID);
            Assert.AreEqual(restaurants[1].ID, fourthRestaurantID);
        }

        [TestMethod]
        public void Update()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");
            int firstRestaurantID = RestaurantManager.InsertRestaurant(firstUserID, "First Restaurant", "First City", "First Description");
            int secondRestaurantID = RestaurantManager.InsertRestaurant(firstUserID, "Second Restaurant", "Second City", "Second Description");
            int thirdRestaurantID = RestaurantManager.InsertRestaurant(secondUserID, "Third Restaurant", "First City", "Third Description");
            int fourthRestaurantID = RestaurantManager.InsertRestaurant(secondUserID, "Fourth Restaurant", "Second City", "Fourth Description");

            // Invalid update (wrong user) - don't update
            int result = RestaurantManager.UpdateRestaurant(thirdRestaurantID, firstUserID, "Third Restaurant Updated", "Another City", "Third Description Updated");
            Assert.AreEqual(result, -1);
            RestaurantInfoModel restaurant = RestaurantManager.GetRestaurant(thirdRestaurantID);
            Assert.AreEqual(restaurant.ID, thirdRestaurantID);
            Assert.AreEqual(restaurant.UserID, secondUserID);
            Assert.AreEqual(restaurant.Name, "Third Restaurant");
            Assert.AreEqual(restaurant.City, "First City");
            Assert.AreEqual(restaurant.Description, "Third Description");
            Assert.AreEqual(restaurant.UserDisplayName, "Second User2");

            // Valid update
            result = RestaurantManager.UpdateRestaurant(thirdRestaurantID, secondUserID, "Third Restaurant Updated", "Another City", "Third Description Updated");
            Assert.AreEqual(result, thirdRestaurantID);
            restaurant = RestaurantManager.GetRestaurant(thirdRestaurantID);
            Assert.AreEqual(restaurant.ID, thirdRestaurantID);
            Assert.AreEqual(restaurant.UserID, secondUserID);
            Assert.AreEqual(restaurant.Name, "Third Restaurant Updated");
            Assert.AreEqual(restaurant.City, "Another City");
            Assert.AreEqual(restaurant.Description, "Third Description Updated");
            Assert.AreEqual(restaurant.UserDisplayName, "Second User2");

            // Non-updated restaurant
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
