using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Tests.Managers
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void Index()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            List<UserModel> users = UserManager.GetAllUsers();

            Assert.IsNotNull(users);
            Assert.AreEqual(users.Count, 0);
        }

        [TestMethod]
        public void Insert()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            List<UserModel> users = UserManager.GetAllUsers();
            Assert.IsNotNull(users);
            Assert.AreEqual(users.Count, 1);

            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");
            users = UserManager.GetAllUsers();
            Assert.IsNotNull(users);
            Assert.AreEqual(users.Count, 2);

            UserModel currentUser = UserManager.GetUser(firstUserID);
            Assert.AreEqual(currentUser.ID, firstUserID);
            Assert.AreEqual(currentUser.Username, "first_user");
            Assert.AreEqual(currentUser.FirstName, "First");
            Assert.AreEqual(currentUser.LastName, "User1");

            currentUser = UserManager.GetUser(secondUserID);
            Assert.AreEqual(currentUser.ID, secondUserID);
            Assert.AreEqual(currentUser.Username, "second_user");
            Assert.AreEqual(currentUser.FirstName, "Second");
            Assert.AreEqual(currentUser.LastName, "User2");
        }

        [TestMethod]
        public void Update()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");

            UserManager.UpdateUser(secondUserID, "second_user_updated", "Second_Updated", "User2_Updated", "second_password_updated");
            UserModel currentUser = UserManager.GetUser(secondUserID);
            Assert.AreEqual(currentUser.Username, "second_user_updated");
            Assert.AreEqual(currentUser.FirstName, "Second_Updated");
            Assert.AreEqual(currentUser.LastName, "User2_Updated");

            currentUser = UserManager.GetUser(firstUserID);
            Assert.AreEqual(currentUser.ID, firstUserID);
            Assert.AreEqual(currentUser.Username, "first_user");
            Assert.AreEqual(currentUser.FirstName, "First");
            Assert.AreEqual(currentUser.LastName, "User1");

            UserManager.UpdateUser(firstUserID, "first_user_updated", "First_Updated", "User1_Updated", "first_password_updated");
            currentUser = UserManager.GetUser(firstUserID);
            Assert.AreEqual(currentUser.ID, firstUserID);
            Assert.AreEqual(currentUser.Username, "first_user_updated");
            Assert.AreEqual(currentUser.FirstName, "First_Updated");
            Assert.AreEqual(currentUser.LastName, "User1_Updated");
        }

        [TestMethod]
        public void Login()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");

            KeyValuePair<int, UserModel> result = UserManager.UserLogin("bad_user", "bad_pass");
            Assert.AreEqual(result.Key, 0);

            result = UserManager.UserLogin("first_user", "bad_pass");
            Assert.AreEqual(result.Key, 0);
            result = UserManager.UserLogin("second_user", "bad_pass");
            Assert.AreEqual(result.Key, 0);
            result = UserManager.UserLogin("first_user", "second_password");
            Assert.AreEqual(result.Key, 0);
            result = UserManager.UserLogin("second_user", "first_password");
            Assert.AreEqual(result.Key, 0);

            result = UserManager.UserLogin("first_user", "first_password");
            Assert.AreEqual(result.Key, firstUserID);
            Assert.AreEqual(result.Value.Username, "first_user");
            Assert.AreEqual(result.Value.FirstName, "First");
            Assert.AreEqual(result.Value.LastName, "User1");

            result = UserManager.UserLogin("second_user", "second_password");
            Assert.AreEqual(result.Key, secondUserID);
            Assert.AreEqual(result.Value.Username, "second_user");
            Assert.AreEqual(result.Value.FirstName, "Second");
            Assert.AreEqual(result.Value.LastName, "User2");

            result = UserManager.UserLogin("first_user", "second_password");
            Assert.AreEqual(result.Key, 0);
            result = UserManager.UserLogin("second_user", "first_password");
            Assert.AreEqual(result.Key, 0);
        }
    }
}
