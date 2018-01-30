using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewReview.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;
using RestaurantReview.BusinessLogic.Models;
using RestaurantReview.Data.Entities;

namespace ReviewReview.Tests.Controllers
{
    [TestClass]
    public class ReviewControllerTest
    {
        [TestMethod]
        public void GetReviewTest_ReviewIDEquals1_ReturnsSuccess_Review_AllPropertiesEqualUnitTestRecord()
        {
            // setting initial data
            int reviewID = 1;

            ReviewController controller = new ReviewController();

            // get review
            IHttpActionResult actionResult = controller.GetReview(reviewID);

            var contentResult = actionResult as OkNegotiatedContentResult<Review>;
            Review reviewResult = contentResult.Content;

            // verify record values match expected value
            Assert.AreEqual("TestComments", reviewResult.comments);
            Assert.AreEqual(3, reviewResult.restaurantID);
            Assert.AreEqual(5, reviewResult.rating);
        }

        [TestMethod]
        public void GetReviewsTest_ReturnsSuccess_AcquiresListOfReviews()
        {
            ReviewController controller = new ReviewController();

            // acquire all reviews
            IHttpActionResult actionResult = controller.GetReviews();

            var contentResult = actionResult as OkNegotiatedContentResult<List<Review>>;

            // verify result is a List<Review>. Success even if list is empty, as the database could be
            Assert.IsNotNull(contentResult);

            List<Review> reviewsResult = contentResult.Content;

            Assert.IsNotNull(reviewsResult);
        }

        [TestMethod]
        public void AddAndRemoveReviewsTest_ReturnsSuccess_AddNewReview_ReviewIDEquals3_VerifyAdditionExists_DeleteNewReview_VerifyAdditionDoesNotExist()
        {
            // setting initial data
            string testPropertyValue = "MyTestReview";

            ReviewController controller = new ReviewController();

            // creating new review context
            ReviewContext context = new ReviewContext();
            context.rating = 5;
            context.restaurantID = 3;
            context.comments = "TestComments";
            context.userName = "correalf01@gmail.com";

            IHttpActionResult postReviewActionResult = controller.Post(context);

            var contentResult = postReviewActionResult as OkNegotiatedContentResult<Review>;

            Assert.IsNotNull(contentResult);

            // capturing new review id
            int newReviewID = contentResult.Content.id;

            // getting full record of new review
            IHttpActionResult getReviewActionResult = controller.GetReview(newReviewID);

            var getReviewContentResult = getReviewActionResult as OkNegotiatedContentResult<Review>;

            Review reviewResult = getReviewContentResult.Content;

            // validating posted data matches with record
            Assert.AreEqual(context.rating, 5);
            Assert.AreEqual(context.restaurantID, 3);
            Assert.AreEqual(context.comments, "TestComments");
            Assert.AreEqual(context.userName, "correalf01@gmail.com");

            // deleting new record
            IHttpActionResult deleteReviewActionResult = controller.Delete(newReviewID);

            var deleteReviewContentResult = deleteReviewActionResult as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(deleteReviewContentResult);

            // capturing id of deleted record
            int deletedRecordID = deleteReviewContentResult.Content;

            // validating new and deleted record ids match
            Assert.AreEqual(newReviewID, deletedRecordID);

            // attempting to get record of deleted review
            IHttpActionResult getDeletedReviewActionResult = controller.GetReview(newReviewID);

            // validated that attempt to get deleted review failed and returns a bad request message
            Assert.IsInstanceOfType(getDeletedReviewActionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void PutReviewTest_ReviewIDEquals3_UpdateReviewNameToPutTest_ReturnsSuccess_VerifyRecordIsUpdated_ResetToPreviousValue()
        {
            // setting initial data
            int reviewID = 1;
            string previousPropertyValue = "TestComments";
            string newPropertyValue = "PutTest";

            // building review context
            ReviewContext context = new ReviewContext();
            context.rating = 5;
            context.restaurantID = 3;
            context.comments = newPropertyValue;
            context.userName = "correalf01@gmail.com";

            ReviewController controller = new ReviewController();

            // updating review record
            IHttpActionResult putActionResult = controller.Put(reviewID, context);

            var putContentResult = putActionResult as OkNegotiatedContentResult<Review>;

            Assert.IsNotNull(putContentResult);

            Review reviewResult = putContentResult.Content;

            // validating record is updated
            Assert.AreEqual(reviewResult.comments, newPropertyValue);

            context.comments = previousPropertyValue;

            // returning record to previous value
            IHttpActionResult putRestoreActionResult = controller.Put(reviewID, context);

            var putRestorContentResult = putRestoreActionResult as OkNegotiatedContentResult<Review>;

            Assert.IsNotNull(putRestorContentResult);

            // validating restore worked
            Assert.AreEqual(putRestorContentResult.Content.comments, previousPropertyValue);
        }
    }
}
