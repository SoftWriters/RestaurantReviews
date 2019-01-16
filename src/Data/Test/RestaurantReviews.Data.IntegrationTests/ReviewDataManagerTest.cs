using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Common;
using RestaurantReviews.Data.Tests;
using RestaurantReviews.Entity;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.Data.IntegrationTests
{
    [TestClass]
    public class ReviewDataManagerTest : DataManagerTestBase
    {
        private List<int> userIds;
        private List<int> restaurantIds;
        private List<int> ids;

        private List<int> FillReviews()
        {
            userIds = FillUsers();
            restaurantIds = FillRestaurants();
            ids = new List<int>
            {
                InsertReview(userIds[0], restaurantIds[0], "yum", "great service", 9),
                InsertReview(userIds[0], restaurantIds[1], "good but...", "I think they sauce was stale", 5),
                InsertReview(userIds[1], restaurantIds[0], "slow service", null, 1),
                InsertReview(userIds[1], restaurantIds[2], "love it", "everything was great", 10)
            };
            return ids;
        }
        private List<int> FillUsers()
        {
            var ids = new List<int>
            {
                InsertUser("Don"),
                InsertUser("Frederick")
            };
            return ids;
        }
        private List<int> FillRestaurants()
        {
            var ids = new List<int>
            {
                InsertRestaurant("cool cones", "123 milky way", "ChocoCity"),
                InsertRestaurant("burgatory", "456 beef alley", "Burgertown"),
                InsertRestaurant("candy R us", "222 cavity street", "ChocoCity"),
                InsertRestaurant("cupcakes for everyone", "300 pound highway", "ChocoCity")
            };
            return ids;
        }
        private int InsertUser(string username)
        {
            var insertSql = "insert into Users(username) values ('{0}'); select @@IDENTITY;";
            return this.ExecuteScalar<int>(string.Format(insertSql, username));
        }

        private int InsertRestaurant(string name, string address, string city)
        {
            var insertSql = "insert into Restaurant(name, address, city) values ('{0}', '{1}', '{2}'); select @@IDENTITY;";
            return this.ExecuteScalar<int>(string.Format(insertSql, name, address, city));
        }

        private int InsertReview(int userId, int restaurantId, string heading, string content, int rating)
        {
            var insertSql = "insert into Review(user_id, restaurant_id, heading, content, rating) values ({0}, {1}, '{2}', '{3}', {4}); select @@IDENTITY;";
            return this.ExecuteScalar<int>(string.Format(insertSql, userId, restaurantId, heading, content, rating));
        }





        [TestMethod]
        public void GetReviewsAsync_FieldValues_Test()
        {
            FillReviews();
            var sut = new ReviewDataManager(DbContext);
            var result = sut.GetReviewsAsync(1, 1, null).Result.ToList();
            Assert.AreEqual(1, result.Count());
            var review = result.First();
            //they are sorted by heading so the 2nd review is the one that will be returned
            Assert.AreEqual(ids[1], review.Id);
            Assert.AreEqual(userIds[0], review.UserId);
            Assert.AreEqual(restaurantIds[1], review.RestaurantId);
            Assert.AreEqual("good but...", review.Heading);
            Assert.AreEqual("I think they sauce was stale", review.Content);
            Assert.AreEqual(5, review.Rating);

        }
        

        [TestMethod]
        public void GetReviewsAsync_Paged_AreOrderedByHeading()
        {
            FillReviews();
            var sut = new ReviewDataManager(DbContext);
            var result = sut.GetReviewsAsync(1, 2, null).Result.ToList();
            Assert.AreEqual(2, result.Count(), 2);
            Assert.AreEqual(result[0].Heading, "good but...");
            Assert.AreEqual(result[1].Heading, "love it");

            result = sut.GetReviewsAsync(2, 2, null).Result.ToList();
            Assert.AreEqual(2, result.Count(), 2);
            Assert.AreEqual("slow service", result[0].Heading);
            Assert.AreEqual("yum", result[1].Heading);
        }

        [TestMethod]
        public void GetReviewsAsync_NoFilter_ReturnsAll()
        {
            FillReviews();
            var sut = new ReviewDataManager(DbContext);
            var result = sut.GetReviewsAsync(1, 100, null).Result.ToList();
            Assert.AreEqual(result.Count(), 4);
        }

        [TestMethod]
        public void GetReviewsAsync_FilterByUser_ReturnsAll()
        {
            FillReviews();
            var sut = new ReviewDataManager(DbContext);
            var filter = new DbFilter<Review> { Field = "UserId", Operator = OperatorEnum.Equal, Value = userIds[1].ToString() };
            var result = sut.GetReviewsAsync(1, 100, filter).Result.ToList();
            Assert.AreEqual(result.Count(), 2);
            //the reviews should be sorted by heading so the 2nd one will be first
            //this is the test data that was created
            //ds.Add(InsertReview(userIds[1], restaurantIds[0], "slow service", null, 1));
            //ids.Add(InsertReview(userIds[1], restaurantIds[2], "love it", "everything was great", 10));
            Assert.AreEqual(result[0].Content, "everything was great");
            Assert.IsTrue(string.IsNullOrEmpty(result[1].Content));
        }

        [TestMethod]
        public void CreateReviewAsync_Success()
        {
            FillReviews();
            var reviewToCreate = new Review() {UserId = userIds[0], RestaurantId = restaurantIds[2], Heading = "awful", Content = "tasted like dog food", Rating = 6 };
            var sut = new ReviewDataManager(DbContext);
            var result = sut.CreateReviewAsync(reviewToCreate).Result;
            Assert.IsTrue(result.Id > 0);
            Assert.AreEqual(result.Heading, "awful");
            using (var reader = this.ExecuteReader("select * from Review where Id=@Id", System.Data.CommandType.Text,
                new System.Data.SqlClient.SqlParameter() { ParameterName = "Id", SqlDbType = System.Data.SqlDbType.Int, Value = result.Id }))
            {
                reader.Read();
                var actualUser = reader.GetValue(reader.GetOrdinal("user_id"));
                var actualRestaurant = reader.GetValue(reader.GetOrdinal("restaurant_id"));
                var actualHeading = reader.GetValue(reader.GetOrdinal("heading"));
                var atualContent = reader.GetValue(reader.GetOrdinal("content"));
                var actualRating = (byte)reader.GetValue(reader.GetOrdinal("rating"));
                Assert.AreEqual(reviewToCreate.UserId, actualUser);
                Assert.AreEqual(reviewToCreate.RestaurantId, actualRestaurant);
                Assert.AreEqual(reviewToCreate.Heading, actualHeading);
                Assert.AreEqual(reviewToCreate.Content, atualContent);
                Assert.AreEqual(reviewToCreate.Rating, actualRating);
            }
        }


        [TestMethod]
        public void DeleteReviewAsync_UserIdMatch_Success()
        {
            var ids = FillReviews();
            var sut = new ReviewDataManager(DbContext);
            sut.DeleteReviewAsync(ids[0], userIds[0]).Wait();
            var count = this.ExecuteScalar<int>(string.Format("select count(*) from Review where Id={0}", ids.First()));
            Assert.AreEqual(count, 0);
        }

        [TestMethod]
        public void DeleteReviewAsync_UserIdMisMatch_NotDeleted()
        {
            var ids = FillReviews();
            var sut = new ReviewDataManager(DbContext);
            sut.DeleteReviewAsync(ids[0], userIds[1]).Wait();
            var count = this.ExecuteScalar<int>(string.Format("select count(*) from Review where Id={0}", ids[0]));
            Assert.AreEqual(count, 1);
        }


        [TestMethod]
        public void GetReviewAsync_Success()
        {
            var ids = FillReviews();
            var sut = new ReviewDataManager(DbContext);
            var result = sut.GetReviewAsync(ids.First()).Result;
            Assert.AreEqual(ids.First(), result.Id );
            Assert.AreEqual(userIds[0], result.UserId);
            Assert.AreEqual(restaurantIds[0], result.RestaurantId);
            Assert.AreEqual("yum", result.Heading);
            Assert.AreEqual("great service", result.Content);
            Assert.AreEqual(9, result.Rating);
        }
    }
}
