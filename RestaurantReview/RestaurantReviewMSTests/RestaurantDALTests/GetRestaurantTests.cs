using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReview.DAL;
using RestaurantReview.Services;

namespace RestaurantReviewMSTests
{
    [TestClass]
    public class GetRestaurantsTests
    {
        public IConn connection;
        public GetRestaurantsTests(IConn conn)
        {
            this.connection = conn;
        }

        [TestMethod]
        public void GetRestaurantsTests_ReturnsCorrectData()
        {
            var sut = new RestaurantsDAL(connection.AWSconnstring());
        }
    }
}
