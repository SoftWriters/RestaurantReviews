using NUnit.Framework;
using RestaurantReviews.Database.Sqlite;
using System;
using System.IO;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateDbTest()
        {
            string filePath = Path.GetTempFileName();
            using (var db = TestDatabase.CreateDatabase(filePath))
            {

            }

            try
            {
                File.Delete(filePath);
            }
            catch { }
        }

    }
}