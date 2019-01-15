using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.Common;
using RestaurantReviews.Data;
using RestaurantReviews.Entity;

namespace RestaurantReviews.Domain.UnitTests
{
    [TestClass]
    public class RestaurantRepositoryTest
    {
        [TestMethod]
        public void CreateRestaurantAsync_Test()
        {
            var restaurantDataManager = new Mock<IRestaurantDataManager>();
            var expected = new Restaurant()
            {
                Name = "seafood and such",
                Address = "123 bay street",
                City = "seaside",
                Id = 1
            };
            Restaurant actual = null;
            restaurantDataManager.Setup(x => x.CreateRestaurantAsync(It.IsAny<Restaurant>())).Callback<Restaurant>(x => { actual = x; }).Returns(Task.FromResult<Restaurant>(expected));
            
            var restaurantRepo = new RestaurantRepository(restaurantDataManager.Object);
            var result = restaurantRepo.CreateRestaurantAsync(expected.Name, expected.Address, expected.City).Result;
            restaurantDataManager.VerifyAll();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.Address, actual.Address);
        }

        [TestMethod]
        public void DeleteRestaurantAsync_Test()
        {
            var restaurantDataManager = new Mock<IRestaurantDataManager>();
            restaurantDataManager.Setup(x => x.DeleteRestaurantAsync(123)).Returns(Task.CompletedTask);

            var restaurantRepo = new RestaurantRepository(restaurantDataManager.Object);
            restaurantRepo.DeleteRestaurantAsync(123).Wait();
            restaurantDataManager.VerifyAll();            
        }

        [TestMethod]
        public void GetRestaurantAsync_Test()
        {
            var restaurantDataManager = new Mock<IRestaurantDataManager>();
            var expected = new Restaurant()
            {
                Name = "seafood and such",
                Address = "123 bay street",
                City = "seaside",
                Id = 4556
            };
            restaurantDataManager.Setup(x => x.GetRestaurantAsync(expected.Id)).Returns(Task.FromResult<Restaurant>(expected));

            var restaurantRepo = new RestaurantRepository(restaurantDataManager.Object);
            var actual = restaurantRepo.GetRestaurantAsync(expected.Id).Result;
            restaurantDataManager.VerifyAll();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.Address, actual.Address);
        }

        [TestMethod]
        public void GetRestaurantsAsync_Test()
        {
            var restaurantDataManager = new Mock<IRestaurantDataManager>();
            var expected = new Restaurant[]{
            new Restaurant{
                Name = "seafood and such",
                Address = "123 bay street",
                City = "seaside",
                Id = 4556
            },
            new Restaurant
            {
                Name = "donuts r better",
                Address = "0 icing circle",
                City = "seaside",
                Id = 4557
            }}.ToList();
            var expectedPage = 1;
            var expectedPageSize = 20;
            var expectedDbFilter = new DbFilter<Restaurant>{ Field = "City", Operator = OperatorEnum.Like, Value = "sea%" };
            restaurantDataManager.Setup(x => x.GetRestaurantsAsync(expectedPage, expectedPageSize, expectedDbFilter)).Returns(Task.FromResult<IEnumerable<Restaurant>>(expected));

            var restaurantRepo = new RestaurantRepository(restaurantDataManager.Object);
            var actual = restaurantRepo.GetRestaurantsAsync(expectedPage, expectedPageSize, expectedDbFilter).Result;
            restaurantDataManager.VerifyAll();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
