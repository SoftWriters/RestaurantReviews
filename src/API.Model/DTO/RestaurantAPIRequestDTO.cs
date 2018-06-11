/******************************************************************************
 * Name: RestaurantAPIRequestDTO.cs
 * Purpose: Restaurant API Request DTO Model to accept data in API POST calls
 *           in Restaurant API Controller
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class RestaurantAPIRequestDTO 
    {
        public APIRequestHeaderDTO Header { get; set; }
        public RestaurantModelDTO Data { get; set; }
    }
}
