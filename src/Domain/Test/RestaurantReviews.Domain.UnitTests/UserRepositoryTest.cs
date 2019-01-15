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
    }
}
