using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.API.Model.DTO
{
    public class ReviewerModelDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Reviewer Name is required")]
        [StringLength(100, ErrorMessage = "Reviewer Name must not exceed 100 characters")]
        public string Name { get; set; }
    }
}
