using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Data.Tests;
using System;

namespace RestaurantReviews.Data.IntegrationTests
{
    [TestClass]
    public class UserDataManagerTest : DataManagerTestBase
    {
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
    }
}
