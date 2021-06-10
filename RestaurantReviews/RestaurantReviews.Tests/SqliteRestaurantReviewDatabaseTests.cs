using NUnit.Framework;
using RestaurantReviews.Core;
using RestaurantReviews.Database.Sqlite;
using SQLite.Net.Platform.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateAndReadDatabaseTest()
        {
            //Verifies the test database can be created and data can be retrieved
            //string filePath = "C:\\Users\\jnen\\Documents\\RestaurantReviews\\RestaurantReviews\\RestaurantReviews.Tests\\TestFiles\\TestDb.db3";
            string filePath = Path.GetTempFileName();
            using (SqliteRestaurantReviewDatabase db = TestDatabase.CreateDatabase(filePath))
            {
                //Verify the restaurants can be retrieved from the db
                foreach (FakeRestaurant restaurant in TestDatabase.Restaurants.AllRestaurants)
                {
                    IReadOnlyList<IRestaurant> findRestaurantResults = db.FindRestaurants(restaurant.Name, restaurant.Address.City, restaurant.Address.StateOrProvince, restaurant.Address.PostalCode);
                    Assert.AreEqual(1, findRestaurantResults.Count, "Incorrect number of results for FindRestaurants");

                    IRestaurant foundRestaurant = findRestaurantResults[0];
                    VerifyRestaurant(restaurant, foundRestaurant);
                }

                //Verify the reviews can be retrieved from the db
                foreach (FakeRestaurantReview review in TestDatabase.Reviews.AllReviews)
                {
                    IReadOnlyList<IRestaurantReview> findReviewResults = db.FindReviews(review.Restaurant);

                    IRestaurantReview foundReview = findReviewResults.FirstOrDefault(r => r.UniqueId == review.UniqueId);
                    Assert.IsNotNull(foundReview, "Review not found for restaurant and review id");

                    VerifyReview(review, foundReview);
                }
            }

            try
            {
                File.Delete(filePath);
            }
            catch { }
        }

        [Test]
        public void FindReviewsByUserTest()
        {
            string filePath = Path.GetTempFileName();
            using (SqliteRestaurantReviewDatabase db = TestDatabase.CreateDatabase(filePath))
            {
                //Get the expected results by user
                IEnumerable<IGrouping<IUser, FakeRestaurantReview>> reviewsByReviewer = TestDatabase.Reviews.AllReviews.GroupBy(r => r.Reviewer);

                //Test each one
                foreach (IGrouping<IUser, FakeRestaurantReview> grouping in reviewsByReviewer)
                {
                    List<FakeRestaurantReview> expectedReviews = grouping.ToList();

                    IReadOnlyList<IRestaurantReview> foundReviews = db.FindReviewsByReviewer(grouping.Key);

                    Assert.AreEqual(expectedReviews.Count, foundReviews.Count, "Incorrect reviews for user");

                    foreach (FakeRestaurantReview review in expectedReviews)
                    {
                        var foundReview = foundReviews.FirstOrDefault(r => r.UniqueId == review.UniqueId);

                        Assert.IsNotNull(foundReview, "Review not found for user and review Id");
                        VerifyReview(review, foundReview);
                    }
                }
            }

            try
            {
                File.Delete(filePath);
            }
            catch { }
        }

        private static void VerifyRestaurant(IRestaurant expected, IRestaurant actual)
        {
            Assert.AreEqual(expected.UniqueId, actual.UniqueId, "Incorrect restaurant unique Id");
            Assert.AreEqual(expected.Name, actual.Name, "Incorrect restaurant name");
            Assert.AreEqual(expected.Description, actual.Description, "Incorrect restaurant description");

            VerifyAddress(expected.Address, actual.Address);
        }

        private static void VerifyAddress(IAddress expected, IAddress actual)
        {
            Assert.AreEqual(expected.UniqueId, actual.UniqueId, "Incorrect address unique Id");
            Assert.AreEqual(expected.StreetLine1, actual.StreetLine1, "Incorrect address street line 1");
            Assert.AreEqual(expected.StreetLine2, actual.StreetLine2, "Incorrect address street line 2");
            Assert.AreEqual(expected.BuildingNumber, actual.BuildingNumber, "Incorrect address building number");
            Assert.AreEqual(expected.CountryOrRegion, actual.CountryOrRegion, "Incorrect address country");
            Assert.AreEqual(expected.StateOrProvince, actual.StateOrProvince, "Incorrect address state");
            Assert.AreEqual(expected.PostalCode, actual.PostalCode, "Incorrect address postal code");
        }

        private static void VerifyReview(IRestaurantReview expected, IRestaurantReview actual)
        {
            Assert.AreEqual(expected.Restaurant.UniqueId, actual.Restaurant.UniqueId, "Incorrect review restaurant");
            Assert.AreEqual(expected.ReviewText, actual.ReviewText, "Incorrect review text");
            Assert.AreEqual(expected.FiveStarRating, actual.FiveStarRating, "Incorrect review star rating");
            Assert.AreEqual(expected.Date, actual.Date, "Incorrect review date");

            VerifyRestaurant(expected.Restaurant, actual.Restaurant);
            VerifyReviewer(expected.Reviewer, actual.Reviewer);
        }

        private static void VerifyReviewer(IUser expected, IUser actual)
        {
            Assert.AreEqual(expected.UniqueId, actual.UniqueId, "Incorrect user unique Id");
            Assert.AreEqual(expected.DisplayName, actual.DisplayName, "Incorrect user display name");
        }
    }
}