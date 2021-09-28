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
    public class ReviewManagerTests
    {
        [TestMethod()]
        public void CreateReviewTest()
        {
            Member member = MemberManager.CreateMember("username", "first", "last", "email");
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            Review review = ReviewManager.CreateReview(restaurant.Id, member.Id, "this is my review");

            Review retrieved = ReviewManager.GetReview(review.Id);

            Assert.AreEqual(review.Id, retrieved.Id);
            Assert.AreEqual(review.MemberId, retrieved.MemberId);
            Assert.AreEqual(review.RestaurantId, retrieved.RestaurantId);
            Assert.AreEqual(review.Body, retrieved.Body);
        }

        [TestMethod()]
        public void UpdateReviewTest()
        {
            Member member = MemberManager.CreateMember("username", "first", "last", "email");
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            Review review = ReviewManager.CreateReview(restaurant.Id, member.Id, "this is my review");

            Review updated = ReviewManager.UpdateReview(review.Id, restaurant.Id, member.Id, "updated review");

            Review retrievedupdated = ReviewManager.GetReview(updated.Id);

            Assert.AreEqual(updated.Id, retrievedupdated.Id);
            Assert.AreEqual(updated.MemberId, retrievedupdated.MemberId);
            Assert.AreEqual(updated.RestaurantId, retrievedupdated.RestaurantId);
            Assert.AreEqual(updated.Body, retrievedupdated.Body);
        }

        [TestMethod()]
        public void GetReviewTest()
        {
            Member member = MemberManager.CreateMember("username", "first", "last", "email");
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            Review review = ReviewManager.CreateReview(restaurant.Id, member.Id, "this is my review");

            Review retrieved = ReviewManager.GetReview(review.Id);

            Assert.AreEqual(review.Id, retrieved.Id);
            Assert.AreEqual(review.MemberId, retrieved.MemberId);
            Assert.AreEqual(review.RestaurantId, retrieved.RestaurantId);
            Assert.AreEqual(review.Body, retrieved.Body);
        }

        [TestMethod()]
        [ExpectedException(typeof(RestaurantReviews.Entities.Data.RetrievalException))]
        public void DeleteReviewTest()
        {
            Member member = MemberManager.CreateMember("username", "first", "last", "email");
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            Review review = ReviewManager.CreateReview(restaurant.Id, member.Id, "this is my review");

            ReviewManager.DeleteReview(review.Id);

            Review retrieved = ReviewManager.GetReview(review.Id);
            Assert.Fail();
        }

        [TestMethod()]
        public void GetReviewsByRestaurantTest()
        {
            Member member = MemberManager.CreateMember("username", "first", "last", "email");
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            Review firstreview = ReviewManager.CreateReview(restaurant.Id, member.Id, "review #1");
            Review secondreview = ReviewManager.CreateReview(restaurant.Id, member.Id, "review #2");

            List<Review> reviews = ReviewManager.GetReviewsByRestaurant(restaurant.Id);

            Assert.AreEqual(2, reviews.Count);
        }

        [TestMethod()]
        public void GetReviewsByMemberTest()
        {
            Member member = MemberManager.CreateMember("username", "first", "last", "email");
            Restaurant restaurant = RestaurantManager.CreateRestaurant("restaurant");

            Review firstreview = ReviewManager.CreateReview(restaurant.Id, member.Id, "review #1");
            Review secondreview = ReviewManager.CreateReview(restaurant.Id, member.Id, "review #2");

            List<Review> reviews = ReviewManager.GetReviewsByMember(member.Id);

            Assert.AreEqual(2, reviews.Count);
        }
    }
}