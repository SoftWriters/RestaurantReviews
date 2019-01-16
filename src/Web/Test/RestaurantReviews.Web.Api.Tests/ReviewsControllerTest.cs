using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.Common;
using RestaurantReviews.Domain;
using RestaurantReviews.Entity;
using RestaurantReviews.Web.Api.Controllers;
using RestaurantReviews.Web.Api.Models;

namespace RestaurantReviews.Web.Api.Tests
{
    [TestClass]
    public class ReviewsControllerTest
    {
        [TestMethod]
        public void Search_DefaultParams_ReturnsList()
        {
            IEnumerable<Review> expected = new List<Review>{
                new Review {
                    Id = 1,
                    UserId = 2,
                    RestaurantId = 3
                },
                new Review {
                    Id = 2,
                    UserId = 4,
                    RestaurantId = 7
                }
            };
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(x => x.GetReviewsAsync(1, 1000, null)).Returns(Task.FromResult<IEnumerable<Review>>(expected));
            var sut = new ReviewsController(reviewRepository.Object);
            var actual = sut.Post(null, null, null).Result;
            Assert.AreEqual(expected, actual);
            reviewRepository.VerifyAll();
        }
        
        [TestMethod]
        public void Search_ExplicitParams_ReturnsList()
        {
            IEnumerable<Review> expected = new List<Review>{
                new Review {
                    Id = 1,
                    UserId = 2,
                    RestaurantId = 3
                },
                new Review {
                    Id = 1,
                    UserId = 4,
                    RestaurantId = 7
                }
            };
            var reviewRepository = new Mock<IReviewRepository>();
            DbFilter<Review> actualFilter = null;
            reviewRepository.Setup(x => x.GetReviewsAsync(3, 2, It.IsAny<DbFilter<Review>>()))
                .Callback<int, int, DbFilter<Review>>((page, pagesize, filter) => { actualFilter = filter; })
                .Returns(Task.FromResult(expected));
            var filterParam = new FilterParam { Field = "UserId", Operator = OperatorEnum.Equal, Value = 1 };

            var sut = new ReviewsController(reviewRepository.Object);
            var actual = sut.Post(filterParam, 3, 2).Result;

            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(actualFilter);
            Assert.AreEqual("UserId", actualFilter.Field);
            Assert.AreEqual(OperatorEnum.Equal, actualFilter.Operator);
            Assert.AreEqual(1, actualFilter.Value);
            reviewRepository.VerifyAll();
        }


        [TestMethod]
        public void Get_CallsRepository_ReturnsReview()
        {
            var expected = new Review
            {
                Id = 1,
                UserId = 2,
                RestaurantId = 3
            };
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(x => x.GetReviewAsync(123)).Returns(Task.FromResult<Review>(expected));

            var sut = new ReviewsController(reviewRepository.Object);
            var actual = sut.Get(123).Result;

            Assert.AreEqual(expected, actual);
            reviewRepository.VerifyAll();
        }


        [TestMethod]
        public void Post_CallsRepository_ReturnsReview()
        {
            var expected = new Review
            {
                Id = 1,
                UserId = 2,
                RestaurantId = 3,
                Heading = "wow",
                Content = "yummy",
                Rating = 10
            };

            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(x => x.CreateReviewAsync(expected.RestaurantId, expected.Heading, expected.Content, expected.Rating)).Returns(Task.FromResult<Review>(expected));

            var sut = new ReviewsController(reviewRepository.Object);
            var actual = sut.Post(expected).Result;

            Assert.AreEqual(expected, actual);
            reviewRepository.VerifyAll();
        }

        [TestMethod]
        public void Delete_CallsRepository_Returns()
        {
            var reviewRepository = new Mock<IReviewRepository>();
            reviewRepository.Setup(x => x.DeleteReviewAsync(123)).Returns(Task.CompletedTask);

            var sut = new ReviewsController(reviewRepository.Object);
            sut.Delete(123).Wait();

            reviewRepository.VerifyAll();
        }

        [TestMethod]
        public void Get_DefaultParams_ReturnsList()
        {
            IEnumerable<Review> expected = new List<Review>{
                new Review {
                    Id = 1,
                    UserId = 2,
                    RestaurantId = 3
                },
                new Review {
                    Id = 2,
                    UserId = 4,
                    RestaurantId = 7
                }
            };
            var reviewRepository = new Mock<IReviewRepository>();
            DbFilter<Review> actualFilter = null;
            reviewRepository.Setup(x => x.GetReviewsAsync(1, 1000, It.IsAny<DbFilter<Review>>()))
                .Callback<int, int, DbFilter<Review>>((page, pagesize, filter) => { actualFilter = filter; })
                .Returns(Task.FromResult(expected));
            var sut = new ReviewsController(reviewRepository.Object);
            var actual = sut.Get(111, null, null).Result;
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(actualFilter);
            Assert.AreEqual("UserId", actualFilter.Field);
            Assert.AreEqual(OperatorEnum.Equal, actualFilter.Operator);
            Assert.AreEqual(111, actualFilter.Value);
            reviewRepository.VerifyAll();
        }

        [TestMethod]
        public void Get_ExplicitParams_ReturnsList()
        {
            IEnumerable<Review> expected = new List<Review>{
                new Review {
                    Id = 1,
                    UserId = 2,
                    RestaurantId = 3
                },
                new Review {
                    Id = 1,
                    UserId = 4,
                    RestaurantId = 7
                }
            };
            var reviewRepository = new Mock<IReviewRepository>();
            DbFilter<Review> actualFilter = null;
            reviewRepository.Setup(x => x.GetReviewsAsync(3, 2, It.IsAny<DbFilter<Review>>()))
                .Callback<int, int, DbFilter<Review>>((page, pagesize, filter) => { actualFilter = filter; })
                .Returns(Task.FromResult(expected));
            
            var sut = new ReviewsController(reviewRepository.Object);
            var actual = sut.Get(111, 3, 2).Result;

            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(actualFilter);
            Assert.AreEqual("UserId", actualFilter.Field);
            Assert.AreEqual(OperatorEnum.Equal, actualFilter.Operator);
            Assert.AreEqual(111, actualFilter.Value);
            reviewRepository.VerifyAll();
        }
    }
}
