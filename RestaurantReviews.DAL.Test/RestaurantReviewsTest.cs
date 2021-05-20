using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.DAL;
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
            moq.SetupGet((a) => (List<Restaurant>)a.GetRestaurants("Pittsburgh")).Returns(list);

            //act
        }
    }
}
