using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Data.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.Data.IntegrationTests
{
    [TestClass]
    public class UserDataManagerTest : DataManagerTestBase
    {
        private List<int> FillUsers()
        {
            var ids = new List<int>
            {
                InsertUser("freddy"),
                InsertUser("five"),
                InsertUser("fab")
            };
            return ids;
        }
        private int InsertUser(string username)
        {
            var insertSql = "insert into Users(username) values ('{0}'); select @@IDENTITY;";
            return this.ExecuteScalar<int>(string.Format(insertSql, username));
        }

        [TestMethod]
        public void CreateUserAsync_Success()
        {
            var sut = new UserDataManager(DbContext);
            var result = sut.CreateUserAsync("testuser").Result;
            Assert.IsTrue(result.Id > 0);
            Assert.IsTrue(result.UserName.Equals("testuser"));
            var count = this.ExecuteScalar<int>("select count(*) from users where username='testuser'");
            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void CreateUserAsync_DuplicateUser_ThrowsException()
        {
            var sut = new UserDataManager(DbContext);
            var result = sut.CreateUserAsync("testuser").Result;
            Assert.IsTrue(result.Id > 0);
            Assert.ThrowsException<AggregateException>(() => { result = sut.CreateUserAsync("testuser").Result; });
        }

        [TestMethod]
        public void GetUsersAsync_Paged_AreOrderedByUserName()
        {
            var ids = FillUsers();
            var sut = new UserDataManager(DbContext);
            var result = sut.GetUsersAsync(1, 3).Result.ToList();
            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0].UserName, "fab");
            Assert.AreEqual(result[1].UserName, "five");
            Assert.AreEqual(result[2].UserName, "freddy");
        }
        
        [TestMethod]
        public void UserExistAsyn_Exists_ReturnsTrue()
        {
            var ids = FillUsers();
            var sut = new UserDataManager(DbContext);
            var result = sut.UserExistAsync(ids.First()).Result;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserExistAsync_DNE_ReturnsFalse()
        {
            var ids = FillUsers();
            var sut = new UserDataManager(DbContext);
            var result = sut.UserExistAsync(ids.Max() + 1).Result;
            Assert.IsFalse(result);
        }
    }
}
