using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.Domain;
using RestaurantReviews.Entity;
using RestaurantReviews.Web.Api.Controllers;

namespace RestaurantReviews.Web.Api.Tests
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void Post_CallsRepository_ReturnsNewUser()
        {
            var expected = new User()
            {
                Id = 1,
                UserName = "test"
            };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.CreateUserAsync(expected.UserName)).Returns(Task.FromResult<User>(expected));
            var sut = new UsersController(userRepository.Object);
            var actual = sut.Post(expected.UserName).Result;
            Assert.AreEqual(expected, actual);
            userRepository.VerifyAll();
        }

        [TestMethod]
        public void Get_DefaultParams_ReturnsList()
        {
            IEnumerable<User> expected = new List<User>{
            new User {
                Id = 1,
                UserName = "test"
            },
            new User {
                Id = 2,
                UserName = "test2"
            }

                };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUsersAsync(1, 1000)).Returns(Task.FromResult<IEnumerable<User>>(expected));
            var sut = new UsersController(userRepository.Object);
            var actual = sut.Get(null, null).Result;
            Assert.AreEqual(expected, actual);
            userRepository.VerifyAll();
        }

        [TestMethod]
        public void Get_ExplicitParams_ReturnsList()
        {
            IEnumerable<User> expected = new List<User>{
            new User {
                Id = 1,
                UserName = "test"
            },
            new User {
                Id = 2,
                UserName = "test2"
            }

                };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUsersAsync(2, 100)).Returns(Task.FromResult<IEnumerable<User>>(expected));
            var sut = new UsersController(userRepository.Object);
            var actual = sut.Get(2, 100).Result;
            Assert.AreEqual(expected, actual);
            userRepository.VerifyAll();
        }
    }
}
