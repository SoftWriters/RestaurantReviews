/******************************************************************************
 * Name: ErrorDTO.cs
 * Purpose: Error DTO Model to return error info in API response
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class ErrorDTO
    {
        public int ErrorID { get; set; }
        public int RequestID { get; set; }
        public DateTime ErrorDateTime { get; set; }
        public ErrorTypeDTO ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
    }
}
