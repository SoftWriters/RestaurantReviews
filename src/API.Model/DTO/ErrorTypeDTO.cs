/******************************************************************************
 * Name: ErrorTypeDTO.cs
 * Purpose: Error Type DTO Model used in Error DTO Model to return error type
 *           information in all API responses
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class ErrorTypeDTO
    {
        public int ErrorTypeID { get; set; }
        public string ErrorName { get; set; }
        public string Description { get; set; }
    }
}
