using RestaurantReview.DAL;
using RestaurantReview.Models;
using RestaurantReview.Services;
using System.Linq;
using Xunit;

namespace RestaurantReviewTests.DALTests
{
    public class UsersDALTests
    {
        [Fact]
        public void GetUsers_ReturnsList()
        {
            var dal = new UserDAL(new Conn().AWSconnstring());
            var result = dal.GetUsers();
            Assert.True(result.Count > 1);
        }

        [Fact]
        public void GetUser_ReturnsSingleUser()
        {
            var dal = new UserDAL(new Conn().AWSconnstring());

            var result = dal.GetUsers().FirstOrDefault();

            Assert.IsType<User>(result);
        }
    }
}