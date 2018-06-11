/******************************************************************************
 * Name: RestaurantController_UnitTest.cs
 * Purpose: Unit Tests cases for Restaurant MVC Controller
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Service;
using RestaurantReviews.API.Controllers;
using RestaurantReviews.API.Test.MockRepository;

namespace RestaurantReviews.API.UnitTest
{
    [TestClass]
    public class RestaurantController_UnitTest
    {
        IRestaurantRepository restaurantRespoistory;
        IRestaurantService restaurantService;
        RestaurantController restaurantController;

        [TestInitialize]
        public void Setup()
        {
            restaurantRespoistory = new RestaurantMockRepository();
            restaurantService = new RestaurantService(restaurantRespoistory);
            restaurantController = new RestaurantController(restaurantService);
        }

        [TestMethod]
        public void Get_By_City()
        {
            string city = "Pittsburgh";
            APIResponseDTO response = restaurantController.Get(city);
            Assert.AreNotEqual<APIResponseDTO>(response, null);
            RestaurantModelList restaurantList = (RestaurantModelList)response.Data;
            Assert.AreNotEqual<RestaurantModelList>(restaurantList, null);
            Assert.AreNotEqual<List<RestaurantModelDTO>>(restaurantList.RestaurantList, null);
            foreach(RestaurantModelDTO restaurant in restaurantList.RestaurantList)
            {
                Assert.AreNotEqual<RestaurantModelDTO>(restaurant, null);
                Assert.AreEqual<string>(restaurant.City, city);
            }
        }

        [TestMethod]
         public void Get_By_City_Invalid_Null()
        {
            APIResponseDTO response = restaurantController.Get(null);
            Assert.AreNotEqual<APIResponseDTO>(response, null);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod]
        public void Get_By_City_Invalid_Empty()
        {
            APIResponseDTO response = restaurantController.Get(string.Empty);
            Assert.AreNotEqual<APIResponseDTO>(response, null);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod]
        public void Get_By_City_Invalid_MaxString()
        {
            APIResponseDTO response = restaurantController.Get("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
            Assert.AreNotEqual<APIResponseDTO>(response, null);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod]
        public void Get_By_City_NonExistent()
        {
            APIResponseDTO response = restaurantController.Get("Tampa");
            Assert.AreNotEqual<APIResponseDTO>(response, null);
            RestaurantModelList restaurantList = (RestaurantModelList)response.Data;
            Assert.AreNotEqual<RestaurantModelList>(restaurantList, null);
            Assert.AreNotEqual<List<RestaurantModelDTO>>(restaurantList.RestaurantList, null);
            Assert.AreEqual<int>(restaurantList.RestaurantList.Count, 0);
        }

        [TestMethod]
        public void Get_By_Id()
        {
            APIResponseDTO response = restaurantController.Get(1);
            Assert.AreNotEqual<APIResponseDTO>(response, null);
            RestaurantModelDTO restaurant = (RestaurantModelDTO)response.Data;
            Assert.AreNotEqual<RestaurantModelDTO>(restaurant, null);
            Assert.AreEqual<int>(restaurant.Id, 1);
        }

        [TestMethod]
        public void Get_By_Id_Invalid_Negative()
        {
            APIResponseDTO response = restaurantController.Get(int.MinValue);
            Assert.AreNotEqual<APIResponseDTO>(response, null);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod]
        public void Get_By_Id_NonExistent()
        {
            APIResponseDTO response = restaurantController.Get(int.MaxValue);
            Assert.AreNotEqual<APIResponseDTO>(response, null);
            Assert.AreEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod]
        public void Add()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = "Restaurant 5",
                City = "Columbus"
            };
            RestaurantAPIRequestDTO request = new RestaurantAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newRestaurant
            };
            APIResponseDTO response = restaurantController.Post(request);
            RestaurantModelDTO newRestaurantRet = (RestaurantModelDTO)response.Data;
            Assert.AreNotEqual<RestaurantModelDTO>(newRestaurantRet, null);
            Assert.IsTrue(newRestaurantRet.Id > 0 && newRestaurant.Id < int.MaxValue);
            Assert.AreEqual<string>(newRestaurantRet.Name, newRestaurant.Name);
            Assert.AreEqual<string>(newRestaurantRet.City, newRestaurant.City);

            response = restaurantController.Get(5);
            RestaurantModelDTO restaurant = (RestaurantModelDTO)response.Data;
            Assert.AreNotEqual<RestaurantModelDTO>(restaurant, null);
            response = restaurantController.Get("Columbus");
            RestaurantModelList restaurantList = (RestaurantModelList)response.Data;
            Assert.AreNotEqual<RestaurantModelList>(restaurantList, null);
        }

        [TestMethod]
        public void Add_Invalid_Name_Null()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = null,
                City = "Columbus"
            };
            RestaurantAPIRequestDTO request = new RestaurantAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newRestaurant
            };
            APIResponseDTO response = restaurantController.Post(request);
            RestaurantModelDTO newRestaurantRet  = (RestaurantModelDTO)response.Data;
            Assert.AreEqual<RestaurantModelDTO>(newRestaurantRet, null);
        }

        [TestMethod]
        public void Add_Invalid_Name_Empty()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = string.Empty,
                City = "Columbus"
            };
            RestaurantAPIRequestDTO request = new RestaurantAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newRestaurant
            };
            APIResponseDTO response = restaurantController.Post(request);
            RestaurantModelDTO newRestaurantRet = (RestaurantModelDTO)response.Data;
            Assert.AreEqual<RestaurantModelDTO>(newRestaurantRet, null);
        }

        [TestMethod]
        public void Add_Invalid_City_Null()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = "Restaurant 6",
                City = null
            };
            RestaurantAPIRequestDTO request = new RestaurantAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newRestaurant
            };
            APIResponseDTO response = restaurantController.Post(request);
            RestaurantModelDTO newRestaurantRet = (RestaurantModelDTO)response.Data;
            Assert.AreEqual<RestaurantModelDTO>(newRestaurantRet, null);
        }

        [TestMethod]
        public void Add_Invalid_City_Empty()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = "Restaurant 6",
                City = string.Empty
            };
            RestaurantAPIRequestDTO request = new RestaurantAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newRestaurant
            };
            APIResponseDTO response = restaurantController.Post(request);
            RestaurantModelDTO newRestaurantRet = (RestaurantModelDTO)response.Data;
            Assert.AreEqual<RestaurantModelDTO>(newRestaurantRet, null);
        }

        [TestMethod]
        public void Add_Invalid_Name_City_Null()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = null,
                City = null
            };
            RestaurantAPIRequestDTO request = new RestaurantAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newRestaurant
            };
            APIResponseDTO response = restaurantController.Post(request);
            RestaurantModelDTO newRestaurantRet = (RestaurantModelDTO)response.Data;
            Assert.AreEqual<RestaurantModelDTO>(newRestaurantRet, null);
        }

        [TestMethod]
        public void Add_Invalid_Name_City_Empty()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = string.Empty,
                City = string.Empty
            };
            RestaurantAPIRequestDTO request = new RestaurantAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newRestaurant
            };
            APIResponseDTO response = restaurantController.Post(request);
            RestaurantModelDTO newRestaurantRet = (RestaurantModelDTO)response.Data;
            Assert.AreEqual<RestaurantModelDTO>(newRestaurantRet, null);
        }

        [TestMethod]
        public void Add_Already_Exists()
        {
            RestaurantModelDTO newRestaurant = new RestaurantModelDTO()
            {
                Name = "Restaurant 1",
                City = "Pittsburgh"
            };
            RestaurantAPIRequestDTO request = new RestaurantAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newRestaurant
            };
            APIResponseDTO response = restaurantController.Post(request);
            RestaurantModelDTO newRestaurantRet = (RestaurantModelDTO)response.Data;
            Assert.AreEqual<RestaurantModelDTO>(newRestaurantRet, null);
        }
    }
}
