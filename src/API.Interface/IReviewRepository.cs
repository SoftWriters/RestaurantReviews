/******************************************************************************
 * Name: IReviewRepository.cs
 * Purpose: Review Repository Interface that implements methods for Review 
 *           Model
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using RestaurantReviews.API.Model.DTO;

namespace RestaurantReviews.API.Interface
{
    public interface IReviewRepository : IAPIDataRepository
    {
        ReviewModelList GetReviewsByRestaurant(int restaurant_id);
        ReviewModelList GetReviewsByReviewer(int reviewer_id);
        ReviewModelDTO GetReviewById(int id);
        ReviewModelDTO AddReview(ReviewModelDTO newReview);
        ReviewModelDTO DeleteReview(int id);
    }
}
