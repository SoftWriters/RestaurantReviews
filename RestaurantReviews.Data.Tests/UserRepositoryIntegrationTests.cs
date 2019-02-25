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
    public class UserRepositoryIntegrationTests
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
        public void GetAllUsers()
        {
            try
            {
                // Arrange
                var userRepository = new UserRepository(RestaurantReviewsContext);
                // Act
                var users = userRepository.GetAllUsers().Result;
                // Assert
                Assert.IsTrue(users.Count() >= DataSeeder.Users.Count()
                    , string.Format("Database has {0} Users and Seeder has {1}", users.Count(), DataSeeder.Users.Count()));
            }
            catch(Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in GetAllUsers {0}", ex.Message));
            }
        }

        [TestMethod]
        public void GetUserById()
        {
            try
            {
                // Arrange
                var userRepository = new UserRepository(RestaurantReviewsContext);
                // Act
                var user = userRepository.GetUserById(DataSeeder.Users[0].Id).Result;
                // Assert
                Assert.IsNotNull(user, string.Format("Did not find school with Id: {0}", DataSeeder.Users[0].Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in GetUserById {0}", ex.Message));
            }
        }

        [TestMethod]
        public void CreateUser()
        {
            try
            {
                // Arrange
                var userRepository = new UserRepository(RestaurantReviewsContext);
                var newUser = new User
                {
                    DateCreated = DateTime.UtcNow,
                    FirstName = "Update1",
                    MiddleName = "",
                    LastName = "",
                    EmailAddress = "UpdateEmail@email.com",
                    IsActive = true
                };
                // Act
                userRepository.CreateUser(newUser).Wait();
                // Assert
                var savedUser = userRepository.GetUserById(newUser.Id).Result;
                Assert.IsNotNull(savedUser, string.Format("User {0} was not saved in the database", newUser.Id));
                Assert.IsTrue(savedUser.EmailAddress == newUser.EmailAddress, string.Format("User({0}).Name was not saved in the database", newUser.Id));
                // Teardown
                userRepository.DeleteUser(savedUser).Wait();
                var shouldBeDeletedUser = userRepository.GetUserById(newUser.Id).Result;
                Assert.IsTrue(shouldBeDeletedUser.IsEmptyObject(), string.Format("User({0}) was not deleted from the database", newUser.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in CreateUser {0}", ex.Message));
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            try
            {
                // Arrange
                var userRepository = new UserRepository(RestaurantReviewsContext);
                var newUser = new User
                {
                    DateCreated = DateTime.UtcNow,
                    FirstName = "Update1",
                    MiddleName = "",
                    LastName = "",
                    EmailAddress = "UpdateEmail@email.com",
                    IsActive = true
                };
                userRepository.CreateUser(newUser).Wait();
                var savedUser = userRepository.GetUserById(newUser.Id).Result;
                Assert.IsNotNull(savedUser, string.Format("User {0} was not saved in the database", newUser.Id));
                Assert.IsTrue(savedUser.EmailAddress == newUser.EmailAddress, string.Format("User({0}).EmailAddress was not saved in the database", newUser.Id));

                // Act
                newUser.EmailAddress = "UpdateEmailUpdate@email.com";
                userRepository.UpdateUser(savedUser, newUser).Wait();

                // Assert
                var retrievedUser = userRepository.GetUserById(savedUser.Id).Result;
                Assert.IsFalse(retrievedUser.IsEmptyObject(), string.Format("Updated user({0}) was not retrieved from the database", newUser.Id));
                Assert.IsTrue(retrievedUser.EmailAddress == newUser.EmailAddress, string.Format("User({0}).EmailAddress was not updated in the database", newUser.Id));

                // Teardown
                userRepository.DeleteUser(savedUser).Wait();
                var shouldBeDeletedUser = userRepository.GetUserById(newUser.Id).Result;
                Assert.IsTrue(shouldBeDeletedUser.IsEmptyObject(), string.Format("User({0}) was not deleted from the database", newUser.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in UpdateUser {0}", ex.Message));
            }
        }

        [TestMethod]
        public void DeleteUser()
        {
            try
            {
                // Arrange
                var userRepository = new UserRepository(RestaurantReviewsContext);
                var newUser = new User
                {
                    DateCreated = DateTime.UtcNow,
                    FirstName = "Update1",
                    MiddleName = "",
                    LastName = "",
                    EmailAddress = "UpdateEmail@email.com",
                    IsActive = true
                };
                userRepository.CreateUser(newUser).Wait();
                var savedUser = userRepository.GetUserById(newUser.Id).Result;
                Assert.IsNotNull(savedUser, string.Format("User {0} was not saved in the database", newUser.Id));
                Assert.IsTrue(savedUser.EmailAddress == newUser.EmailAddress, string.Format("User({0}).EmailAddress was not saved in the database", newUser.Id));
                // Act
                userRepository.DeleteUser(savedUser).Wait();
                // Assert
                var shouldBeDeletedUser = userRepository.GetUserById(newUser.Id).Result;
                Assert.IsTrue(shouldBeDeletedUser.IsEmptyObject(), string.Format("User({0}) was not deleted from the database", newUser.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in DeleteUser {0}", ex.Message));
            }
        }

        #endregion Test Methods
    }
}
