/******************************************************************************
 * Name: ReviewService.cs
 * Purpose: Review Service class definition
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Data;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.API.Service
{
    public class ReviewService : APIServiceBase, IReviewService
    {
        IReviewRepository reviewRepository;
        IReviewerRepository reviewerRepository;

        public ReviewService()
        {
            this.reviewRepository = new ReviewRepository();
            this.reviewerRepository = new ReviewerRepository();
        }

        public ReviewService(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository)
        {
            this.reviewRepository = reviewRepository;
            this.reviewerRepository = reviewerRepository;
        }

        public ReviewModelList GetReviewsByRestaurant(int restaurant_id)
        {
            if (restaurant_id <= 0) throw new Exception("Restaurant Id number is negative");
            return this.reviewRepository.GetReviewsByRestaurant(restaurant_id);
        }

        public ReviewModelList GetReviewsByReviewer(int reviewer_id)
        {
            if (reviewer_id <= 0) throw new Exception("Reviewer Id number is negative");
            return this.reviewRepository.GetReviewsByReviewer(reviewer_id);
        }

        public ReviewModelDTO GetReviewById(int id)
        {
            if(id <= 0) { throw new Exception("Review Id number is negative"); }
            return this.reviewRepository.GetReviewById(id);
        }

        public ReviewModelDTO AddReview(ReviewModelDTO newReview)
        {
            ValidateModel(newReview);

            ReviewerService reviewerService = new ReviewerService(this.reviewerRepository);
            if (!reviewerService.CheckReviewerExists(newReview.Reviewer))
                newReview.Reviewer = reviewerService.AddReviewer(newReview.Reviewer);

            return this.reviewRepository.AddReview(newReview);
        }

        public ReviewModelDTO DeleteReview(int id)
        {
            return this.reviewRepository.DeleteReview(id);
        }
    }
}
