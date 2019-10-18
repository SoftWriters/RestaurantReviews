using System;
using Xunit;
using RestaurantReview.Models;
using RestaurantReview.DAL;
using RestaurantReview.Services;

namespace RestaurantReviewTests.RestaurantsDALTests
{
    public class GetRestaurantsTest
    {
        public IConn connection;
        public GetRestaurantsTest(IConn conn)
        {
            this.connection = conn;
        }
        [Fact]
        public void GetRestaurantsTests_ReturnsCorrectData()
        {
            var sut = new RestaurantsDAL(connection.AWSconnstring());
        }
    }
}
