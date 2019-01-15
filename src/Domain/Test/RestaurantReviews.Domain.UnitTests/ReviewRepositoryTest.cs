﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.Common;
using RestaurantReviews.Data;
using RestaurantReviews.Entity;

namespace RestaurantReviews.Domain.UnitTests
{
    [TestClass]
    public class ReviewRepositoryTest
    {
        //fake userinfo provider for resolving userid
        IUserInfoProvider _userInfoProvider = new UserInfoProvider(new UserInfo { Id = 1234 });

        [TestMethod]
        public void CreateReviewAsync_Test()
        {
            var expected = new Review()
            {
                Id = 789,
                UserId = _userInfoProvider.GetCurrentUserInfo().Id,
                RestaurantId = 22,
                Heading = "awesome!!",
                Content = "super food",
                Rating = 10
            };
            Review actual = null;
            var reviewDataManager = new Mock<IReviewDataManager>();
            reviewDataManager.Setup(x => x.CreateReviewAsync(It.IsAny<Review>())).Callback<Review>(x => { actual = x; }).Returns(Task.FromResult<Review>(expected));



            var reviewRepo = new ReviewRepository(reviewDataManager.Object, _userInfoProvider);
            var result = reviewRepo.CreateReviewAsync(expected.RestaurantId, expected.Heading, expected.Content, expected.Rating).Result;
            reviewDataManager.VerifyAll();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.RestaurantId, actual.RestaurantId);
            Assert.AreEqual(expected.Heading, actual.Heading);
            Assert.AreEqual(expected.Content, actual.Content);
            Assert.AreEqual(expected.Rating, actual.Rating);
        }

        [TestMethod]
        public void DeleteReviewAsync_Test()
        {
            var reviewDataManager = new Mock<IReviewDataManager>();
            var userId = _userInfoProvider.GetCurrentUserInfo().Id;
            reviewDataManager.Setup(x => x.DeleteReviewAsync(123, userId)).Returns(Task.CompletedTask);

            var reviewRepo = new ReviewRepository(reviewDataManager.Object, _userInfoProvider);
            reviewRepo.DeleteReviewAsync(123).Wait();
            reviewDataManager.VerifyAll();
        }

        [TestMethod]
        public void GetReviewAsync_Test()
        {
            var reviewDataManager = new Mock<IReviewDataManager>();
            var expected = new Review()
            {
                UserId = 1,
                RestaurantId = 2,
                Heading = "test heading",
                Content = "some content",
                Id = 4556
            };
            reviewDataManager.Setup(x => x.GetReviewAsync(expected.Id)).Returns(Task.FromResult<Review>(expected));

            var reviewRepo = new ReviewRepository(reviewDataManager.Object, _userInfoProvider);
            var actual = reviewRepo.GetReviewAsync(expected.Id).Result;
            reviewDataManager.VerifyAll();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetReviewsAsync_Test()
        {
            var reviewDataManager = new Mock<IReviewDataManager>();
            var userId = _userInfoProvider.GetCurrentUserInfo().Id;
            var expected = new Review[]{
                new Review{
                    Id = 789,
                    UserId = userId,
                    RestaurantId = 22,
                    Heading = "awesome!!",
                    Content = "super food",
                    Rating = 10

                }}.ToList();
            var expectedPage = 1;
            var expectedPageSize = 20;
            var expectedDbFilter = new DbFilter<Review> { Field = "UserId", Operator = OperatorEnum.Equal, Value = userId.ToString() };
            reviewDataManager.Setup(x => x.GetReviewsAsync(expectedPage, expectedPageSize, expectedDbFilter)).Returns(Task.FromResult<IEnumerable<Review>>(expected));

            var reviewRepo = new ReviewRepository(reviewDataManager.Object, _userInfoProvider);
            var actual = reviewRepo.GetReviewsAsync(expectedPage, expectedPageSize, expectedDbFilter).Result;
            reviewDataManager.VerifyAll();
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(expected[0], actual.First());
        }
    }
}
