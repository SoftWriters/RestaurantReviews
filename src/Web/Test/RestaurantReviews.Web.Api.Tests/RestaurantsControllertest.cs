using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.Common;
using RestaurantReviews.Domain;
using RestaurantReviews.Entity;
using RestaurantReviews.Web.Api.Controllers;
using RestaurantReviews.Web.Api.Models;

namespace RestaurantReviews.Web.Api.Tests
{
    [TestClass]
    public class RestaurantsControllerTest
    {
        [TestMethod]
        public void Get_DefaultParams_ReturnsList()
        {
            IEnumerable<Restaurant> expected = new List<Restaurant>{
                new Restaurant {
                    Id = 1,
                    Name = "test"
                },
                new Restaurant {
                    Id = 2,
                    Name = "test2"
                }
            };
            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(x => x.GetRestaurantsAsync(1, 1000, null)).Returns(Task.FromResult<IEnumerable<Restaurant>>(expected));
            var sut = new RestaurantsController(restaurantRepository.Object);
            var actual = sut.Get(null, null, null).Result;
            Assert.AreEqual(expected, actual);
            restaurantRepository.VerifyAll();
        }

        [TestMethod]
        public void Get_ExplicitParams_ReturnsList()
        {
            IEnumerable<Restaurant> expected = new List<Restaurant>{
                new Restaurant {
                    Id = 1,
                    Name = "test"
                },
                new Restaurant {
                    Id = 2,
                    Name = "test2"
                }
            };
            var restaurantRepository = new Mock<IRestaurantRepository>();
            DbFilter<Restaurant> actualFilter = null;
            restaurantRepository.Setup(x => x.GetRestaurantsAsync(3, 2, It.IsAny<DbFilter<Restaurant>>()))
                .Callback<int, int, DbFilter<Restaurant>>((page, pagesize, filter)=> { actualFilter = filter; })
                .Returns(Task.FromResult(expected));
            var sut = new RestaurantsController(restaurantRepository.Object);

            var actual = sut.Get(3, 2, "TestCity").Result;

            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(actualFilter);
            Assert.AreEqual("City", actualFilter.Field);
            Assert.AreEqual(OperatorEnum.Equal, actualFilter.Operator);
            Assert.AreEqual("TestCity", actualFilter.Value);
            restaurantRepository.VerifyAll();
        }

        [TestMethod]
        public void Search_DefaultParams_ReturnsList()
        {
            IEnumerable<Restaurant> expected = new List<Restaurant>{
                new Restaurant {
                    Id = 1,
                    Name = "test"
                },
                new Restaurant {
                    Id = 2,
                    Name = "test2"
                }
            };
            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(x => x.GetRestaurantsAsync(1, 1000, null)).Returns(Task.FromResult<IEnumerable<Restaurant>>(expected));
            var sut = new RestaurantsController(restaurantRepository.Object);
            var actual = sut.Post(null, null, null).Result;
            Assert.AreEqual(expected, actual);
            restaurantRepository.VerifyAll();
        }

        [TestMethod]
        public void Search_ExplicitParams_ReturnsList()
        {
            IEnumerable<Restaurant> expected = new List<Restaurant>{
                new Restaurant {
                    Id = 1,
                    Name = "test"
                },
                new Restaurant {
                    Id = 2,
                    Name = "test2"
                }
            };
            var restaurantRepository = new Mock<IRestaurantRepository>();
            DbFilter<Restaurant> actualFilter = null;
            restaurantRepository.Setup(x => x.GetRestaurantsAsync(3, 2, It.IsAny<DbFilter<Restaurant>>()))
                .Callback<int, int, DbFilter<Restaurant>>((page, pagesize, filter) => { actualFilter = filter; })
                .Returns(Task.FromResult<IEnumerable<Restaurant>>(expected));
            var filterParam = new FilterParam { Field = "Name", Operator = OperatorEnum.Like, Value = "Burger*" };

            var sut = new RestaurantsController(restaurantRepository.Object);
            var actual = sut.Post(filterParam, 3, 2).Result;

            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(actualFilter);
            Assert.AreEqual("Name", actualFilter.Field);
            Assert.AreEqual(OperatorEnum.Like, actualFilter.Operator);
            Assert.AreEqual("Burger*", actualFilter.Value);
            restaurantRepository.VerifyAll();
        }


        [TestMethod]
        public void Get_CallsRepository_ReturnsRestaurant()
        {
            var expected = new Restaurant
            {
                Id = 1,
                Name = "test"
            };
            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(x => x.GetRestaurantAsync(123)).Returns(Task.FromResult<Restaurant>(expected));

            var sut = new RestaurantsController(restaurantRepository.Object);
            var actual = sut.Get(123).Result;

            Assert.AreEqual(expected, actual);
            restaurantRepository.VerifyAll();
        }

        [TestMethod]
        public void Post_CallsRepository_ReturnsRestaurant()
        {
            var expected = new Restaurant
            {
                Id = 1,
                Name = "test",
                Address = "123 somestreet",
                City = "FakeCity"
            };

            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(x => x.CreateRestaurantAsync(expected.Name, expected.Address, expected.City)).Returns(Task.FromResult<Restaurant>(expected));

            var sut = new RestaurantsController(restaurantRepository.Object);
            var actual = sut.Post(expected).Result;

            Assert.AreEqual(expected, actual);
            restaurantRepository.VerifyAll();
        }

        [TestMethod]
        public void Delete_CallsRepository_Returns()
        {
            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(x => x.DeleteRestaurantAsync(123)).Returns(Task.CompletedTask);

            var sut = new RestaurantsController(restaurantRepository.Object);
            sut.Delete(123).Wait();

            restaurantRepository.VerifyAll();
        }
    }
}
