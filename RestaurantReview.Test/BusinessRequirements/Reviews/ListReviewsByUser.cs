using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.Test.Requirements
{
    [TestClass]
    public class ListReviewsByUsers : Base.ReviewBase
    {

        [TestInitialize]
        public new void Initialize()
        {
            base.Initialize();
        }

        [TestMethod]
        public void ListReviewsByUsersTest()
        {
            _mockContext.Setup(m => m.ReviewsGetByUser(It.IsAny<int>()))
                .Returns(() =>
                {
                    List<DAL.Entity.Review> r = new List<DAL.Entity.Review>();

                    r.Add(new DAL.Entity.Review() { Id = 1, RestaurantID = 0, UserID = 1, Rating = 1, Comments = "test", DateCreated = DateTime.Now });
                    r.Add(new DAL.Entity.Review() { Id = 1, RestaurantID = 0, UserID = 1, Rating = 1, Comments = "test", DateCreated = DateTime.Now });

                    return r;
                });

            //Act
            List<RestaurantReview.BL.Model.Review> results = _service.GetByUserId(1).ToList() as List<RestaurantReview.BL.Model.Review>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }
    }
}
