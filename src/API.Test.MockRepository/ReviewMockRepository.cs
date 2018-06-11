/******************************************************************************
 * Name: ReviewMockRespository.cs
 * Purpose: In Memory Mock Respository of Reviews for unit test cases
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
    public class ReviewMockRepository : IReviewRepository
    {
        ReviewModelList reviews;

        public ReviewMockRepository()
        {
            this.reviews = new ReviewModelList();
            this.reviews.ReviewList = new List<ReviewModelDTO>() 
            {
                new ReviewModelDTO() {
                    Id = 1,
                    Reviewer = new ReviewerModelDTO() {
                        Id = 1,
                        Name = "Reviewer 1"
                    },
                    Restaurant = new RestaurantModelDTO()
                    {
                        Id = 1,
                        Name = "Restaurant 1",
                        City = "Pittsburgh"
                    },
                    ReviewedDateTime = DateTime.Now,
                    Rating = 1,
                    ReviewText = "This is a sample review text by Reviewer 1 for Restaurant 1. This is a sample review text by Reviewer 1 for Restaurant 1.This is a sample review text by Reviewer 1 for Restaurant 1.This is a sample review text by Reviewer 1 for Restaurant 1.This is a sample review text by Reviewer 1 for Restaurant 1.This is a sample review text by Reviewer 1 for Restaurant 1."
                },
                new ReviewModelDTO() {
                    Id = 2,
                    Reviewer = new ReviewerModelDTO() {
                        Id = 1,
                        Name = "Reviewer 1"
                    },
                    Restaurant = new RestaurantModelDTO()
                    {
                        Id = 2,
                        Name = "Restaurant 2",
                        City = "Pittsburgh"
                    },
                    ReviewedDateTime = DateTime.Now,
                    Rating = 5,
                    ReviewText = "This is a sample review text by Reviewer 1 for Restaurant 2. This is a sample review text by Reviewer 1 for Restaurant 2.This is a sample review text by Reviewer 1 for Restaurant 2.This is a sample review text by Reviewer 1 for Restaurant 2.This is a sample review text by Reviewer 1 for Restaurant 2.This is a sample review text by Reviewer 1 for Restaurant 2."
                },
                new ReviewModelDTO() {
                    Id = 3,
                    Reviewer = new ReviewerModelDTO() {
                        Id = 2,
                        Name = "Reviewer 2"
                    },
                    Restaurant = new RestaurantModelDTO()
                    {
                        Id = 3,
                        Name = "Restaurant 3",
                        City = "Morgantown"
                    },
                    ReviewedDateTime = DateTime.Now,
                    Rating = 2,
                    ReviewText = "This is a sample review text by Reviewer 2 for Restaurant 3. This is a sample review text by Reviewer 2 for Restaurant 3.This is a sample review text by Reviewer 2 for Restaurant 3.This is a sample review text by Reviewer 2 for Restaurant 3.This is a sample review text by Reviewer 2 for Restaurant 3.This is a sample review text by Reviewer 2 for Restaurant 3."
                },
                new ReviewModelDTO() {
                    Id = 4,
                    Reviewer = new ReviewerModelDTO() {
                        Id = 3,
                        Name = "Reviewer 3"
                    },

                    Restaurant = new RestaurantModelDTO()
                    {
                        Id = 4,
                        Name = "Restaurant 4",
                        City = "New Stanton"
                    },
                    ReviewedDateTime = DateTime.Now,
                    Rating = 4,
                    ReviewText = "This is a sample review text by Reviewer 3 for Restaurant 4. This is a sample review text by Reviewer 3 for Restaurant 4. This is a sample review text by Reviewer 3 for Restaurant 4. This is a sample review text by Reviewer 3 for Restaurant 4. This is a sample review text by Reviewer 3 for Restaurant 4. This is a sample review text by Reviewer 3 for Restaurant 4."
                }
            };
        }

        protected ReviewModelList GetReviews()
        {
            return reviews;
        }

        public ReviewModelList GetReviewsByRestaurant(int restaurant_id)
        {
            return new ReviewModelList()
            {
                ReviewList = GetReviews().ReviewList.Where(x => x.Restaurant.Id == restaurant_id).ToList()
            };
        }

        public ReviewModelList GetReviewsByReviewer(int reviewer_id)
        {
            return new ReviewModelList()
            {
                ReviewList = GetReviews().ReviewList.Where(x => x.Reviewer.Id == reviewer_id).ToList()
            };
        }

        public ReviewModelDTO GetReviewById(int id)
        {
            return GetReviews().ReviewList.Find(x => x.Id == id);
        }

        public ReviewModelDTO AddReview(ReviewModelDTO newReview)
        {
            ReviewModelDTO lastReview = GetReviews().ReviewList.Last();
            newReview.Id = lastReview.Id + 1;
            reviews.ReviewList.Add(newReview);
            return newReview;
        }

        public ReviewModelDTO DeleteReview(int id)
        {
            ReviewModelDTO review = GetReviewById(id);
            if (review == null) throw new Exception("Review Id #" + id.ToString() + " does not exists");
            reviews.ReviewList.Remove(review);
            return review;
        }
    }
}
