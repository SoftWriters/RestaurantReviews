using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Common;
using RestaurantReviews.Entity;
namespace RestaurantReviews.Common.Tests
{
    [TestClass]
    public class DbFilterTest
    {
        [TestMethod]
        public void Field_DoesNotExist_ThrowsException()
        {
            var sut = new DbFilter<Restaurant>() { Field = "testit", Operator = OperatorEnum.Equal, Value = "123" };
            Assert.ThrowsException<Exceptions.InvalidFilterFieldException>(() =>
            sut.GetFilterSql("filterval"));
        }

        [TestMethod]
        public void IntField_Equal_ShouldHaveCorrectSql()
        {
            var sut = new DbFilter<Restaurant>() { Field = "Id", Operator = OperatorEnum.Equal, Value = "123" };
            var filterString = sut.GetFilterSql("filterval");
            Assert.AreEqual(" Id = @filterval", filterString);
        }

        [TestMethod]
        public void StringField_Like_ShouldHaveCorrectSql()
        {
            var sut = new DbFilter<Restaurant>() { Field = "City", Operator = OperatorEnum.Like, Value = "Pitts%" };
            var filterString = sut.GetFilterSql("filterval");
            Assert.AreEqual(" City like @filterval", filterString);
        }
    }
}
