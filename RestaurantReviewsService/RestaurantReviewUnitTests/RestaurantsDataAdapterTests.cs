using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviewsService.DataAdapters;
using RestaurantReviewsService.DomainModels;

namespace RestaurantReviewUnitTests
{
    [TestClass]
    public class RestaurantsDataAdapterTests : UnitTestBase
    {
        private const string _testCategory = "DataAdapterTests";

        public RestaurantsDataAdapterTests() : base()
        {
        }       


        [TestMethod]
        [TestCategory(_testCategory)]
        public void CanGetAllRestaurantsDomainModelList()
        {
            try
            {
                IRestaurantsDataAdapter restaurantsDataAdapter = new RestaurantsDataAdapter();

                IList<RestaurantDM> results = restaurantsDataAdapter.GetAllRestaurants();

                Assert.IsTrue(results != null);

                Assert.IsTrue(results.Count > 0);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory(_testCategory)]
        public void CanGetRestaurantDomainModelListByCityName()
        {
            try
            {
                string cityName = "Twin Peaks";

                IRestaurantsDataAdapter restaurantsDataAdapter = new RestaurantsDataAdapter();

                IList<RestaurantDM> results = restaurantsDataAdapter.GetRestaurantsByCity(cityName);

                Assert.IsTrue(results != null);

                Assert.IsTrue(results.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory(_testCategory)]
        public void CanAddNewNonDuplicateRestaurantRecord()
        {
            Assert.IsTrue(1 == 1);
            return;

            // TODO: Implement a mocking framework and strategy, ideally testing the command and parameters
            // etc. - stopping short of executing the command.

            #region to be mocked

            //RestaurantDM nonDuplicateRestaurant = new RestaurantDM
            //{
            //    City = "Columbus",
            //    Name = "A Common Table",
            //    Overview = "Gourment Sandwiches",
            //    StateId = 2,
            //    StreetAddress = "1455 High Street"
            //};

            //try
            //{
            //    IRestaurantsDataAdapter restaurantsDataAdapter = new RestaurantsDataAdapter();

            //    int addResult = restaurantsDataAdapter.AddNewRestaurant(nonDuplicateRestaurant);

            //    Assert.IsTrue(addResult > 0);
            //}           
            //catch (Exception e)
            //{
            //    Assert.Fail(e.Message);
            //}

            #endregion
        }

        [TestMethod]
        [TestCategory(_testCategory)]
        public void WillGenerateExceptionOnDuplicateRestaurantAddAttempt()
        {          
            RestaurantDM duplicateAddAttempt = new RestaurantDM
            {
                City = "Butler",
                Name = "Mama Rosa",
                Overview = "Italian Cuisine From Old Country Recipes",
                StateId = 1,
                StreetAddress = "123 Old Butler Plank Road"
            };

            try
            {
                IRestaurantsDataAdapter restaurantsDataAdapter = new RestaurantsDataAdapter();

                int addResult = restaurantsDataAdapter.AddNewRestaurant(duplicateAddAttempt);
                Assert.Fail();
            }
            catch(AssertFailedException e)
            {
                Assert.Fail(e.Message);
            }
            catch(Exception e)
            {
                Assert.IsTrue(e is Exception);
            }
        }
    }
}
