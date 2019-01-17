using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Common;
using RestaurantReviews.Data.Tests;
using RestaurantReviews.Entity;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.Data.IntegrationTests
{
    [TestClass]
    public class RestaurantDataManagerTest : DataManagerTestBase
    {
        private List<int> FillRestaurants()
        {
            var ids = new List<int>
            {
                InsertRestaurant("cool cones", "123 milky way", "ChocoCity"),
                InsertRestaurant("burgatory", "456 beef alley", "Burgertown"),
                InsertRestaurant("candy R us", "222 cavity street", "ChocoCity"),
                InsertRestaurant("cupcakes for everyone", "300 pound highway", "ChocoCity")
            };
            return ids;
        }
        private int InsertRestaurant(string name, string address, string city)
        {
            var insertSql = "insert into Restaurant(name, address, city) values ('{0}', '{1}', '{2}'); select @@IDENTITY;";
            return this.ExecuteScalar<int>(string.Format(insertSql, name, address, city));
        }

        [TestMethod]
        public void GetRestaurantsAsync_Paged_AreOrderedByName()
        {
            var ids = FillRestaurants();
            var sut = new RestaurantDataManager(DbContext);
            var result = sut.GetRestaurantsAsync(1, 2, null).Result.ToList();
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Name, "burgatory");
            Assert.AreEqual(result[1].Name, "candy R us");

            result = sut.GetRestaurantsAsync(2, 2, null).Result.ToList();
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Name, "cool cones");
            Assert.AreEqual(result[1].Name, "cupcakes for everyone");
        }

        [TestMethod]
        public void GetRestaurantsAsync_NoFilter_ReturnsAll()
        {
            var ids = FillRestaurants();
            var sut = new RestaurantDataManager(DbContext);
            var result = sut.GetRestaurantsAsync(1, 100, null).Result.ToList();
            Assert.AreEqual(result.Count(), 4);
        }

        [TestMethod]
        public void GetRestaurantsAsync_FilterByCity_ReturnsAll()
        {
            var ids = FillRestaurants();
            var sut = new RestaurantDataManager(DbContext);
            var filter = new DbFilter<Restaurant> { Field = "City", Operator = OperatorEnum.Equal, Value = "ChocoCity" };
            var result = sut.GetRestaurantsAsync(1, 100, filter).Result.ToList();
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        public void CreateRestaurantAsync_Success()
        {
            var restaurantToCreate = new Restaurant() { Name = "Cool Cones", Address = "123 milkshake way", City = "Chocoburgh" };
            var sut = new RestaurantDataManager(DbContext);
            var result = sut.CreateRestaurantAsync(restaurantToCreate).Result;
            Assert.IsTrue(result.Id > 0);
            Assert.AreEqual(result.Name, "Cool Cones");
            using (var reader = this.ExecuteReader("select * from Restaurant where Id=@Id", System.Data.CommandType.Text,
                new System.Data.SqlClient.SqlParameter() { ParameterName = "Id", SqlDbType = System.Data.SqlDbType.Int, Value = result.Id }))
            {
                reader.Read();
                var actualName = reader.GetValue(reader.GetOrdinal("Name"));
                var actualCity = reader.GetValue(reader.GetOrdinal("City"));
                Assert.AreEqual(restaurantToCreate.Name, actualName);
                Assert.AreEqual(restaurantToCreate.City, actualCity);

            }

        }

        [TestMethod]
        public void DeleteRestaurantAsync_Success()
        {
            var ids = FillRestaurants();
            var sut = new RestaurantDataManager(DbContext);
            sut.DeleteRestaurantAsync(ids.First()).Wait();
            var count = this.ExecuteScalar<int>(string.Format("select count(*) from Restaurant where Id={0}", ids.First()));
            Assert.AreEqual(count, 0);
        }

        //public async Task<Restaurant> GetRestaurantAsync(int id)
        [TestMethod]
        public void GetRestaurantAsync_Success()
        {
            var ids = FillRestaurants();
            var sut = new RestaurantDataManager(DbContext);
            var result = sut.GetRestaurantAsync(ids.First()).Result;
            Assert.AreEqual(result.Id, ids.First());
            Assert.AreEqual(result.Name, "cool cones");
            Assert.AreEqual(result.Address, "123 milky way");
            Assert.AreEqual(result.City, "ChocoCity");
            
        }

    }
}
