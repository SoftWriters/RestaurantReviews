/******************************************************************************
 * Name: RestaurantModelDTO.cs
 * Purpose: Restaurant DTO Model class definition
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class RestaurantModelDTO : APIBaseDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Restaurant Name is required")]
        [StringLength(100, ErrorMessage = "Restaurant Name cannot be more than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Restaurant City is required")]
        [StringLength(50, ErrorMessage = "Restaurant City Name cannot be more than 50 characters")]
        public string City { get; set; }
    }
}
