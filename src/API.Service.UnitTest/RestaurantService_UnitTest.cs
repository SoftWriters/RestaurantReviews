/******************************************************************************
 * Name: RestaurantService_UnitTest.cs
 * Purpose: Unit Test cases for Restaurant Service
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Service;
using RestaurantReviews.API.Test.MockRepository;

namespace RestaurantReviews.API.Service.UnitTest
{
    [TestClass()]
    public class RestaurantService_UnitTest
    {
        IRestaurantRepository restaurantRespoistory;
        IRestaurantService restaurantService;

        [TestInitialize]
        public void Setup()
        {
            restaurantRespoistory = new RestaurantMockRepository();
            restaurantService = new RestaurantService(restaurantRespoistory);
        }

        [TestMethod]
        public void Get_By_City()
        {
            string city = "Pittsburgh";
            RestaurantModelList restaurantList = restaurantService.GetRestaurants(city);
            Assert.AreNotEqual<RestaurantModelList>(restaurantList, null);
            Assert.AreNotEqual<List<RestaurantModelDTO>>(restaurantList.RestaurantList, null);
            foreach (RestaurantModelDTO restaurant in restaurantList.RestaurantList)
            {
                Assert.AreNotEqual<RestaurantModelDTO>(restaurant, null);
                Assert.AreEqual<string>(restaurant.City, city);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Get_By_City_Invalid_Null()
        {
            RestaurantModelList restaurantList = restaurantService.GetRestaurants(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Get_By_City_Invalid_Empty()
        {
            RestaurantModelList restaurantList = restaurantService.GetRestaurants(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Get_By_City_Invalid_MaxString()
        {
            RestaurantModelList restaurantList = restaurantService.GetRestaurants("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
        }

        [TestMethod]
        public void Get_By_City_NonExistent()
        {
            RestaurantModelList restaurantList = restaurantService.GetRestaurants("Tampa");
            Assert.AreNotEqual<RestaurantModelList>(restaurantList, null);
            Assert.AreNotEqual<List<RestaurantModelDTO>>(restaurantList.RestaurantList, null);
            Assert.AreEqual<int>(restaurantList.RestaurantList.Count, 0);
        }

        [TestMethod]
        public void Get_By_Id()
        {
            RestaurantModelDTO restaurant = restaurantService.GetRestaurantById(1);
            Assert.AreNotEqual<RestaurantModelDTO>(restaurant, null);
            Assert.AreEqual<int>(restaurant.Id, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Get_By_Id_Invalid_Negative()
        {
            RestaurantModelDTO restaurant = restaurantService.GetRestaurantById(int.MinValue);
        }

        [TestMethod]
        public void Get_By_Id_NonExistent()
        {
            RestaurantModelDTO restaurant = restaurantService.GetRestaurantById(int.MaxValue);
            Assert.AreEqual<RestaurantModelDTO>(restaurant, null);
        }

        [TestMethod]
        public void Add()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = "Restaurant 5",
                City = "Columbus"
            };
            RestaurantModelDTO newRestaurantRet = restaurantService.AddRestaurant(newRestaurant);
            Assert.AreNotEqual<RestaurantModelDTO>(newRestaurantRet, null);
            Assert.IsTrue(newRestaurantRet.Id > 0 && newRestaurant.Id < int.MaxValue);
            Assert.AreEqual<string>(newRestaurantRet.Name, newRestaurant.Name);
            Assert.AreEqual<string>(newRestaurantRet.City, newRestaurant.City);

            RestaurantModelDTO restaurant = restaurantService.GetRestaurantById(5);
            Assert.AreNotEqual<RestaurantModelDTO>(restaurant, null);
            RestaurantModelList restaurantList = restaurantService.GetRestaurants("Columbus");
            Assert.AreNotEqual<RestaurantModelList>(restaurantList, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Invalid_Name_Null()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = null,
                City = "Columbus"
            };
            RestaurantModelDTO newRestaurantRet = restaurantService.AddRestaurant(newRestaurant);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Invalid_Name_Empty()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = string.Empty,
                City = "Columbus"
            };
            RestaurantModelDTO newRestaurantRet = restaurantService.AddRestaurant(newRestaurant);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Invalid_City_Null()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = "Restaurant 6",
                City = null
            };
            RestaurantModelDTO newRestaurantRet = restaurantService.AddRestaurant(newRestaurant);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Invalid_City_Empty()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = "Restaurant 6",
                City = string.Empty
            };
            RestaurantModelDTO newRestaurantRet = restaurantService.AddRestaurant(newRestaurant);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Invalid_Name_City_Null()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = null,
                City = null
            };
            RestaurantModelDTO newRestaurantRet = restaurantService.AddRestaurant(newRestaurant);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Invalid_Name_City_Empty()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = string.Empty,
                City = string.Empty
            };
            RestaurantModelDTO newRestaurantRet = restaurantService.AddRestaurant(newRestaurant);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Add_Already_Exists()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = "Restaurant 1",
                City = "Pittsburgh"
            };
            RestaurantModelDTO newRestaurantRet = restaurantService.AddRestaurant(newRestaurant);
        }
    }
}
