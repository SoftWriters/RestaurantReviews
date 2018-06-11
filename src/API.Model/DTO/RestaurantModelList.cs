/******************************************************************************
 * Name: RestaurantModelList.cs
 * Purpose: Restaurant Model List DTO
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class RestaurantModelList : APIBaseDTO
    {
        public List<RestaurantModelDTO> RestaurantList { get; set; }
    }
}
