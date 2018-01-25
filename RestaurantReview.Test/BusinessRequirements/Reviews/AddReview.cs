using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReview.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.Test.Requirements
{
    [TestClass]
    public class AddReview : Base.ReviewBase
    {
        [TestInitialize]
        public new void Initialize()
        {
            base.Initialize();
        }

        [TestMethod]
        public void AddReviewTest()
        {
            //Arrange
            RestaurantReview.BL.Model.Review restaurant = 
                new RestaurantReview.BL.Model.Review() { Id = 1, RestaurantID = 0, UserID = 0, Rating = 1, Comments = "test", DateCreated = DateTime.Now };

            //should be done with stored procedure, demonstrating it doesn't have to be
            _mockContext.Setup(m => m.ReviewAdd(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
            {
                return 0;
            });


            //Act
            //Could return ID here.  No real need at this point.
            int rID = _service.Add(restaurant, 1);

            //Assert
            Assert.AreEqual(0, rID);
        }
    }
}
