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
    public class RestaurantManagerTests
    {
        [TestMethod()]
        public void CreateRestaurantTest()
        {
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            Restaurant retrievedrestaurant = RestaurantManager.GetRestaurant(restaurant.Id);

            Assert.AreEqual(restaurant.Id, retrievedrestaurant.Id);
            Assert.AreEqual(restaurant.Name, retrievedrestaurant.Name);
        }

        [TestMethod()]
        public void UpdateRestaurantTest()
        {
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            Restaurant update = RestaurantManager.UpdateRestaurant(restaurant.Id, "name2");

            Restaurant retrieved = RestaurantManager.GetRestaurant(restaurant.Id);

            Assert.AreEqual(restaurant.Id, retrieved.Id);
            Assert.AreEqual("name2", retrieved.Name);
        }

        [TestMethod()]
        public void GetRestaurantTest()
        {
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            Restaurant retrieved = RestaurantManager.GetRestaurant(restaurant.Id);

            Assert.AreEqual(restaurant.Id, retrieved.Id);
            Assert.AreEqual(restaurant.Name, retrieved.Name);
        }

        [TestMethod()]
        public void GetRestaurantsByCityRegionTest()
        {
            string city = Guid.NewGuid().ToString();
            string region = Guid.NewGuid().ToString();

            //create 3 restaurants with address in the city/region
            RestaurantAddressManager.CreateRestaurantAddress(RestaurantManager.CreateRestaurant("rest1").Id, null, null, city, region, null);
            RestaurantAddressManager.CreateRestaurantAddress(RestaurantManager.CreateRestaurant("rest2").Id, null, null, city, region, null);
            RestaurantAddressManager.CreateRestaurantAddress(RestaurantManager.CreateRestaurant("rest3").Id, null, null, city, region, null);

            //full name search
            List<Restaurant> fullcitysearch = RestaurantManager.GetRestaurantsByCityRegion(city, region);
            List<Restaurant> partialcitysearch = RestaurantManager.GetRestaurantsByCityRegion(city.Substring(0,4), region.Substring(0, 5));

            Assert.AreEqual(3, fullcitysearch.Count);
            Assert.AreEqual(3, partialcitysearch.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(RestaurantReviews.Entities.Data.RetrievalException))]
        public void DeleteRestaurantTest()
        {
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            RestaurantManager.DeleteRestaurant(restaurant.Id);

            Restaurant retrieved = RestaurantManager.GetRestaurant(restaurant.Id);

            Assert.Fail();
        }
    }
}