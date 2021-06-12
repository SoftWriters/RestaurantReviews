using NUnit.Framework;
using RestaurantReviews.Controller;
using RestaurantReviews.Core;
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
            //This basic tests exercises most of the functionality of the database,
            //verifying the database can be created, entities added, and retrieved

            using (var tempFile = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFile.FilePath))
                {
                    //Verify the restaurants can be retrieved from the db
                    foreach (FakeRestaurant restaurant in TestData.Restaurants.AllRestaurants)
                    {
                        var restaurantQuery = new RestaurantsQuery() { Name = restaurant.Name, City = restaurant.Address.City, StateOrProvince = restaurant.Address.StateOrProvince, PostalCode = restaurant.Address.PostalCode };
                        IReadOnlyList<IRestaurant> findRestaurantResults = controller.FindRestaurants(restaurantQuery);
                        Assert.AreEqual(1, findRestaurantResults.Count, "Incorrect number of results for FindRestaurants");

                        IRestaurant foundRestaurant = findRestaurantResults[0];
                        VerifyRestaurant(restaurant, foundRestaurant);
                    }

                    //Verify the reviews can be retrieved from the db
                    foreach (FakeRestaurantReview review in TestData.Reviews.AllReviews)
                    {
                        IReadOnlyList<IRestaurantReview> findReviewResults = controller.GetReviewsForRestaurant(review.RestaurantUniqueId);

                        IRestaurantReview foundReview = findReviewResults.FirstOrDefault(r => r.UniqueId == review.UniqueId);
                        Assert.IsNotNull(foundReview, "Review not found for restaurant and review id");

                        VerifyReview(review, foundReview);
                    }
                }
            }
        }

        [Test]
        public void FindReviewsByUserTest()
        {
            using (var tempFileWrapper = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFileWrapper.FilePath))
                {
                    //Get the expected results by user
                    IEnumerable<IGrouping<Guid, FakeRestaurantReview>> reviewsByReviewerId = TestData.Reviews.AllReviews.GroupBy(r => r.Reviewer.UniqueId);

                    //Test each one
                    foreach (IGrouping<Guid, FakeRestaurantReview> grouping in reviewsByReviewerId)
                    {
                        List<FakeRestaurantReview> expectedReviews = grouping.ToList();

                        IReadOnlyList<IRestaurantReview> foundReviews = controller.GetReviewsForUser(grouping.Key);

                        Assert.AreEqual(expectedReviews.Count, foundReviews.Count, "Incorrect reviews for user");

                        foreach (FakeRestaurantReview review in expectedReviews)
                        {
                            var foundReview = foundReviews.FirstOrDefault(r => r.UniqueId == review.UniqueId);

                            Assert.IsNotNull(foundReview, "Review not found for user and review Id");
                            VerifyReview(review, foundReview);
                        }
                    }
                }
            }
        }

        [Test]
        public void AddDuplicateRestaurantThrowsDuplicateEntityException()
        {
            using (var tempFileWrapper = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFileWrapper.FilePath))
                {
                    FakeRestaurant testRestaurant = TestData.Restaurants.MadNoodles;
                    Assert.Throws<DuplicateEntityException>(() => controller.AddRestaurant(testRestaurant), "Adding duplicate restaurant did not throw an exception");

                    //Verify the duplicate wasn't added
                    var restaurantQuery = new RestaurantsQuery() { Name = testRestaurant.Name, City = testRestaurant.Address.City, StateOrProvince = testRestaurant.Address.StateOrProvince, PostalCode = testRestaurant.Address.PostalCode };
                    IReadOnlyList<IRestaurant> findRestaurantResults = controller.FindRestaurants(restaurantQuery);
                    Assert.AreEqual(1, findRestaurantResults.Count, "Incorrect number of results for FindRestaurants");
                }
            }
        }

        [Test]
        public void AddRestaurantAddsRestaurantAndAddress()
        {
            using (var tempFileWrapper = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFileWrapper.FilePath))
                {
                    //TODO
                }
            }
        }

        [Test]
        public void AddRestaurantAddsRestaurantAndIgnoresDuplicateAddress()
        {
            using (var tempFileWrapper = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFileWrapper.FilePath))
                {
                    //TODO
                }
            }
        }

        [Test]
        public void UpdateRestaurantUpdateRestaurant()
        {
            using (var tempFileWrapper = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFileWrapper.FilePath))
                {
                    //TODO
                }
            }
        }

        [Test]
        public void UpdateUnknownRestaurantThrowsEntityNotFoundException()
        {
            using (var tempFileWrapper = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFileWrapper.FilePath))
                {
                    //TODO
                }
            }
        }

        [Test]
        public void DeleteRestaurantDeletesRestaurantAndReviewsAndAddress()
        {
            using (var tempFileWrapper = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFileWrapper.FilePath))
                {
                    FakeRestaurant testRestaurant = TestData.Restaurants.MadNoodles;
                    controller.DeleteRestaurant(testRestaurant.UniqueId);
                    //TODO
                }
            }
        }

        [Test]
        public void DeleteRestaurantDoesNotDeleteAddressIfShared()
        {
            using (var tempFileWrapper = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFileWrapper.FilePath))
                {
                    // FakeRestaurant testRestaurant;
                    //db.DeleteRestaurant()
                    //TODO
                }
            }
        }

        [Test]
        public void DeleteUnknownRestaurantThrowsEntityNotFoundException()
        {
            using (var tempFileWrapper = new TempFileWrapper())
            {
                using (IRestaurantReviewController controller = TestData.InitializeController(tempFileWrapper.FilePath))
                {
                    //TODO
                }
            }
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
            Assert.AreEqual(expected.RestaurantUniqueId, actual.RestaurantUniqueId, "Incorrect review restaurant");
            Assert.AreEqual(expected.ReviewText, actual.ReviewText, "Incorrect review text");
            Assert.AreEqual(expected.FiveStarRating, actual.FiveStarRating, "Incorrect review star rating");
            Assert.AreEqual(expected.Timestamp, actual.Timestamp, "Incorrect review date");

            VerifyReviewer(expected.Reviewer, actual.Reviewer);
        }

        private static void VerifyReviewer(IUser expected, IUser actual)
        {
            Assert.AreEqual(expected.UniqueId, actual.UniqueId, "Incorrect user unique Id");
            Assert.AreEqual(expected.DisplayName, actual.DisplayName, "Incorrect user display name");
        }

        /// <summary>
        /// Helper to create and destroy temporary files
        /// </summary>
        private class TempFileWrapper : IDisposable
        {
            public TempFileWrapper()
            {
                FilePath = Path.GetTempFileName();
            }

            public string FilePath { get; }

            public void Dispose()
            {
                try
                {
                    File.Delete(FilePath);
                }
                catch { }
            }
        }
    }
}