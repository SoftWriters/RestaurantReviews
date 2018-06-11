/******************************************************************************
 * Name: ReviewModelDTO.cs
 * Purpose: Review DTO Model class definition
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RestaurantReviews.API.Common;

namespace RestaurantReviews.API.Model.DTO
{
    public class ReviewModelDTO : APIBaseDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Reviewer is required")]
        public ReviewerModelDTO Reviewer { get; set; }
        [Required(ErrorMessage = "Restraunt being reviewed is required")]
        public RestaurantModelDTO Restaurant { get; set; }
        [Required(ErrorMessage = "Review Date/Time is required")]
        [TodayDate(ErrorMessage = "Review Date/Time must be sometime today before now")]
        public DateTime ReviewedDateTime { get; set; }
        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must between 1 and 5")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Review text is required")]
        [StringLength(5000, ErrorMessage = "Review text must be less than 5000 characters")]
        public string ReviewText { get; set; }
    }
}
