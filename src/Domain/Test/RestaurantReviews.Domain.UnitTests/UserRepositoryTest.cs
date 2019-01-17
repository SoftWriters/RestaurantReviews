using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.Data;
using RestaurantReviews.Entity;

namespace RestaurantReviews.Domain.UnitTests
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void CreateUserAsync_CallsDataManager()
        {
            var userDataManager = new Mock<IUserDataManager>();
            userDataManager.Setup(x => x.CreateUserAsync("testuser")).Returns(Task.FromResult<User>(new User() { UserName="testuser", Id=1 }));

            var userRepo = new UserRepository(userDataManager.Object);
            var result = userRepo.CreateUserAsync("testuser").Result;
            userDataManager.VerifyAll();
        }

        [TestMethod]
        public void GetUsersAsync_Test()
        {
            var userDataManager = new Mock<IUserDataManager>();
            var expected = new User[]{
            new User{
                UserName = "Joe Dirt",
                Id = 4556
            },
            new User
            {
                UserName = "Brandy",
                Id = 4557
            }}.ToList();
            var expectedPage = 1;
            var expectedPageSize = 20;
            userDataManager.Setup(x => x.GetUsersAsync(expectedPage, expectedPageSize)).Returns(Task.FromResult<IEnumerable<User>>(expected));

            var restaurantRepo = new UserRepository(userDataManager.Object);
            var actual = restaurantRepo.GetUsersAsync(expectedPage, expectedPageSize).Result;
            userDataManager.VerifyAll();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserExistsAsync_Test()
        {
            var userDataManager = new Mock<IUserDataManager>();
            
            var expectedUserId = 11;
            userDataManager.Setup(x => x.UserExistAsync(expectedUserId)).Returns(Task.FromResult(true));

            var restaurantRepo = new UserRepository(userDataManager.Object);
            var actual = restaurantRepo.UserExistsAsync(11).Result;
            userDataManager.VerifyAll();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual);
        }
    }
}
