using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.Logic.Model.Review.Delete
{
    public class DeleteReviewRequest
    {
        [Required]
        public string ReviewId { get; set; }
    }
}
