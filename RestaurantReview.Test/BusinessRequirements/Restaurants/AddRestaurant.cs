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
    public class AddRestaurant : Base.RestaurantBase
    {

        [TestInitialize]
        public new void Initialize()
        {
            base.Initialize();
        }

        [TestMethod]
        public void AddRestaurantTest()
        {
            //Arrange
            int Id = 1;
            RestaurantReview.BL.Model.Restaurant restaurant = new RestaurantReview.BL.Model.Restaurant() { Name = "Red Lobster", CityID = 1 };

            //should be done with stored procedure, demonstrating it doesn't have to be
            _mockSet.Setup(m => m.Add(It.IsAny<Restaurant>())).Returns((Restaurant e) =>
            {
                e.Id = Id;
                return e;
            });


            //Act
            int rID = _service.Add(restaurant, 1);

            //Assert
            Assert.AreEqual(Id, rID);

            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
