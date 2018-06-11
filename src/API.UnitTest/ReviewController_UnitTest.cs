/******************************************************************************
 * Name: ReviewController_UnitTest.cs
 * Purpose: Unit Tests cases for Review MVC Controller
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Service;
using RestaurantReviews.API.Controllers;
using RestaurantReviews.API.Test.MockRepository;

namespace RestaurantReviews.API.UnitTest
{
    [TestClass]
    public class ReviewController_UnitTest
    {
        IReviewRepository reviewRepository;
        IReviewerRepository reviewerRepository;
        IReviewService reviewService;
        IReviewerService reviewerService;
        ReviewController reviewController;

        [TestInitialize]
        public void SetUp()
        {
            this.reviewRepository = new ReviewMockRepository();
            this.reviewerRepository = new ReviewerMockRepository();
            this.reviewService = new ReviewService(this.reviewRepository, this.reviewerRepository);
            this.reviewerService = new ReviewerService(this.reviewerRepository);
            this.reviewController = new ReviewController(this.reviewService, this.reviewerService);
        }

        [TestMethod()]
        public void Get_By_Restaurant_Id()
        {
            APIResponseDTO response = this.reviewController.GetByRestaurantId(1);
            ReviewModelList reviewsByRestaurant = (ReviewModelList)response.Data;
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
            APIResponseDTO response = this.reviewController.GetByRestaurantId(int.MaxValue);
            ReviewModelList reviewsByRestaurant = (ReviewModelList)response.Data;
            Assert.AreNotEqual<ReviewModelList>(reviewsByRestaurant, null);
            Assert.AreEqual<int>(reviewsByRestaurant.ReviewList.Count, 0);
        }

        [TestMethod()]
        public void Get_By_Restaurant_Id_Negative()
        {
            APIResponseDTO response = this.reviewController.GetByRestaurantId(int.MinValue);
            ReviewModelList reviewsByRestaurant = (ReviewModelList)response.Data;
            Assert.AreEqual<ReviewModelList>(reviewsByRestaurant, null);
        }

        [TestMethod()]
        public void Get_By_Id()
        {
            APIResponseDTO response = this.reviewController.Get(1);
            ReviewModelDTO review = (ReviewModelDTO)response.Data;
            Assert.AreNotEqual<ReviewModelDTO>(review, null);
            Assert.AreEqual<int>(review.Id, 1);
        }

        [TestMethod()]
        public void Get_By_Id_NotExistent()
        {
            APIResponseDTO response = this.reviewController.Get(int.MaxValue);
            ReviewModelDTO review = (ReviewModelDTO)response.Data;
            Assert.AreEqual<ReviewModelDTO>(review, null);
        }

        [TestMethod()]
        public void Get_By_Id_Negative()
        {
            APIResponseDTO response = this.reviewController.Get(int.MinValue);
            ReviewModelDTO review = (ReviewModelDTO)response.Data;
            Assert.AreEqual<ReviewModelDTO>(review, null);
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
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreEqual<ErrorDTO>(response.Error, null);
            Assert.AreNotEqual<APIBaseDTO>(response.Data, null);
            ReviewModelDTO newReviewRet = (ReviewModelDTO)response.Data;
            Assert.AreNotEqual<ReviewModelDTO>(newReviewRet, null);
            Assert.AreEqual<string>(newReviewRet.Reviewer.Name, "Reviewer 4");
            Assert.AreEqual<int>(newReviewRet.Restaurant.Id, 4);
            Assert.AreEqual<int>(newReviewRet.Rating, 3);

            Assert.AreEqual<int>(newReview.Id, newReviewRet.Id);
            Assert.AreEqual<string>(newReview.Reviewer.Name, newReviewRet.Reviewer.Name);
            Assert.AreEqual<int>(newReview.Restaurant.Id, newReviewRet.Restaurant.Id);
            Assert.AreEqual<int>(newReview.Rating, newReviewRet.Rating);

            response = this.reviewController.Get(newReviewRet.Id);
            ReviewModelDTO review = (ReviewModelDTO)response.Data;
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
                ReviewText = "This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4."
            };
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreEqual<ErrorDTO>(response.Error, null);
            Assert.AreNotEqual<APIBaseDTO>(response.Data, null);
            ReviewModelDTO newReviewRet = (ReviewModelDTO)response.Data;
            Assert.AreNotEqual<ReviewModelDTO>(newReviewRet, null);
            Assert.AreEqual<string>(newReviewRet.Reviewer.Name, "Reviewer 5");
            Assert.AreEqual<int>(newReviewRet.Restaurant.Id, 4);
            Assert.AreEqual<int>(newReviewRet.Rating, 3);

            Assert.AreEqual<int>(newReview.Id, newReviewRet.Id);
            Assert.AreEqual<string>(newReview.Reviewer.Name, newReviewRet.Reviewer.Name);
            Assert.AreEqual<int>(newReview.Restaurant.Id, newReviewRet.Restaurant.Id);
            Assert.AreEqual<int>(newReview.Rating, newReviewRet.Rating);

            response = this.reviewController.Get(newReviewRet.Id);
            ReviewModelDTO review = (ReviewModelDTO)response.Data;
            Assert.AreNotEqual<ReviewModelDTO>(review, null);
            Assert.AreEqual<int>(review.Id, newReviewRet.Id);
            Assert.AreEqual<string>(review.Reviewer.Name, newReviewRet.Reviewer.Name);
            Assert.AreEqual<int>(review.Restaurant.Id, newReviewRet.Restaurant.Id);
            Assert.AreEqual<int>(review.Rating, newReviewRet.Rating);
        }

        [TestMethod()]
        public void Add_Reviewer_Not_Exist_Name_Null()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = null,
                Restaurant = new RestaurantModelDTO()
                {
                    Name = null
                },
                ReviewedDateTime = DateTime.Now,
                Rating = 1,
                ReviewText = "This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4."
            };
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod()]
        public void Add_Reviewer_Not_Exist_Name_Empty()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = null,
                Restaurant = new RestaurantModelDTO()
                {
                    Name = string.Empty
                },
                ReviewedDateTime = DateTime.Now,
                Rating = 1,
                ReviewText = "This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4."
            };
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod()]
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
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod()]
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
                ReviewText = "This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4."
            };
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod()]
        public void Add_All_Restaurant_Null()
        {
            ReviewModelDTO newReview = new ReviewModelDTO()
            {
                Reviewer = new ReviewerModelDTO()
                {
                    Id = 4,
                    Name = "Reviewer 4"
                },
                Restaurant = null,
                ReviewedDateTime = DateTime.Now,
                Rating = 1,
                ReviewText = "This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4. This is sample review text by Reviewer 4 for Restaurant 4."
            };
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod()]
        public void Add_All_ReviewText_Null()
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
                Rating = 1,
                ReviewText = null
            };
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod()]
        public void Add_All_ReviewText_Empty()
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
                Rating = 1,
                ReviewText = string.Empty
            };
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod()]
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
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod()]
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
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }

        [TestMethod()]
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
            ReviewAPIRequestDTO request = new ReviewAPIRequestDTO()
            {
                Header = new APIRequestHeaderDTO()
                {
                    RequestID = 1
                },
                Data = newReview
            };
            APIResponseDTO response = this.reviewController.Post(request);
            Assert.AreNotEqual<ErrorDTO>(response.Error, null);
            Assert.AreEqual<APIBaseDTO>(response.Data, null);
        }
    }
}
