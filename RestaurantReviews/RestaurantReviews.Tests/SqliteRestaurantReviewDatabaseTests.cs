using NUnit.Framework;
using RestaurantReviews.Core;
using RestaurantReviews.Database.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            using (var db = TestDatabase.CreateDatabase(filePath))
            {
                //Verify the restaurants can be retrieved from the db
                foreach (var restaurant in TestDatabase.Restaurants.AllRestaurants)
                {
                    IReadOnlyList<IRestaurant> findRestaurantResults = db.FindRestaurants(restaurant.Name, restaurant.Address.City, restaurant.Address.StateOrProvince, restaurant.Address.PostalCode);
                    Assert.AreEqual(1, findRestaurantResults.Count, "Incorrect number of results for FindRestaurants");

                    IRestaurant foundRestaurant = findRestaurantResults[0];
                    Assert.AreEqual(restaurant.UniqueId, foundRestaurant.UniqueId, "Incorrect restaurant unique Id");
                    Assert.AreEqual(restaurant.Name, foundRestaurant.Name, "Incorrect restaurant name");
                    Assert.AreEqual(restaurant.Description, foundRestaurant.Description, "Incorrect restaurant description");

                    VerifyAddress(restaurant.Address, foundRestaurant.Address);
                }

                //Verify the reviews can be retrieved from the db
                foreach (var review in TestDatabase.Reviews.AllReviews)
                {
                    IReadOnlyList<IRestaurantReview> findReviewResults = db.FindReviews(review.Restaurant);

                    IRestaurantReview foundReview = findReviewResults.FirstOrDefault(r => r.UniqueId == review.UniqueId);
                    Assert.IsNotNull(foundReview, "Review not found for restaurant and review id");

                    Assert.AreEqual(review.Restaurant.UniqueId, foundReview.Restaurant.UniqueId, "Incorrect review restaurant");
                    Assert.AreEqual(review.ReviewText, foundReview.ReviewText, "Incorrect review text");
                    Assert.AreEqual(review.FiveStarRating, foundReview.FiveStarRating, "Incorrect review star rating");
                    Assert.AreEqual(review.Date, foundReview.Date, "Incorrect review date");

                    VerifyReviewer(review.Reviewer, foundReview.Reviewer);
                }
            }
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

        private static void VerifyReviewer(IUser expected, IUser actual)
        {
            Assert.AreEqual(expected.UniqueId, actual.UniqueId, "Incorrect user unique Id");
            Assert.AreEqual(expected.DisplayName, actual.DisplayName, "Incorrect user display name");
        }
    }
}