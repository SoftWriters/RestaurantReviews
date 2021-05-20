using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestarauntReviews.Controllers;
using RestaurantReviews.DAL;
using RestaurantReviews.DAL.DTO;
using System.Collections.Generic;

namespace RestaurantReviews.DAL.Test
{
    [TestClass]
    public class RestaurantReviewsTest
    {
        [TestMethod]
        public void GetRestarauntTest()
        {
            //arrange
            var list = new List<Restaurant>();
            list.Add(new Restaurant() { BusinessName = "Tai Pei", PriceRatings = "Economical", RestaurantId = 1 });
            var moq = new Moq.Mock<RestaurantReviewDAL>();
            moq.Setup((a) => (List<Restaurant>)a.GetRestaurants("Pittsburgh")).Returns(list);

            //act
            var controller = new RestaurantReviewsController(new Logger<RestaurantReviewsController>(),moq.Object);
        }
    }
}
