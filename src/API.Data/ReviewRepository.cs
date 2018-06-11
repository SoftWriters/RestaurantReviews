/******************************************************************************
 * Name: ReviewRepository.cs
 * Purpose: Wrapper Repository class for Review
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Data.SqlServer;

namespace RestaurantReviews.API.Data
{
    public class ReviewRepository : IReviewRepository
    {
        IReviewRepository reviewRepository = null;

        public ReviewRepository(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public ReviewRepository()
        {
            this.reviewRepository = new SqlReviewRepository();
        }

        public ReviewModelList GetReviewsByRestaurant(int restaurant_id)
        {
            return this.reviewRepository.GetReviewsByRestaurant(restaurant_id);
        }

        public ReviewModelList GetReviewsByReviewer(int reviewer_id)
        {
            return this.reviewRepository.GetReviewsByReviewer(reviewer_id);
        }

        public ReviewModelDTO GetReviewById(int id)
        {
            return this.reviewRepository.GetReviewById(id);
        }

        public ReviewModelDTO AddReview(ReviewModelDTO newReview)
        {
            return this.reviewRepository.AddReview(newReview);
        }

        public ReviewModelDTO DeleteReview(int id)
        {
            return this.reviewRepository.DeleteReview(id);
        }
    }
}
