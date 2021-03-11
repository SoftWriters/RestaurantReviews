using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftWriters.RestaurantReviews.DataLibrary;

namespace SoftWriters.RestaurantReviews.WebApi.Tests
{
    // Some unit test methods to generate local data for testing purposes

    [TestClass]
    public class GenerateData
    {
        // Uncomment [TestMethod] attributes on the given methods in order to run
        // unit tests which clear and load database with test data

        private List<Restaurant> _restaurants;
        private List<Review> _reviews;
        private List<User> _users;

        [TestMethod]
        public void GenerateAllData()
        {
            GenerateRestaurantData();
            GenerateUserData();
            GenerateReviewData();
        }

        //[TestMethod]
        public void GenerateRestaurantData()
        {
            string myDocsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(myDocsDir, string.Format("xmlDataStore_{0}.xml", nameof(Restaurant)));
            if(File.Exists(filename))
                File.Delete(filename);

            _restaurants = new List<Restaurant>()
            {
                new Restaurant(Guid.NewGuid(), "Grille 565", new Address("565 Lincoln Ave", "15202", "Pittsburgh", "PA", "United States")),
                new Restaurant(Guid.NewGuid(), "202 Hometown Tacos", new Address("202 Lincoln Ave", "15202", "Pittsburgh", "PA", "United States")),
                new Restaurant(Guid.NewGuid(), "Bryan's Speakeasy", new Address("205 N Sprague Ave", "15202", "Pittsburgh", "PA", "United States")),
                new Restaurant(Guid.NewGuid(), "Katz's Delicatessen", new Address("205 E Houston St", "10002", "New York", "NY", "United States"))
            };

            var dataStore = new XmlDataStore<Restaurant>();
            foreach(var item in _restaurants)
                dataStore.AddItem(item);
        }

        //[TestMethod]
        public void GenerateUserData()
        {
            string myDocsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(myDocsDir, string.Format("xmlDataStore_{0}.xml", nameof(User)));
            if (File.Exists(filename))
                File.Delete(filename);

            _users = new List<User>()
            {
                new User(Guid.NewGuid(), "John Doe", new Address("703 Highland", "16335", "Meadville", "PA", "United States")),
                new User(Guid.NewGuid(), "Jane Doe", new Address("12 Marie Ave", "15202", "Pittsburgh", "PA", "United States"))
            };

            var dataStore = new XmlDataStore<User>();
            foreach (var item in _users)
                dataStore.AddItem(item);
        }

        //[TestMethod]
        public void GenerateReviewData()
        {
            string myDocsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(myDocsDir, string.Format("xmlDataStore_{0}.xml", nameof(Review)));
            if (File.Exists(filename))
                File.Delete(filename);

            _reviews = new List<Review>()
            {
                new Review(Guid.NewGuid(), _users[0].Id, _restaurants[0].Id, 4, 4, 4, 3, "Cozy!"),
                new Review(Guid.NewGuid(), _users[1].Id, _restaurants[1].Id, 3, 3, 2, 3, "Meh!")
            };

            var dataStore = new XmlDataStore<Review>();
            foreach (var item in _reviews)
                dataStore.AddItem(item);
        }
    }
}
