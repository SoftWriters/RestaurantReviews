using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftWriters.RestaurantReviews.DataLibrary;

namespace SoftWriters.RestaurantReviews.WebApi.Tests
{
    [TestClass]
    public class ReviewApiTests
    {
        private TestDataStore<Restaurant> _restaurantStore;
        private TestDataStore<Review> _reviewDataStore;
        private TestDataStore<User> _userDataStore;
        private ReviewApi _reviewApi;

        [TestInitialize]
        public void Initialize()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant(Guid.NewGuid(), "Grille 565",
                    new Address("565 Lincoln Ave", "15202", "Pittsburgh", "PA", "United States")),
                new Restaurant(Guid.NewGuid(), "202 Hometown Tacos",
                    new Address("202 Lincoln Ave", "15202", "Pittsburgh", "PA", "United States")),
                new Restaurant(Guid.NewGuid(), "Bryan's Speakeasy",
                    new Address("205 N Sprague Ave", "15202", "Pittsburgh", "PA", "United States")),
                new Restaurant(Guid.NewGuid(), "Katz's Delicatessen",
                    new Address("205 E Houston St", "10002", "New York", "NY", "United States"))
            };

            var users = new List<User>()
            {
                new User(Guid.NewGuid(), "John Doe",
                    new Address("123 Main", "15202", "Pittsburgh", "PA", "United States")),
                new User(Guid.NewGuid(), "Jane Doe",
                    new Address("123 Main", "15202", "Pittsburgh", "PA", "United States"))
            };

            var reviews = new List<Review>()
            {
                new Review(Guid.NewGuid(), users[0].Id, restaurants[0].Id, 4, 4, 4, 3, "Cozy!"),
                new Review(Guid.NewGuid(), users[1].Id, restaurants[1].Id, 3, 3, 3, 3, "meh")
            };

            _restaurantStore = new TestDataStore<Restaurant>(restaurants);
            _reviewDataStore = new TestDataStore<Review>(reviews);
            _userDataStore = new TestDataStore<User>(users);
            _reviewApi = new ReviewApi(_restaurantStore, _reviewDataStore, _userDataStore);
        }

        [TestMethod]
        public void AddRestaurant_ReturnsTrue_WhenRestaurantWithMatchingNameAndAddressDoesNotExist()
        {
            bool result = _reviewApi.AddRestaurant("Hanks", "123 Main", "City", "PA", "00000", "US");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddRestaurant_ReturnsFalse_WhenRestaurantWithMatchingNameAndAddressExists()
        {
            bool result = _reviewApi.AddRestaurant("Bryan's Speakeasy", "205 N Sprague Ave", "Pittsburgh", "PA", "15202", "United States");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetRestaurants_ReturnsOnlyRestaurantsMatchingAllCriteria()
        {
            var restaurants = _reviewApi.GetRestaurants("", "Pittsburgh", "", "", "United States").ToList();
            Assert.AreEqual(3, restaurants.Count);
            Assert.IsTrue(restaurants.All(item => item.Address.City == "Pittsburgh"));
        }

        [TestMethod]
        public void AddReview_ReturnsFalse_WhenUserDoesNotExist()
        {
            var restaurant = _restaurantStore.GetAllItems().First();
            bool result = _reviewApi.AddReview(Guid.NewGuid(), restaurant.Id, 5, 5, 5, 5, "");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddReview_ReturnsFalse_WhenReviewExistsForUser()
        {
            var review = _reviewDataStore.GetAllItems().First();
            bool result = _reviewApi.AddReview(review.UserId, review.RestaurantId, 0, 0, 0, 0, "");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddReview_ReturnsTrue_WhenUsersExistsAndReviewDoesNot()
        {
            var user = _userDataStore.GetAllItems().First();
            var restaurant = _restaurantStore.GetAllItems().Last();
            bool result = _reviewApi.AddReview(user.Id, restaurant.Id, 3, 3, 3, 5, "Too expensive!");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetReviews_ReturnsOnlyReviewsMatchingCriteria()
        {
            var user = _userDataStore.GetAllItems().First();
            var reviews = _reviewApi.GetReviews(user.Id, Guid.Empty).ToList();
            Assert.AreEqual(1, reviews.Count);

            var restaurant = _restaurantStore.GetAllItems().ToList()[1];
            reviews = _reviewApi.GetReviews(Guid.Empty, restaurant.Id).ToList();
            Assert.AreEqual(1, reviews.Count);

        }

        [TestMethod]
        public void GetReviews_ReturnsAllReviews_WhenNoInputIsGiven()
        {
            var reviews = _reviewApi.GetReviews(Guid.Empty, Guid.Empty).ToList();
            Assert.AreEqual(2, reviews.Count);
        }

        [TestMethod]
        public void DeleteReview_ReturnsTrueAndRemovesItem()
        {
            var reviews = _reviewApi.GetReviews(Guid.Empty, Guid.Empty).ToList();
            Assert.AreEqual(2, reviews.Count);

            _reviewApi.DeleteReview(reviews[0].Id);

            reviews = _reviewApi.GetReviews(Guid.Empty, Guid.Empty).ToList();
            Assert.AreEqual(1, reviews.Count);
        }
    }
}
