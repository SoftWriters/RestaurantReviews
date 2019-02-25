using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Data.DataSeeding;
using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.Extensions;
using RestaurantReviews.Data.Repositories.Entities;
using System;
using System.Linq;

namespace RestaurantReviews.Data.Tests
{
    [TestClass]
    public class ReviewRepositoryIntegrationTests
    {
        public static RestaurantReviewsContext RestaurantReviewsContext { get; set; }

        #region Setup/Teardown

        [ClassInitialize]
        public static void TestClassSetup(TestContext context)
        {
            // Setup
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddEntityFrameworkNpgsql()
                .AddDbContext<RestaurantReviewsContext>()
                .BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<RestaurantReviewsContext>();
            // ToDo: factor this to somewhere less visible
            builder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=RestaurantReviews;User Id=postgres;Password=password;");
            var dbContextOptions = builder.Options;
            RestaurantReviewsContext = new RestaurantReviewsContext();
            RestaurantReviewsContext.Database.Migrate();

            // Test Assertions
            Assert.IsTrue(RestaurantReviewsContext.Users.Count() == DataSeeder.Users.Count()
                , string.Format("Database has {0} Users and Seeder has {1}", RestaurantReviewsContext.Users.Count(), DataSeeder.Users.Count()));
            Assert.IsTrue(RestaurantReviewsContext.Restaurants.Count() == DataSeeder.Restaurants.Count()
                , string.Format("Database has {0} Restaurants and Seeder has {1}", RestaurantReviewsContext.Restaurants.Count(), DataSeeder.Restaurants.Count()));
            Assert.IsTrue(RestaurantReviewsContext.Reviews.Count() == DataSeeder.Reviews.Count()
                , string.Format("Database has {0} Reviews and Seeder has {1}", RestaurantReviewsContext.Reviews.Count(), DataSeeder.Reviews.Count()));
        }

        [ClassCleanup]
        public static void TestClassCleanup()
        {
            RestaurantReviewsContext = null;
        }

        #endregion Setup/Teardown

        #region Test Methods

        [TestMethod]
        public void GetAllReviews()
        {
            try
            {
                // Arrange
                var reviewRepository = new ReviewRepository(RestaurantReviewsContext);
                // Act
                var reviews = reviewRepository.GetAllReviews().Result;
                // Assert
                Assert.IsTrue(reviews.Count() >= DataSeeder.Reviews.Count()
                    , string.Format("Database has {0} Reviews and Seeder has {1}", reviews.Count(), DataSeeder.Reviews.Count()));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in GetAllReviews {0}", ex.Message));
            }
        }

        [TestMethod]
        public void GetReviewsByUser()
        {
            try
            {
                // Arrange
                var userRepository = new UserRepository(RestaurantReviewsContext);
                var reviewRepository = new ReviewRepository(RestaurantReviewsContext);
                var user = (userRepository.FindByCondition(u => u.EmailAddress == "user1@email.com").Result).FirstOrDefault();
                var userId = user.Id;
                // Act
                var reviews = reviewRepository.GetReviewsByUser(userId).Result;
                // Assert
                var numberOfReviewsByUser = 3;
                var reviewsList = reviews.ToList();
                var numberOfReviewsFromDb = reviewsList.Count();
                Assert.IsTrue(reviews.Count() >= numberOfReviewsByUser, string.Format("Database has {0} Reviews and Seeder has {1}", numberOfReviewsFromDb, numberOfReviewsByUser));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in GetReviewsByUser {0}", ex.Message));
            }
        }

        [TestMethod]
        public void GetReviewById()
        {
            try
            {
                // Arrange
                var reviewRepository = new ReviewRepository(RestaurantReviewsContext);
                // Act
                var review = reviewRepository.GetReviewById(DataSeeder.Reviews[0].Id).Result;
                // Assert
                Assert.IsNotNull(review, string.Format("Did not find review with Id: {0}", DataSeeder.Reviews[0].Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in GetReviewById {0}", ex.Message));
            }
        }

        [TestMethod]
        public void CreateReview()
        {
            try
            {
                // Arrange
                var reviewRepository = new ReviewRepository(RestaurantReviewsContext);
                var newReview = new Review
                {
                    Comment = "Good food.",
                    Rating = 4,
                    RestaurauntId = DataSeeder.Restaurants[0].Id,
                    SubmissionDate = DateTime.UtcNow,
                    UserId = DataSeeder.Users[0].Id
                };
                // Act
                reviewRepository.CreateReview(newReview).Wait();
                // Assert
                var savedReview = reviewRepository.GetReviewById(newReview.Id).Result;
                Assert.IsNotNull(savedReview, string.Format("Review {0} was not saved in the database", newReview.Id));
                Assert.IsTrue(savedReview.Comment == newReview.Comment, string.Format("Review({0}).Name was not saved in the database", newReview.Id));
                // Teardown
                reviewRepository.DeleteReview(savedReview).Wait();
                var shouldBeDeletedReview = reviewRepository.GetReviewById(newReview.Id).Result;
                Assert.IsTrue(shouldBeDeletedReview.IsEmptyObject(), string.Format("Review({0}) was not deleted from the database", newReview.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in CreateReview {0}", ex.Message));
            }
        }

        [TestMethod]
        public void UpdateReview()
        {
            try
            {
                // Arrange
                var reviewRepository = new ReviewRepository(RestaurantReviewsContext);
                var newReview = new Review
                {
                    Comment = "Good food.",
                    Rating = 4,
                    RestaurauntId = DataSeeder.Restaurants[0].Id,
                    SubmissionDate = DateTime.UtcNow,
                    UserId = DataSeeder.Users[0].Id
                };
                reviewRepository.CreateReview(newReview).Wait();
                var savedReview = reviewRepository.GetReviewById(newReview.Id).Result;
                Assert.IsNotNull(savedReview, string.Format("Review {0} was not saved in the database", newReview.Id));
                Assert.IsTrue(savedReview.Comment == newReview.Comment, string.Format("Review({0}).Comment was not saved in the database", newReview.Id));
                // Act
                newReview.Comment = "Good food. - Modified";
                reviewRepository.UpdateReview(savedReview, newReview).Wait();
                // Assert
                var retrievedReview = reviewRepository.GetReviewById(savedReview.Id).Result;
                Assert.IsFalse(retrievedReview.IsEmptyObject(), string.Format("Updated review({0}) was not retrieved from the database", newReview.Id));
                Assert.IsTrue(retrievedReview.Comment == newReview.Comment, string.Format("Review({0}).Name was not updated in the database", newReview.Id));
                // Teardown
                reviewRepository.DeleteReview(savedReview).Wait();
                var shouldBeDeletedReview = reviewRepository.GetReviewById(newReview.Id).Result;
                Assert.IsTrue(shouldBeDeletedReview.IsEmptyObject(), string.Format("Review({0}) was not deleted from the database", newReview.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in UpdateReview {0}", ex.Message));
            }
        }

        [TestMethod]
        public void DeleteReview()
        {
            try
            {
                // Arrange
                var reviewRepository = new ReviewRepository(RestaurantReviewsContext);
                var newReview = new Review
                {
                    Comment = "Good food.",
                    Rating = 4,
                    RestaurauntId = DataSeeder.Restaurants[0].Id,
                    SubmissionDate = DateTime.UtcNow,
                    UserId = DataSeeder.Users[0].Id
                };
                reviewRepository.CreateReview(newReview).Wait();
                var savedReview = reviewRepository.GetReviewById(newReview.Id).Result;
                Assert.IsNotNull(savedReview, string.Format("Review {0} was not saved in the database", newReview.Id));
                Assert.IsTrue(savedReview.Comment == newReview.Comment, string.Format("Review({0}).Comment was not saved in the database", newReview.Id));
                // Act
                reviewRepository.DeleteReview(savedReview).Wait();
                // Assert
                var shouldBeDeletedReview = reviewRepository.GetReviewById(newReview.Id).Result;
                Assert.IsTrue(shouldBeDeletedReview.IsEmptyObject(), string.Format("Review({0}) was not deleted from the database", newReview.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in DeleteReview {0}", ex.Message));
            }
        }

        #endregion Test Methods
    }
}
