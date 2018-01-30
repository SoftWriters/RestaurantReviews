using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReview;
using RestaurantReview.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using RestaurantReview.BusinessLogic.Controllers;
using System.Net;
using RestaurantReview.BusinessLogic.Models;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;
using RestaurantReview.Data.Entities;

namespace RestaurantReview.Tests.Controllers
{
    [TestClass]
    public class RestaurantControllerTest
    {
        [TestMethod]
        public void GetRestaurantTest_RestaurantIDEquals3_ReturnsSuccess_Restaurant_AllPropertiesEqualUnitTestRecord()
        {
            // setting initial data
            int restaurantID = 3;
            string expectedPropertyValue = "UnitTestRecord";
            
            RestaurantController controller = new RestaurantController();

            // get restaurant
            IHttpActionResult actionResult = controller.GetRestaurant(restaurantID);

            var contentResult = actionResult as OkNegotiatedContentResult<Restaurant>;
            Restaurant restaurantResult = contentResult.Content;
            
            // verify record values match expected value
            Assert.AreEqual(expectedPropertyValue, restaurantResult.city);
            Assert.AreEqual(expectedPropertyValue, restaurantResult.country);
            Assert.AreEqual(expectedPropertyValue, restaurantResult.name);
            Assert.AreEqual(expectedPropertyValue, restaurantResult.state);
            Assert.AreEqual(expectedPropertyValue, restaurantResult.streetAddress);
            Assert.AreEqual(expectedPropertyValue, restaurantResult.thumbnailBase64);
            Assert.AreEqual(expectedPropertyValue, restaurantResult.zipcode);
        }

        [TestMethod]
        public void GetRestaurantsTest_ReturnsSuccess_AcquiresListOfRestaurants()
        {
            RestaurantController controller = new RestaurantController();

            // acquire all restaurants
            IHttpActionResult actionResult = controller.GetRestaurants();

            var contentResult = actionResult as OkNegotiatedContentResult<List<Restaurant>>;

            // verify result is a List<Restaurant>. Success even if list is empty, as the database could be
            Assert.IsNotNull(contentResult);

            List<Restaurant> restaurantsResult = contentResult.Content;
            
            Assert.IsNotNull(restaurantsResult);
        }

        [TestMethod]
        public void AddAndRemoveRestaurantsTest_ReturnsSuccess_AddNewRestaurant_VerifyAdditionExists_DeleteNewRestaurant_VerifyAdditionDoesNotExist()
        {
            // setting initial data
            string testPropertyValue = "MyTestRestaurant";

            RestaurantController controller = new RestaurantController();

            // creating new restaurant context
            RestaurantContext context = new RestaurantContext();
            context.city = testPropertyValue;
            context.country = testPropertyValue;
            context.name = testPropertyValue;
            context.state = testPropertyValue;
            context.streetAddress = testPropertyValue;
            context.thumbnailBase64 = testPropertyValue;
            context.zipcode = testPropertyValue;

            IHttpActionResult postRestaurantActionResult = controller.Post(context);

            var contentResult = postRestaurantActionResult as OkNegotiatedContentResult<Restaurant>;
            
            Assert.IsNotNull(contentResult);

            // capturing new restaurant id
            int newRestaurantID = contentResult.Content.id;

            // getting full record of new restaurant
            IHttpActionResult getRestaurantActionResult = controller.GetRestaurant(newRestaurantID);

            var getRestaurantContentResult = getRestaurantActionResult as OkNegotiatedContentResult<Restaurant>;

            Restaurant restaurantResult = getRestaurantContentResult.Content;

            // validating posted data matches with record
            Assert.AreEqual(testPropertyValue, restaurantResult.city);
            Assert.AreEqual(testPropertyValue, restaurantResult.country);
            Assert.AreEqual(testPropertyValue, restaurantResult.name);
            Assert.AreEqual(testPropertyValue, restaurantResult.state);
            Assert.AreEqual(testPropertyValue, restaurantResult.streetAddress);
            Assert.AreEqual(testPropertyValue, restaurantResult.thumbnailBase64);
            Assert.AreEqual(testPropertyValue, restaurantResult.zipcode);

            // deleting new record
            IHttpActionResult deleteRestaurantActionResult = controller.Delete(newRestaurantID);

            var deleteRestaurantContentResult = deleteRestaurantActionResult as OkNegotiatedContentResult<int>;
            
            Assert.IsNotNull(deleteRestaurantContentResult);

            // capturing id of deleted record
            int deletedRecordID = deleteRestaurantContentResult.Content;

            // validating new and deleted record ids match
            Assert.AreEqual(newRestaurantID, deletedRecordID);

            // attempting to get record of deleted restaurant
            IHttpActionResult getDeletedRestaurantActionResult = controller.GetRestaurant(newRestaurantID);

            // validated that attempt to get deleted restaurant failed and returns a bad request message
            Assert.IsInstanceOfType(getDeletedRestaurantActionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void PutRestaurantTest_RestaurantIDEquals3_UpdateRestaurantNameToPutTest_ReturnsSuccess_VerifyRecordIsUpdated_ResetToPreviousValue()
        {
            // setting initial data
            int restaurantID = 3;
            string previousPropertyValue = "UnitTestRecord";
            string newPropertyValue = "PutTest";

            // building restaurant context
            RestaurantContext context = new RestaurantContext();
            context.city = previousPropertyValue;
            context.country = previousPropertyValue;
            context.name = newPropertyValue;
            context.state = previousPropertyValue;
            context.streetAddress = previousPropertyValue;
            context.thumbnailBase64 = previousPropertyValue;
            context.zipcode = previousPropertyValue;

            RestaurantController controller = new RestaurantController();

            // updating restaurant record
            IHttpActionResult putActionResult = controller.Put(restaurantID, context);

            var putContentResult = putActionResult as OkNegotiatedContentResult<Restaurant>;

            Assert.IsNotNull(putContentResult);

            Restaurant restaurantResult = putContentResult.Content;

            // validating record is updated
            Assert.AreEqual(restaurantResult.name, newPropertyValue);

            context.name = previousPropertyValue;

            // returning record to previous value
            IHttpActionResult putRestoreActionResult = controller.Put(restaurantID, context);

            var putRestorContentResult = putRestoreActionResult as OkNegotiatedContentResult<Restaurant>;

            Assert.IsNotNull(putRestorContentResult);

            // validating restore worked
            Assert.AreEqual(putRestorContentResult.Content.name, previousPropertyValue);
        }
    }
}