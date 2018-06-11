/******************************************************************************
 * Name: ReviewerModelList.cs
 * Purpose: Reviewer Model List DTO class definition
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class ReviewerModelList : APIBaseDTO
    {
        public List<ReviewerModelDTO> ReviewerList { get; set; }
    }
}
