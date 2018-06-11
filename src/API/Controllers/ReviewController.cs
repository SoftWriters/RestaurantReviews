/******************************************************************************
 * Name: ReviewController.cs
 * Purpose: API operations on Review(s)
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Data;
using RestaurantReviews.API.Service;

namespace RestaurantReviews.API.Controllers
{
    [Produces("application/json")]
    [Route("Review")]
    public class ReviewController : APIBaseController
    {
        IReviewService reviewService = null;
        IReviewerService reviewerService = null;

        public ReviewController(IReviewService reviewService, IReviewerService reviewerService)
        {
            this.reviewService = reviewService;
            this.reviewerService = reviewerService;
        }

        // GET: Review/Restaurant?id=1
        [HttpGet("Restaurant", Name = "GetReviewsByRestaurantId")]
        public APIResponseDTO GetByRestaurantId(int id)
        {
            try
            {
                ReviewModelList reviews = this.reviewService.GetReviewsByRestaurant(id);
                return GetDataDTO(reviews);
            }
            catch (Exception ex)
            {
                return GetErrorDTO(ex);
            }
        }

        // GET: Review/Reviewer?id=1
        [HttpGet("Reviewer", Name = "GetReviewsByReviewerId")]
        public APIResponseDTO GetByReviewerId(int id)
        {
            try
            {
                ReviewModelList reviews = this.reviewService.GetReviewsByReviewer(id);
                return GetDataDTO(reviews);
            }
            catch (Exception ex)
            {
                return GetErrorDTO(ex);
            }
        }

        // GET: Review/5
        [HttpGet("{id}", Name = "GetReview")]
        public APIResponseDTO Get(int id)
        {
            try
            {
                ReviewModelDTO review = this.reviewService.GetReviewById(id);
                return GetDataDTO(review);
            }
            catch (Exception ex)
            {
                return GetErrorDTO(ex);
            }
        }
        
        // POST: Review
        [HttpPost]
        public APIResponseDTO Post([FromBody]ReviewAPIRequestDTO request)
        {
            try
            {
                ReviewModelDTO newReview = request.Data;
                newReview = this.reviewService.AddReview(newReview);
                return GetDataDTO(newReview);
            }
            catch (Exception ex)
            {
                return GetErrorDTO(ex);
            }
        }
        
        // DELETE: Review/5
        [HttpDelete("{id}")]
        public APIResponseDTO Delete(int id)
        {
            try
            {
                ReviewModelDTO review = this.reviewService.DeleteReview(id); ;
                return GetDataDTO(review);
            }
            catch (Exception ex)
            {
                return GetErrorDTO(ex);
            }
            
        }
    }
}
