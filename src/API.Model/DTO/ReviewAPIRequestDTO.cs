/******************************************************************************
 * Name: ReviewAPIRequestDTO.cs
 * Purpose: Review API Request DTO Model to accept data in API POST calls in 
 *           Review API Controller
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class ReviewAPIRequestDTO
    {
        public APIRequestHeaderDTO Header { get; set; }
        public ReviewModelDTO Data { get; set; }
    }
}
