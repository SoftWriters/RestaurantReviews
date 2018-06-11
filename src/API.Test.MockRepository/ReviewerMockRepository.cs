/******************************************************************************
 * Name: ReviewerMockRespository.cs
 * Purpose: In Memory Mock Respository of Reviewers for unit test cases
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Model.DTO;

namespace RestaurantReviews.API.Test.MockRepository
{
    public class ReviewerMockRepository : IReviewerRepository
    {
        private ReviewerModelList reviewers;

        public ReviewerMockRepository()
        {
            this.reviewers = new ReviewerModelList();
            this.reviewers.ReviewerList = new List<ReviewerModelDTO>()
            {

                new ReviewerModelDTO() {
                    Id = 1,
                    Name = "Reviewer 1"
                },

                new ReviewerModelDTO() {
                    Id = 2,
                    Name = "Reviewer 2"
                },

                new ReviewerModelDTO() {
                    Id = 3,
                    Name = "Reviewer 3"
                },

                new ReviewerModelDTO() {
                    Id = 4,
                    Name = "Reviewer 4"
                }
            };
        }

        protected ReviewerModelList GetReviewers()
        {
            return reviewers;
        }

        public ReviewerModelDTO GetReviewerById(int id)
        {
            return GetReviewers().ReviewerList.Find(x => x.Id == id);
        }

        public bool CheckReviewerExists(ReviewerModelDTO reviewer)
        {
            return (GetReviewers().ReviewerList.Find(x => x.Name.CompareTo(reviewer.Name) == 0) != null);
        }

        public ReviewerModelDTO AddReviewer(ReviewerModelDTO newReviewer)
        {
            ReviewerModelDTO lastReviewer = GetReviewers().ReviewerList.Last();
            ReviewerModelDTO ret = new ReviewerModelDTO()
            {
                Id = lastReviewer.Id + 1,
                Name = newReviewer.Name
            };
            reviewers.ReviewerList.Add(ret);
            return ret;

        }
    }
}
