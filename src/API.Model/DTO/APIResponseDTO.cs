/******************************************************************************
 * Name: APIResponseDTO.cs
 * Purpose: Standard API Response Model for all RESTful APIs
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class APIResponseDTO
    {
        public ErrorDTO Error { get; set; }
        public APIBaseDTO Data { get; set; }
    }
}
