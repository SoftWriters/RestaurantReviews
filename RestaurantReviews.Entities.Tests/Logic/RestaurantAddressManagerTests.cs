using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Entities.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities.Logic.Tests
{
    [TestClass()]
    public class RestaurantAddressManagerTests
    {
        [TestMethod()]
        public void CreateRestaurantAddressTest()
        {
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            RestaurantAddress createdaddress = RestaurantAddressManager.CreateRestaurantAddress(restaurant.Id, "street1", "street2", "city", "region", "postalcode");

            RestaurantAddress retrievedaddress = RestaurantAddressManager.GetRestaurantAddress(restaurant.Id, createdaddress.Id);

            Assert.AreEqual(restaurant.Id, retrievedaddress.RestaurantId);
            Assert.AreEqual(createdaddress.Id, retrievedaddress.Id);
            Assert.AreEqual(createdaddress.Street1, retrievedaddress.Street1);
            Assert.AreEqual(createdaddress.Street2, retrievedaddress.Street2);
            Assert.AreEqual(createdaddress.City, retrievedaddress.City);
            Assert.AreEqual(createdaddress.Region, retrievedaddress.Region);
            Assert.AreEqual(createdaddress.PostalCode, retrievedaddress.PostalCode);
        }

        [TestMethod()]
        public void UpdateRestaurantAddressTest()
        {
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            RestaurantAddress createdaddress = RestaurantAddressManager.CreateRestaurantAddress(restaurant.Id, "street1", "street2", "city", "region", "postalcode");

            RestaurantAddressManager.UpdateRestaurantAddress(restaurant.Id, createdaddress.Id, "s1", "s2", "c", "r", "p");

            RestaurantAddress retrievedaddress = RestaurantAddressManager.GetRestaurantAddress(restaurant.Id, createdaddress.Id);

            Assert.AreEqual(restaurant.Id, retrievedaddress.RestaurantId);
            Assert.AreEqual(createdaddress.Id, retrievedaddress.Id);
            Assert.AreEqual("s1", retrievedaddress.Street1);
            Assert.AreEqual("s2", retrievedaddress.Street2);
            Assert.AreEqual("c", retrievedaddress.City);
            Assert.AreEqual("r", retrievedaddress.Region);
            Assert.AreEqual("p", retrievedaddress.PostalCode);
        }

        [TestMethod()]
        public void GetRestaurantAddressTest()
        {
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            RestaurantAddress createdaddress = RestaurantAddressManager.CreateRestaurantAddress(restaurant.Id, "street1", "street2", "city", "region", "postalcode");

            RestaurantAddress retrievedaddress = RestaurantAddressManager.GetRestaurantAddress(restaurant.Id, createdaddress.Id);

            Assert.AreEqual(restaurant.Id, retrievedaddress.RestaurantId);
            Assert.AreEqual(createdaddress.Id, retrievedaddress.Id);
            Assert.AreEqual(createdaddress.Street1, retrievedaddress.Street1);
            Assert.AreEqual(createdaddress.Street2, retrievedaddress.Street2);
            Assert.AreEqual(createdaddress.City, retrievedaddress.City);
            Assert.AreEqual(createdaddress.Region, retrievedaddress.Region);
            Assert.AreEqual(createdaddress.PostalCode, retrievedaddress.PostalCode);
        }

        [TestMethod()]
        public void GetRestaurantAddressesTest()
        {
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            RestaurantAddress address1 = RestaurantAddressManager.CreateRestaurantAddress(restaurant.Id, "street1", "street2", "city", "region", "postalcode");
            RestaurantAddress address2 = RestaurantAddressManager.CreateRestaurantAddress(restaurant.Id, "street1", "street2", "city", "region", "postalcode");

            List<RestaurantAddress> retrievedaddresses = RestaurantAddressManager.GetRestaurantAddresses(restaurant.Id);

            Assert.AreEqual(2, retrievedaddresses.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(RestaurantReviews.Entities.Data.RetrievalException))]
        public void DeleteRestaurantAddressTest()
        {
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            RestaurantAddress address = RestaurantAddressManager.CreateRestaurantAddress(restaurant.Id, "street1", "street2", "city", "region", "postalcode");

            RestaurantAddressManager.DeleteRestaurantAddress(restaurant.Id, address.Id);

            RestaurantAddress retrievedaddress = RestaurantAddressManager.GetRestaurantAddress(restaurant.Id, address.Id);

            Assert.Fail();
        }
    }
}