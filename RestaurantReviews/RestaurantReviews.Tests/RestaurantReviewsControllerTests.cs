using NUnit.Framework;
using RestaurantReviews.Controller;
using RestaurantReviews.Core.DataTypes;
using RestaurantReviews.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    [TestFixture]
    public class RestaurantReviewsControllerTests
    {
        private TempFileWrapper _testDatabaseFile;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            //Create a reusable (readonly) database for testing
            //This reduces each test execution time significantly since the db doesn't need to be recreated for every test (4+s to a few ms)
            _testDatabaseFile = new TempFileWrapper();
            TestData.InitializeController(_testDatabaseFile.FilePath);
        }

        [OneTimeTearDown]
        public void FixtureTeardown()
        {
            _testDatabaseFile?.Dispose();
            _testDatabaseFile = null;
        }

        [Test]
        public void CreateAndReadDatabaseTest()
        {
            //This basic tests exercises most of the functionality of the database,
            //verifying the created database (in one-time setup) added all the entities
            using (IRestaurantReviewController controller = new RestaurantReviewsController(_testDatabaseFile.FilePath))
            {
                //Verify the restaurants can be retrieved from the db
                foreach (Restaurant restaurant in TestData.Restaurants.AllRestaurants)
                {
                    IRestaurant foundRestaurant = controller.GetRestaurant(restaurant.UniqueId);
                    Assert.IsNotNull(foundRestaurant, "Restaurant was not found");

                    VerifyRestaurant(restaurant, foundRestaurant);
                }

                //Verify the reviews can be retrieved from the db
                foreach (RestaurantReview review in TestData.Reviews.AllReviews)
                {
                    IRestaurantReview foundReview = controller.GetReview(review.UniqueId);
                    Assert.IsNotNull(foundReview, "Review not found");

                    VerifyReview(review, foundReview);
                }
            }
        }
        
        [Test]
        public void FindRestaurantsByFullQueryTest()
        {
            using (IRestaurantReviewController controller = new RestaurantReviewsController(_testDatabaseFile.FilePath))
            {
                //Verify the restaurants can be retrieved from the db with a fully-specified query
                foreach (Restaurant restaurant in TestData.Restaurants.AllRestaurants)
                {
                    //Full query
                    var restaurantQuery = new RestaurantsQuery() { Name = restaurant.Name, City = restaurant.Address.City, StateOrProvince = restaurant.Address.StateOrProvince, PostalCode = restaurant.Address.PostalCode };
                    IReadOnlyList<IRestaurant> findRestaurantResults = controller.FindRestaurants(restaurantQuery);
                    Assert.AreEqual(1, findRestaurantResults.Count, "Incorrect number of results for FindRestaurants");
                    IRestaurant foundRestaurant = findRestaurantResults[0];
                    VerifyRestaurant(restaurant, foundRestaurant);
                }
            }
        }

        [Test]
        public void FindRestaurantsByCityTest()
        {
            using (IRestaurantReviewController controller = new RestaurantReviewsController(_testDatabaseFile.FilePath))
            {
                //Verify the restaurants can be retrieved from the db by city
                var expectedResults = TestData.Restaurants.AllRestaurants.GroupBy(r => r.Address.City);

                foreach (IGrouping<string, Restaurant> grouping in expectedResults)
                {
                    Dictionary<Guid, Restaurant> expectedRestaurantsById = grouping.ToDictionary(r => r.UniqueId, r => r);

                    var restaurantQuery = new RestaurantsQuery() { City = grouping.Key };
                    IReadOnlyList<IRestaurant> findRestaurantResults = controller.FindRestaurants(restaurantQuery);
                    Assert.AreEqual(expectedRestaurantsById.Count, findRestaurantResults.Count, "Incorrect number of results for FindRestaurants");

                    //Match them up by unique Id and compare
                    foreach (IRestaurant foundRestaurant in findRestaurantResults)
                    {
                        if (!expectedRestaurantsById.TryGetValue(foundRestaurant.UniqueId, out var expectedRestaurant))
                            Assert.Fail("Unexpected restaurant found");

                        VerifyRestaurant(expectedRestaurant, foundRestaurant);
                    }
                }
            }
        }

        [Test]
        public void FindReviewsByUserTest()
        {
            using (IRestaurantReviewController controller = new RestaurantReviewsController(_testDatabaseFile.FilePath))
            {
                //Get the expected results by user
                IEnumerable<IGrouping<Guid, RestaurantReview>> reviewsByReviewerId = TestData.Reviews.AllReviews.GroupBy(r => r.Reviewer.UniqueId);

                //Test each one
                foreach (IGrouping<Guid, RestaurantReview> grouping in reviewsByReviewerId)
                {
                    List<RestaurantReview> expectedReviews = grouping.ToList();

                    IReadOnlyList<IRestaurantReview> foundReviews = controller.GetReviewsForUser(grouping.Key);

                    Assert.AreEqual(expectedReviews.Count, foundReviews.Count, "Incorrect reviews for user");

                    foreach (RestaurantReview review in expectedReviews)
                    {
                        var foundReview = foundReviews.FirstOrDefault(r => r.UniqueId == review.UniqueId);

                        Assert.IsNotNull(foundReview, "Review not found for user and review Id");
                        VerifyReview(review, foundReview);
                    }
                }
            }
        }

        [Test]
        public void AddDuplicateRestaurantThrowsDuplicateEntityException()
        {
            //Make a copy of the test db so we don't corrupt it
            using (var tempFileWrapper = new TempFileWrapper(CreateTempFileCopy(_testDatabaseFile.FilePath)))
            {
                using (IRestaurantReviewController controller = new RestaurantReviewsController(tempFileWrapper.FilePath))
                {
                    Restaurant testRestaurant = TestData.Restaurants.MadNoodles;
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
            //Make a copy of the test db so we don't corrupt it
            using (var tempFileWrapper = new TempFileWrapper(CreateTempFileCopy(_testDatabaseFile.FilePath)))
            {
                using (IRestaurantReviewController controller = new RestaurantReviewsController(tempFileWrapper.FilePath))
                {
                    var newRestaurant = new Restaurant()
                    {
                        Name = "Test fake restaurant",
                        Description = "This restaurant is a new entry",
                        Address = new Address()
                        {
                            StreetLine1 = "123 Fake St",
                            City = "Faketown",
                            StateOrProvince = "PA",
                            PostalCode = "15000"
                        }
                    };

                    controller.AddRestaurant(newRestaurant);
                    var restaurantQuery = new RestaurantsQuery() { Name = newRestaurant.Name, City = newRestaurant.Address.City, StateOrProvince = newRestaurant.Address.StateOrProvince, PostalCode = newRestaurant.Address.PostalCode };
                    IReadOnlyList<IRestaurant> findRestaurantResults = controller.FindRestaurants(restaurantQuery);
                    Assert.AreEqual(1, findRestaurantResults.Count, "Incorrect number of results for FindRestaurants");

                    IRestaurant foundRestaurant = findRestaurantResults[0];
                    VerifyRestaurant(newRestaurant, foundRestaurant);
                }
            }
        }

        [Test]
        public void AddReviewAddsReviewAndUser()
        {
            //Make a copy of the test db so we don't corrupt it
            using (var tempFileWrapper = new TempFileWrapper(CreateTempFileCopy(_testDatabaseFile.FilePath)))
            {
                using (IRestaurantReviewController controller = new RestaurantReviewsController(tempFileWrapper.FilePath))
                {
                    var newReview = new RestaurantReview()
                    {
                        RestaurantUniqueId = TestData.Restaurants.DuckDonuts.UniqueId,
                        FiveStarRating=2,
                        ReviewText = "This place is run by a bunch of quacks!",
                        Timestamp = DateTime.UtcNow,
                        Reviewer = new User()
                        {
                            UniqueId = Guid.NewGuid(),
                            DisplayName = "Test Reviewer 42"
                        }
                    };

                    controller.AddReview(newReview);
                    IReadOnlyList<IRestaurantReview> findReviewsResult = controller.GetReviewsForRestaurant(newReview.RestaurantUniqueId);
                    Assert.AreEqual(1, findReviewsResult.Count, "Incorrect number of results for GetReviewsForRestaurant");

                    IRestaurantReview foundReview = findReviewsResult[0];
                    VerifyReview(newReview, foundReview);

                    //Query by user too, just for fun
                    findReviewsResult = controller.GetReviewsForUser(newReview.Reviewer.UniqueId);
                    Assert.AreEqual(1, findReviewsResult.Count, "Incorrect number of results for GetReviewsForUser");

                    foundReview = findReviewsResult[0];
                    VerifyReview(newReview, foundReview);
                }
            }
        }


        [Test]
        public void DeleteRestaurantDeletesRestaurantAndReviews()
        {
            //Make a copy of the test db so we don't corrupt it
            using (var tempFileWrapper = new TempFileWrapper(CreateTempFileCopy(_testDatabaseFile.FilePath)))
            {
                using (IRestaurantReviewController controller = new RestaurantReviewsController(tempFileWrapper.FilePath))
                {
                    Restaurant testRestaurant = TestData.Restaurants.MadNoodles;
                    controller.DeleteRestaurant(testRestaurant.UniqueId);

                    //Verify it was deleted
                    var restaurantQuery = new RestaurantsQuery() { Name = testRestaurant.Name, City = testRestaurant.Address.City, StateOrProvince = testRestaurant.Address.StateOrProvince, PostalCode = testRestaurant.Address.PostalCode };
                    IReadOnlyList<IRestaurant> findRestaurantResults = controller.FindRestaurants(restaurantQuery);

                    Assert.IsEmpty(findRestaurantResults, "Restaurant was not deleted");

                    IReadOnlyList<IRestaurantReview> findReviewsResults = controller.GetReviewsForRestaurant(testRestaurant.UniqueId);
                    Assert.IsEmpty(findReviewsResults, "Reviews were not deleted");
                }
            }
        }

        [Test]
        public void DeleteReviewDeletesReview()
        {
            //Make a copy of the test db so we don't corrupt it
            using (var tempFileWrapper = new TempFileWrapper(CreateTempFileCopy(_testDatabaseFile.FilePath)))
            {
                using (IRestaurantReviewController controller = new RestaurantReviewsController(tempFileWrapper.FilePath))
                {
                    RestaurantReview testReview = TestData.Reviews.MadNoodles3;
                    controller.DeleteReview(testReview.UniqueId);

                    //Verify it was deleted
                    IReadOnlyList<IRestaurantReview> findReviewsResult = controller.GetReviewsForRestaurant(testReview.RestaurantUniqueId);

                    Assert.IsFalse(findReviewsResult.Any(r => r.UniqueId == testReview.UniqueId), "Review was not deleted");

                    //Query by user too just for fun
                    findReviewsResult = controller.GetReviewsForUser(testReview.Reviewer.UniqueId);

                    Assert.IsFalse(findReviewsResult.Any(r => r.UniqueId == testReview.UniqueId), "Review was not deleted");
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

        private static string CreateTempFileCopy(string filePath)
        {
            string destFile = Path.GetTempFileName();
            File.Copy(filePath, destFile, true);
            return destFile;
        }

        /// <summary>
        /// Helper to create and destroy temporary files
        /// </summary>
        private class TempFileWrapper : IDisposable
        {
            public TempFileWrapper()
                : this(Path.GetTempFileName())
            {
            }

            public TempFileWrapper(string filePath)
            {
                FilePath = filePath;
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