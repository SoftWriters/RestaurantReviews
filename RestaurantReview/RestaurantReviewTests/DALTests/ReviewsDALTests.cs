using RestaurantReview.DAL;
using RestaurantReview.Models;
using RestaurantReview.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RestaurantReviewTests.DALTests
{
    public class ReviewsDALTests
    {
        [Fact]
        public void PostReview_AddsNewReview()
        {
            var review = new PostReview
            {
                RestaurantId = 1,
                UserId = 2,
                ReviewText = "add this review. restaurant was great."
            };

            var dal = new ReviewsDAL(new Conn().AWSconnstring());
            var result = dal.PostReview(review);
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetReviews_ReturnsList()
        {
            var dal = new ReviewsDAL(new Conn().AWSconnstring());
            var list = dal.GetAllReviews();

            Assert.True(list.Count > 1);
        }

        [Fact]
        public void UpdateReview_ReturnsTrue()
        {
            var dal = new ReviewsDAL(new Conn().AWSconnstring());
            var review = dal.GetAllReviews().FirstOrDefault();
            var updatedReview = new UpdateReview
            {
                ReviewId = review.ReviewId,
                ReviewText = "my updated text"
            };
            bool updated = dal.UpdateReview(updatedReview).IsSuccessful;
            Assert.True(updated);
        }
        [Fact]
        public void DeletesReview_LastIdChangedAfterDeletion()
        {
            var dal = new ReviewsDAL(new Conn().AWSconnstring());
            var review = dal.GetAllReviews().FirstOrDefault();

            dal.DeleteReview(review.ReviewId);
            var currentFirstReview = dal.GetAllReviews().FirstOrDefault();

            Assert.True(!review.ReviewId.Equals(currentFirstReview.ReviewId));
        }

        [Fact]
        public void DeletesReview_ReturnsTrue()
        {
            var dal = new ReviewsDAL(new Conn().AWSconnstring());
            var review = dal.GetAllReviews().FirstOrDefault();

            bool deleted = dal.DeleteReview(review.ReviewId);

            Assert.True(deleted);
        }
    }
}
