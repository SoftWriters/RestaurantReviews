/******************************************************************************
 * Name: ReviewerRepository.cs
 * Purpose: Wrapper Reviewer Repository class for Reviewer
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
    public class ReviewerRepository : IReviewerRepository
    {
        IReviewerRepository reviewerRepository = null;

        public ReviewerRepository()
        {
            this.reviewerRepository = new SqlReviewerRepository();
        }

        public ReviewerRepository(IReviewerRepository reviewerRepository)
        {
            this.reviewerRepository = reviewerRepository;
        }

        public ReviewerModelDTO GetReviewerById(int id)
        {
            return this.reviewerRepository.GetReviewerById(id);
        }

        public bool CheckReviewerExists(ReviewerModelDTO reviewer)
        {
            return this.reviewerRepository.CheckReviewerExists(reviewer);
        }

        public ReviewerModelDTO AddReviewer(ReviewerModelDTO newReviewer)
        {
            return this.reviewerRepository.AddReviewer(newReviewer);
        }
    }
}
