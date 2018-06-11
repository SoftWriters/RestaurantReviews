/******************************************************************************
 * Name: APIRequestHeaderDTO.cs
 * Purpose: Request Header DTO Model object to accept standard header
 *           attributes in all API POST requests
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class APIRequestHeaderDTO
    {
        public int RequestID { get; set; }
   }
}
