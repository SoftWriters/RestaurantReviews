/******************************************************************************
 * Name: IReviewerRepository.cs
 * Purpose: Reviewer Repository Interface that implements methods for 
 *           Reviewer Model
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
    public interface IReviewerService
    {
        ReviewerModelDTO GetReviewerById(int id);
        bool CheckReviewerExists(ReviewerModelDTO reviewer);
        ReviewerModelDTO AddReviewer(ReviewerModelDTO newReviewer);
    }
}
