/******************************************************************************
 * Name: ReviewService_UnitTest.cs
 * Purpose: Unit Test cases for Review Service
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Service;
using RestaurantReviews.API.Test.MockRepository;

namespace RestaurantReviews.API.Service.UnitTest
{
    [TestClass]
    public class ReviewService_UnitTest
    {
        IReviewRepository reviewRepository;
        IReviewerRepository reviewerRepository;
        IReviewService reviewService;

        [TestInitialize]
        public void SetUp()
        {
            this.reviewRepository = new ReviewMockRepository();
            this.reviewerRepository = new ReviewerMockRepository();
            this.reviewService = new ReviewService(this.reviewRepository, this.reviewerRepository);
        }

        [TestMethod()]
        public void Get_By_Restaurant_Id()
        {
            ReviewModelList reviewsByRestaurant = this.reviewService.GetReviewsByRestaurant(1);
            Assert.AreNotEqual<ReviewModelList>(reviewsByRestaurant, null);
            Assert.AreNotEqual<int>(reviewsByRestaurant.ReviewList.Count, 0);
            foreach (ReviewModelDTO review in reviewsByRestaurant.ReviewList)
            {
                Assert.AreNotEqual<ReviewModelDTO>(review, null);
                Assert.AreEqual<int>(review.Restaurant.Id, 1);
            }
        }

        [TestMethod()]
        public void Get_By_Restaurant_Id_NotExistent()
        {
            ReviewModelList reviewsByRestaurant = this.reviewService.GetReviewsByRestaurant(int.MaxValue);
            Assert.AreNotEqual<ReviewModelList>(reviewsByRestaurant, null);
            Assert.AreEqual<int>(reviewsByRestaurant.ReviewList.Count, 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void Get_By_Restaurant_Id_Negative()
        {
            ReviewModelList reviewsByRestaurant = this.reviewService.GetReviewsByRestaurant(int.MinValue);
        }

        [TestMethod()]
        public void Get_By_Id()
        {
            ReviewModelDTO review = this.reviewService.GetReviewById(1);
            Assert.AreNotEqual<ReviewModelDTO>(review, null);
            Assert.AreEqual<int>(review.Id, 1);
        }

        [TestMethod()]
        public void Get_By_Id_NotExistent()
        {
            ReviewModelDTO review = this.reviewService.GetReviewById(int.MaxValue);
            Assert.AreEqual<ReviewModelDTO>(review, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void Get_By_Id_Negative()
        {
            ReviewModelDTO review = this.reviewService.GetReviewById(int.MinValue);
        }

        [TestMethod()]
        public void Add()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Id = 4,
                    Name = "Reviewer 4"
                },
                Restaurant = new RestaurantModelDTO()
                {
                    Id = 4
                },
                ReviewedDateTime = DateTime.Now,
                Rating = 3,
                ReviewText = "This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4."
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
            Assert.AreNotEqual<ReviewModelDTO>(newReviewRet, null);
            Assert.AreEqual<string>(newReviewRet.Reviewer.Name, "Reviewer 4");
            Assert.AreEqual<int>(newReviewRet.Restaurant.Id, 4);
            Assert.AreEqual<int>(newReviewRet.Rating, 3);

            Assert.AreEqual<int>(newReview.Id, newReviewRet.Id);
            Assert.AreEqual<string>(newReview.Reviewer.Name, newReviewRet.Reviewer.Name);
            Assert.AreEqual<int>(newReview.Restaurant.Id, newReviewRet.Restaurant.Id);
            Assert.AreEqual<int>(newReview.Rating, newReviewRet.Rating);

            ReviewModelDTO review = this.reviewService.GetReviewById(newReviewRet.Id);
            Assert.AreNotEqual<ReviewModelDTO>(review, null);
            Assert.AreEqual<int>(review.Id, newReviewRet.Id);
            Assert.AreEqual<string>(review.Reviewer.Name, newReviewRet.Reviewer.Name);
            Assert.AreEqual<int>(review.Restaurant.Id, newReviewRet.Restaurant.Id);
            Assert.AreEqual<int>(review.Rating, newReviewRet.Rating);
        }

        [TestMethod()]
        public void Add_Reviewer_Not_Exist()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Name = "Reviewer 5"
                },
                Restaurant = new RestaurantModelDTO()
                {
                    Id = 4
                },
                ReviewedDateTime = DateTime.Now,
                Rating = 3,
                ReviewText = "This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4."
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
            Assert.AreNotEqual<ReviewModelDTO>(newReviewRet, null);
            Assert.AreEqual<string>(newReviewRet.Reviewer.Name, "Reviewer 5");
            Assert.AreEqual<int>(newReviewRet.Restaurant.Id, 4);
            Assert.AreEqual<int>(newReviewRet.Rating, 3);

            Assert.AreEqual<int>(newReview.Id, newReviewRet.Id);
            Assert.AreEqual<string>(newReview.Reviewer.Name, newReviewRet.Reviewer.Name);
            Assert.AreEqual<int>(newReview.Restaurant.Id, newReviewRet.Restaurant.Id);
            Assert.AreEqual<int>(newReview.Rating, newReviewRet.Rating);

            ReviewModelDTO review = this.reviewService.GetReviewById(newReviewRet.Id);
            Assert.AreNotEqual<ReviewModelDTO>(review, null);
            Assert.AreEqual<int>(review.Id, newReviewRet.Id);
            Assert.AreEqual<string>(review.Reviewer.Name, newReviewRet.Reviewer.Name);
            Assert.AreEqual<int>(review.Restaurant.Id, newReviewRet.Restaurant.Id);
            Assert.AreEqual<int>(review.Rating, newReviewRet.Rating);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void Add_Reviewer_Not_Exist_Name_Null()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Name = null
                },
                Restaurant = new RestaurantModelDTO()
                {
                    Id = 4
                },
                ReviewedDateTime = DateTime.Now,
                Rating = 3,
                ReviewText = "This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4."
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void Add_Reviewer_Not_Exist_Name_Empty()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Name = string.Empty
                },
                Restaurant = new RestaurantModelDTO()
                {
                    Id = 4
                },
                ReviewedDateTime = DateTime.Now,
                Rating = 3,
                ReviewText = "This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4."
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void Add_All_Null()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = null,
                Restaurant = null,
                ReviewedDateTime = DateTime.Now,
                Rating = 1,
                ReviewText = null
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void Add_All_Reviewer_Null()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = null,
                Restaurant = new RestaurantModelDTO()
                {
                    Id = 4
                },
                ReviewedDateTime = DateTime.Now,
                Rating = 1,
                ReviewText = "This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4."
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void Add_All_Restaurant_Null()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Id = 5,
                    Name = "Reviewer 5"
                },
                Restaurant = null,
                ReviewedDateTime = DateTime.Now,
                Rating = 1,
                ReviewText = "This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4. This is sample review text by Reviewer 5 for Restaurant 4."
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void Add_All_ReviwerText_Null()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Id = 5,
                    Name = "Reviewer 5"
                },
                Restaurant = new RestaurantModelDTO()
                {
                    Id = 4
                },
                ReviewedDateTime = DateTime.Now,
                Rating = 1,
                ReviewText = null
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void Add_All_Empty()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Name = string.Empty
                },
                Restaurant = new RestaurantModelDTO()
                {
                    Id = 1
                },
                ReviewedDateTime = DateTime.Now,
                Rating = 1,
                ReviewText = string.Empty
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Date_Yesterday()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Id = 4,
                    Name = "Reviewer 4"
                },
                Restaurant = new RestaurantModelDTO()
                {
                    Id = 4
                },
                ReviewedDateTime = DateTime.Now.AddDays(-1),
                Rating = 3,
                ReviewText = "This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4."
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Date_Tomorrow()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Id = 4,
                    Name = "Reviewer 4"
                },
                Restaurant = new RestaurantModelDTO()
                {
                    Id = 4
                },
                ReviewedDateTime = DateTime.Now.AddDays(1),
                Rating = 3,
                ReviewText = "This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4."
            };
            ReviewModelDTO newReviewRet = this.reviewService.AddReview(newReview);
        }
    }
}
