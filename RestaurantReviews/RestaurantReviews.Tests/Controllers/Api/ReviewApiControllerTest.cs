using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Controllers.Api;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;
using System.Collections.Generic;
using System.Web;

namespace RestaurantReviews.Tests.Controllers
{
    [TestClass]
    public class ReviewApiControllerTest
    {
        [TestMethod]
        public void Put()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();
            HttpContext.Current = TestHelper.TestHttpContext();

            UserController userController = new UserController();
            ReviewController controller = new ReviewController();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");
            int firstRestaurantID = RestaurantManager.InsertRestaurant(firstUserID, "First Restaurant", "First City", "First Description");
            int secondRestaurantID = RestaurantManager.InsertRestaurant(secondUserID, "Second Restaurant", "Second City", "Second Description");

            // Insert - not logged in - no insert
            SessionHelper.Clear();
            int result = controller.Put(new ReviewModel { ID = 0, RestaurantID = firstRestaurantID, Title = "First/First", Description = "First/First Description" });
            Assert.AreEqual(result, -1);
            List<ReviewInfoModel> reviews = ReviewManager.GetReviews(0, 0, 0);
            Assert.IsNotNull(reviews);
            Assert.AreEqual(reviews.Count, 0);

            // Inserts as first user
            userController.Login(new LoginModel() { Username = "first_user", Password = "first_password" });
            int firstReviewID = controller.Put(new ReviewModel { ID = 0, RestaurantID = firstRestaurantID, Title = "First/First", Description = "First/First Description" });
            int secondReviewID = controller.Put(new ReviewModel { ID = 0, RestaurantID = secondRestaurantID, Title = "First/Second", Description = "First/Second Description" });

            // Inserts as second user
            SessionHelper.Clear();
            userController.Login(new LoginModel() { Username = "second_user", Password = "second_password" });
            int thirdReviewID = controller.Put(new ReviewModel { ID = 0, RestaurantID = firstRestaurantID, Title = "Second/First", Description = "Second/First Description" });
            int fourthReviewID = controller.Put(new ReviewModel { ID = 0, RestaurantID = secondRestaurantID, Title = "Second/Second", Description = "Second/Second Description" });

            reviews = ReviewManager.GetReviews(0, 0, 0);
            Assert.IsNotNull(reviews);
            Assert.AreEqual(reviews.Count, 4);

            // Invalid update - not logged in - don't update
            SessionHelper.Clear();
            result = controller.Put(new ReviewModel { ID = thirdReviewID, RestaurantID = firstRestaurantID, Title = "Third Review Updated", Description = "Third Review Description Updated" });
            Assert.AreEqual(result, -1);
            ReviewInfoModel review = ReviewManager.GetReview(thirdReviewID);
            Assert.AreEqual(review.ID, thirdReviewID);
            Assert.AreEqual(review.UserID, secondUserID);
            Assert.AreEqual(review.RestaurantID, firstRestaurantID);
            Assert.AreEqual(review.Title, "Second/First");
            Assert.AreEqual(review.Description, "Second/First Description");

            // Invalid update (wrong user) - don't update
            SessionHelper.Clear();
            userController.Login(new LoginModel() { Username = "first_user", Password = "first_password" });
            result = controller.Put(new ReviewModel { ID = thirdReviewID, RestaurantID = firstRestaurantID, Title = "Third Review Updated", Description = "Third Review Description Updated" });
            Assert.AreEqual(result, -1);
            review = ReviewManager.GetReview(thirdReviewID);
            Assert.AreEqual(review.ID, thirdReviewID);
            Assert.AreEqual(review.UserID, secondUserID);
            Assert.AreEqual(review.RestaurantID, firstRestaurantID);
            Assert.AreEqual(review.Title, "Second/First");
            Assert.AreEqual(review.Description, "Second/First Description");

            // Valid update
            SessionHelper.Clear();
            userController.Login(new LoginModel() { Username = "second_user", Password = "second_password" });
            result = controller.Put(new ReviewModel { ID = thirdReviewID, RestaurantID = secondRestaurantID, Title = "Third Review Updated", Description = "Third Review Description Updated" });
            Assert.AreEqual(result, thirdReviewID);
            review = ReviewManager.GetReview(thirdReviewID);
            Assert.AreEqual(review.ID, thirdReviewID);
            Assert.AreEqual(review.UserID, secondUserID);
            Assert.AreEqual(review.RestaurantID, secondRestaurantID);
            Assert.AreEqual(review.Title, "Third Review Updated");
            Assert.AreEqual(review.Description, "Third Review Description Updated");

            // Non-updated review
            review = ReviewManager.GetReview(secondReviewID);
            Assert.AreEqual(review.ID, secondReviewID);
            Assert.AreEqual(review.UserID, firstUserID);
            Assert.AreEqual(review.RestaurantID, secondRestaurantID);
            Assert.AreEqual(review.Title, "First/Second");
            Assert.AreEqual(review.Description, "First/Second Description");
        }

        [TestMethod]
        public void Delete()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();
            HttpContext.Current = TestHelper.TestHttpContext();

            UserController userController = new UserController();
            ReviewController controller = new ReviewController();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");
            int firstRestaurantID = RestaurantManager.InsertRestaurant(firstUserID, "First Restaurant", "First City", "First Description");
            int secondRestaurantID = RestaurantManager.InsertRestaurant(secondUserID, "Second Restaurant", "Second City", "Second Description");

            // Inserts as first user
            userController.Login(new LoginModel() { Username = "first_user", Password = "first_password" });
            int firstReviewID = controller.Put(new ReviewModel { ID = 0, RestaurantID = firstRestaurantID, Title = "First/First", Description = "First/First Description" });
            int secondReviewID = controller.Put(new ReviewModel { ID = 0, RestaurantID = secondRestaurantID, Title = "First/Second", Description = "First/Second Description" });

            // Inserts as second user
            SessionHelper.Clear();
            userController.Login(new LoginModel() { Username = "second_user", Password = "second_password" });
            int thirdReviewID = controller.Put(new ReviewModel { ID = 0, RestaurantID = firstRestaurantID, Title = "Second/First", Description = "Second/First Description" });
            int fourthReviewID = controller.Put(new ReviewModel { ID = 0, RestaurantID = secondRestaurantID, Title = "Second/Second", Description = "Second/Second Description" });

            // Invalid delete - not logged in - don't delete
            SessionHelper.Clear();
            int result = controller.Delete(thirdReviewID);
            Assert.AreEqual(result, -1);
            ReviewInfoModel review = ReviewManager.GetReview(thirdReviewID);
            Assert.AreEqual(review.ID, thirdReviewID);

            // Invalid delete (wrong user) - don't delete
            SessionHelper.Clear();
            userController.Login(new LoginModel() { Username = "first_user", Password = "first_password" });
            result = controller.Delete(thirdReviewID);
            Assert.AreEqual(result, -1);
            review = ReviewManager.GetReview(thirdReviewID);
            Assert.AreEqual(review.ID, thirdReviewID);

            // Valid deletes
            result = ReviewManager.DeleteReview(secondReviewID, firstUserID);
            Assert.AreEqual(result, secondReviewID);

            SessionHelper.Clear();
            userController.Login(new LoginModel() { Username = "second_user", Password = "second_password" });
            result = ReviewManager.DeleteReview(thirdReviewID, secondUserID);
            Assert.AreEqual(result, thirdReviewID);

            List<ReviewInfoModel> reviews = ReviewManager.GetReviews(0, 0, 0);
            Assert.AreEqual(reviews.Count, 2);
            Assert.AreEqual(reviews[0].ID, firstReviewID);
            Assert.AreEqual(reviews[1].ID, fourthReviewID);
        }
    }
}
