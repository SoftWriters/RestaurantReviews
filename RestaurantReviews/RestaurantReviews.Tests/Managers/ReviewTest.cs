using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Tests.Managers
{
    [TestClass]
    public class ReviewTest
    {
        [TestMethod]
        public void Index()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            List<ReviewInfoModel> reviews = ReviewManager.GetReviews(0, 0, 0);

            Assert.IsNotNull(reviews);
            Assert.AreEqual(reviews.Count, 0);
        }

        [TestMethod]
        public void Insert()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");
            int firstRestaurantID = RestaurantManager.InsertRestaurant(firstUserID, "First Restaurant", "First City", "First Description");
            int secondRestaurantID = RestaurantManager.InsertRestaurant(secondUserID, "Second Restaurant", "Second City", "Second Description");
            int firstReviewID = ReviewManager.InsertReview(firstUserID, firstRestaurantID, "First/First", "First/First Description");
            int secondReviewID = ReviewManager.InsertReview(firstUserID, secondRestaurantID, "First/Second", "First/Second Description");

            List<ReviewInfoModel> reviews = ReviewManager.GetReviews(0, 0, 0);
            Assert.IsNotNull(reviews);
            Assert.AreEqual(reviews.Count, 2);

            int thirdReviewID = ReviewManager.InsertReview(secondUserID, firstRestaurantID, "Second/First", "Second/First Description");
            int fourthReviewID = ReviewManager.InsertReview(secondUserID, secondRestaurantID, "Second/Second", "Second/Second Description");

            reviews = ReviewManager.GetReviews(0, 0, 0);
            Assert.IsNotNull(reviews);
            Assert.AreEqual(reviews.Count, 4);

            ReviewInfoModel review = ReviewManager.GetReview(firstReviewID);
            Assert.AreEqual(review.ID, firstReviewID);
            Assert.AreEqual(review.UserID, firstUserID);
            Assert.AreEqual(review.RestaurantID, firstRestaurantID);
            Assert.AreEqual(review.Title, "First/First");
            Assert.AreEqual(review.Description, "First/First Description");
            Assert.AreEqual(review.UserDisplayName, "First User1");
            Assert.AreEqual(review.RestaurantName, "First Restaurant");

            review = ReviewManager.GetReview(secondReviewID);
            Assert.AreEqual(review.ID, secondReviewID);
            Assert.AreEqual(review.UserID, firstUserID);
            Assert.AreEqual(review.RestaurantID, secondRestaurantID);
            Assert.AreEqual(review.Title, "First/Second");
            Assert.AreEqual(review.Description, "First/Second Description");
            Assert.AreEqual(review.UserDisplayName, "First User1");
            Assert.AreEqual(review.RestaurantName, "Second Restaurant");

            review = ReviewManager.GetReview(thirdReviewID);
            Assert.AreEqual(review.ID, thirdReviewID);
            Assert.AreEqual(review.UserID, secondUserID);
            Assert.AreEqual(review.RestaurantID, firstRestaurantID);
            Assert.AreEqual(review.Title, "Second/First");
            Assert.AreEqual(review.Description, "Second/First Description");
            Assert.AreEqual(review.UserDisplayName, "Second User2");
            Assert.AreEqual(review.RestaurantName, "First Restaurant");

            review = ReviewManager.GetReview(fourthReviewID);
            Assert.AreEqual(review.ID, fourthReviewID);
            Assert.AreEqual(review.UserID, secondUserID);
            Assert.AreEqual(review.RestaurantID, secondRestaurantID);
            Assert.AreEqual(review.Title, "Second/Second");
            Assert.AreEqual(review.Description, "Second/Second Description");
            Assert.AreEqual(review.UserDisplayName, "Second User2");
            Assert.AreEqual(review.RestaurantName, "Second Restaurant");

            reviews = ReviewManager.GetReviews(0, firstUserID, firstRestaurantID);
            Assert.AreEqual(reviews.Count, 1);
            Assert.AreEqual(reviews[0].ID, firstReviewID);

            reviews = ReviewManager.GetReviews(0, secondUserID, firstRestaurantID);
            Assert.IsNotNull(reviews);
            Assert.AreEqual(reviews.Count, 1);
            Assert.AreEqual(reviews[0].ID, thirdReviewID);

            reviews = ReviewManager.GetReviews(0, secondUserID, 0);
            Assert.IsNotNull(reviews);
            Assert.AreEqual(reviews.Count, 2);
            Assert.AreEqual(reviews[0].ID, thirdReviewID);
            Assert.AreEqual(reviews[1].ID, fourthReviewID);

            reviews = ReviewManager.GetReviews(0, 0, secondRestaurantID);
            Assert.IsNotNull(reviews);
            Assert.AreEqual(reviews.Count, 2);
            Assert.AreEqual(reviews[0].ID, secondReviewID);
            Assert.AreEqual(reviews[1].ID, fourthReviewID);
        }

        [TestMethod]
        public void Update()
        {
            DBCaller.IsTest = true;
            TestManager.ClearTestData();

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");
            int firstRestaurantID = RestaurantManager.InsertRestaurant(firstUserID, "First Restaurant", "First City", "First Description");
            int secondRestaurantID = RestaurantManager.InsertRestaurant(secondUserID, "Second Restaurant", "Second City", "Second Description");
            int firstReviewID = ReviewManager.InsertReview(firstUserID, firstRestaurantID, "First/First", "First/First Description");
            int secondReviewID = ReviewManager.InsertReview(firstUserID, secondRestaurantID, "First/Second", "First/Second Description");
            int thirdReviewID = ReviewManager.InsertReview(secondUserID, firstRestaurantID, "Second/First", "Second/First Description");
            int fourthReviewID = ReviewManager.InsertReview(secondUserID, secondRestaurantID, "Second/Second", "Second/Second Description");

            // Invalid update (wrong user) - don't update
            int result = ReviewManager.UpdateReview(thirdReviewID, firstUserID, secondRestaurantID, "Third Review Updated", "Third Review Description Updated");
            Assert.AreEqual(result, -1);
            ReviewInfoModel review = ReviewManager.GetReview(thirdReviewID);
            Assert.AreEqual(review.ID, thirdReviewID);
            Assert.AreEqual(review.UserID, secondUserID);
            Assert.AreEqual(review.RestaurantID, firstRestaurantID);
            Assert.AreEqual(review.Title, "Second/First");
            Assert.AreEqual(review.Description, "Second/First Description");

            // Valid update
            result = ReviewManager.UpdateReview(thirdReviewID, secondUserID, secondRestaurantID, "Third Review Updated", "Third Review Description Updated");
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

            int firstUserID = UserManager.InsertUser("first_user", "First", "User1", "first_password");
            int secondUserID = UserManager.InsertUser("second_user", "Second", "User2", "second_password");
            int firstRestaurantID = RestaurantManager.InsertRestaurant(firstUserID, "First Restaurant", "First City", "First Description");
            int secondRestaurantID = RestaurantManager.InsertRestaurant(secondUserID, "Second Restaurant", "Second City", "Second Description");
            int firstReviewID = ReviewManager.InsertReview(firstUserID, firstRestaurantID, "First/First", "First/First Description");
            int secondReviewID = ReviewManager.InsertReview(firstUserID, secondRestaurantID, "First/Second", "First/Second Description");
            int thirdReviewID = ReviewManager.InsertReview(secondUserID, firstRestaurantID, "Second/First", "Second/First Description");
            int fourthReviewID = ReviewManager.InsertReview(secondUserID, secondRestaurantID, "Second/Second", "Second/Second Description");

            List<ReviewInfoModel> reviews = ReviewManager.GetReviews(0, 0, 0);
            Assert.IsNotNull(reviews);
            Assert.AreEqual(reviews.Count, 4);

            // Invalid delete (wrong user) - don't delete
            int result = ReviewManager.DeleteReview(secondReviewID, secondUserID);
            Assert.AreEqual(result, -1);
            ReviewInfoModel review = ReviewManager.GetReview(secondReviewID);
            Assert.AreEqual(review.ID, secondReviewID);

            // Valid deletes
            result = ReviewManager.DeleteReview(secondReviewID, firstUserID);
            Assert.AreEqual(result, secondReviewID);
            result = ReviewManager.DeleteReview(thirdReviewID, secondUserID);
            Assert.AreEqual(result, thirdReviewID);

            reviews = ReviewManager.GetReviews(0, 0, 0);
            Assert.AreEqual(reviews.Count, 2);
            Assert.AreEqual(reviews[0].ID, firstReviewID);
            Assert.AreEqual(reviews[1].ID, fourthReviewID);
        }
    }
}
