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
    public class RestaurantRepositoryIntegrationTests
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
            // ToDo: factor the connection string to somewhere less visible
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
        public void GetAllRestaurants()
        {
            try
            {
                // Arrange
                var restaurantRepository = new RestaurantRepository(RestaurantReviewsContext);
                // Act
                var restaurants = restaurantRepository.GetAllRestaurants().Result;
                // Assert
                Assert.IsTrue(restaurants.Count() >= DataSeeder.Restaurants.Count()
                    , string.Format("Database has {0} Restaurants and Seeder has {1}", restaurants.Count(), DataSeeder.Restaurants.Count()));
            }
            catch(Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in GetAllRestaurants {0}", ex.Message));
            }
        }

        [TestMethod]
        public void GetRestaurantsByCity()
        {
            try
            {
                // Arrange
                var restaurantRepository = new RestaurantRepository(RestaurantReviewsContext);
                var city = "Niles";
                var state = "OH";
                var country = "USA";
                // Act
                var restaurants = restaurantRepository.GetRestaurantsByCity(city, state, country).Result;
                // Assert
                var numberOfRestaurantsInCity = DataSeeder.Restaurants.Where<Restaurant>(restaurant => restaurant.City == city && restaurant.State == state && restaurant.Country == country).Count();
                Assert.IsTrue(restaurants.Count() >= numberOfRestaurantsInCity, string.Format("Database has {0} Restaurants and Seeder has {1}", restaurants.Count(), numberOfRestaurantsInCity));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in GetRestaurantsByCity {0}", ex.Message));
            }
        }

        [TestMethod]
        public void GetRestaurantById()
        {
            try
            {
                // Arrange
                var restaurantRepository = new RestaurantRepository(RestaurantReviewsContext);
                // Act
                var school = restaurantRepository.GetRestaurantById(DataSeeder.Restaurants[0].Id).Result;
                // Assert
                Assert.IsNotNull(school, string.Format("Did not find Restaurant with Id: {0}", DataSeeder.Restaurants[0].Id));
            }
            catch(Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in GetRestaurantById {0}", ex.Message));
            }
        }

        [TestMethod]
        public void CreateRestaurant()
        {
            try
            {
                // Arrange
                var restaurantRepository = new RestaurantRepository(RestaurantReviewsContext);
                var newRestaurant = new Restaurant
                {
                    Address = "10 Leninskiy Rayon",
                    City = "Novosibirsk",
                    Country = "Russian Federation",
                    EmailAddress = "NovosibirskEats@email.com",
                    IsConfirmed = true,
                    Name = "Novosibirsk Eats",
                    Phone = "+7 961 222-87-45",
                    PostalCode = "630001",
                    State = "Siberia",
                    WebsiteUrl = "www.novosibirskeats.com"
                };
                // Act
                restaurantRepository.CreateRestaurant(newRestaurant).Wait();
                // Assert
                var savedSchool = restaurantRepository.GetRestaurantById(newRestaurant.Id).Result;
                Assert.IsNotNull(savedSchool, string.Format("Restaurant {0} was not saved in the database", newRestaurant.Id));
                Assert.IsTrue(savedSchool.Name == newRestaurant.Name, string.Format("Restaurant({0}).Name was not saved in the database", newRestaurant.Id));
                // Teardown
                restaurantRepository.DeleteRestaurant(savedSchool).Wait();
                var shouldBeDeletedRestaurant = restaurantRepository.GetRestaurantById(newRestaurant.Id).Result;
                Assert.IsTrue(shouldBeDeletedRestaurant.IsEmptyObject(), string.Format("Restaurant({0}) was not deleted from the database", newRestaurant.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in CreateRestaurant ({0})", ex.Message));
            }
        }

        [TestMethod]
        public void UpdateRestaurant()
        {
            try
            {
                // Arrange
                var restaurantRepository = new RestaurantRepository(RestaurantReviewsContext);
                var newRestaurant = new Restaurant
                {
                    Address = "10 Leninskiy Rayon",
                    City = "Novosibirsk",
                    Country = "Russian Federation",
                    EmailAddress = "NovosibirskEats@email.com",
                    IsConfirmed = true,
                    Name = "Novosibirsk Eats 2",
                    Phone = "+7 961 222-87-45",
                    PostalCode = "630001",
                    State = "Siberia",
                    WebsiteUrl = "www.novosibirskeats.com"
                };
                restaurantRepository.CreateRestaurant(newRestaurant).Wait();
                var savedSchool = restaurantRepository.GetRestaurantById(newRestaurant.Id).Result;
                Assert.IsNotNull(savedSchool, string.Format("Restaurant {0} was not saved in the database", newRestaurant.Id));
                Assert.IsTrue(savedSchool.Name == newRestaurant.Name, string.Format("Restaurant({0}).Name was not saved in the database", newRestaurant.Id));

                // Act
                newRestaurant.Name = "Novosibirsk Eats 2 - Modified";
                restaurantRepository.UpdateRestaurant(savedSchool, newRestaurant).Wait();

                // Assert
                var retrievedRestaurant = restaurantRepository.GetRestaurantById(savedSchool.Id).Result;
                Assert.IsFalse(retrievedRestaurant.IsEmptyObject(), string.Format("Updated restaurant({0}) was not retrieved from the database", newRestaurant.Id));
                Assert.IsTrue(retrievedRestaurant.Name == newRestaurant.Name, string.Format("Restaurant({0}).Name was not updated in the database", newRestaurant.Id));

                // Teardown
                restaurantRepository.DeleteRestaurant(savedSchool).Wait();
                var shouldBeDeletedRestaurant = restaurantRepository.GetRestaurantById(newRestaurant.Id).Result;
                Assert.IsTrue(shouldBeDeletedRestaurant.IsEmptyObject(), string.Format("Restaurant({0}) was not deleted from the database", newRestaurant.Id));
            }
            catch(Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in CreateRestaurant {0}", ex.Message));
            }
        }

        [TestMethod]
        public void DeleteRestaurant()
        {
            try
            {
                // Arrange
                var restaurantRepository = new RestaurantRepository(RestaurantReviewsContext);
                var newRestaurant = new Restaurant
                {
                    Address = "10 Leninskiy Rayon",
                    City = "Novosibirsk",
                    Country = "Russian Federation",
                    EmailAddress = "NovosibirskEats@email.com",
                    IsConfirmed = true,
                    Name = "Novosibirsk Eats 3",
                    Phone = "+7 961 222-87-45",
                    PostalCode = "630001",
                    State = "Siberia",
                    WebsiteUrl = "www.novosibirskeats.com"
                };
                restaurantRepository.CreateRestaurant(newRestaurant).Wait();
                var savedRestaurant = restaurantRepository.GetRestaurantById(newRestaurant.Id).Result;
                Assert.IsNotNull(savedRestaurant, string.Format("Restaurant {0} was not saved in the database", newRestaurant.Id));
                Assert.IsTrue(savedRestaurant.Name == newRestaurant.Name, string.Format("Restaurant({0}).Name was not saved in the database", newRestaurant.Id));
                // Act
                restaurantRepository.DeleteRestaurant(savedRestaurant).Wait();
                // Assert
                var shouldBeDeletedRestaurant = restaurantRepository.GetRestaurantById(newRestaurant.Id).Result;
                Assert.IsTrue(shouldBeDeletedRestaurant.IsEmptyObject(), string.Format("Restaurant({0}) was not deleted from the database", newRestaurant.Id));
            }
            catch(Exception ex)
            {
                Assert.Fail(string.Format("An unexpected exception occurred in DeleteRestaurant {0}", ex.Message));
            }
        }

        #endregion Test Methods
    }
}
