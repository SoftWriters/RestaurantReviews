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
    public class DeleteReview : Base.ReviewBase
    {
        [TestInitialize]
        public new void Initialize()
        {
            base.Initialize();
        }

        [TestMethod]
        public void DeleteReviewTest()
        {
            //Arrange
            RestaurantReview.BL.Model.Review restaurant =
                new RestaurantReview.BL.Model.Review() { Id = 1, RestaurantID = 0, UserID = 0, Rating = 1, Comments = "test", DateCreated = DateTime.Now };

            //should be done with stored procedure, demonstrating it doesn't have to be
            _mockSet.Setup(m => m.Remove(It.IsAny<DAL.Entity.Review>())).Returns(() =>
            {
                return null;
            });


            //Act
            _service.Delete(restaurant, 1);

            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
