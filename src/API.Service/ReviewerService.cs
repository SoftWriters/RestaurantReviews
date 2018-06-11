/******************************************************************************
 * Name: ReviewerService.cs
 * Purpose: Reviewer Service class definition
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
    public class ReviewerService : APIServiceBase, IReviewerService
    {
        IReviewerRepository reviewerRepository = null;

        public ReviewerService()
        {
            this.reviewerRepository = new ReviewerRepository();
        }

        public ReviewerService(IReviewerRepository reviewerRepository)
        {
            this.reviewerRepository = reviewerRepository;
        }

        public ReviewerModelDTO GetReviewerById(int id)
        {
            return this.reviewerRepository.GetReviewerById(id);
        }

        public bool CheckReviewerExists(ReviewerModelDTO reviewer)
        {
            if (reviewer == null || string.IsNullOrEmpty(reviewer.Name))
                throw new Exception("Invalid Reviewer object or Null/Empty Name");

            return this.reviewerRepository.CheckReviewerExists(reviewer);
        }

        public ReviewerModelDTO AddReviewer(ReviewerModelDTO newReviewer)
        {
            ValidateModel(newReviewer);
            return this.reviewerRepository.AddReviewer(newReviewer);
        }
    }
}
