using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReview.Service.Interface;
using Moq;
using RestaurantReview.DAL.Interface;
using RestaurantReview.DAL.Entity;
using System.Data.Entity;
using RestaurantReview.Service;

namespace RestaurantReview.Test.Requirements
{
    [TestClass]
    public class ListRestaurantsByCity : Base.RestaurantBase
    {

        [TestInitialize]
        public new void Initialize()
        {
            base.Initialize();
        }

        [TestMethod]
        public void ListRestaurantsByCityTest()
        {
            _mockContext.Setup(m => m.RestaurantsGetByCity(It.IsAny<int>()))
                .Returns(() =>
            {
                List<Restaurant> r = new List<Restaurant>();

                r.Add(new Restaurant() { Name = "Test 1", CityID = 1 });
                r.Add(new Restaurant() { Name = "Test 2", CityID = 1 });

                return r;
            });

            //Act
            List<RestaurantReview.BL.Model.Restaurant> results = _service.GetByCityId(1).ToList() as List<RestaurantReview.BL.Model.Restaurant>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }
    }
}
