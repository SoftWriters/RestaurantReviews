using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.API.Dtos
{
    public class ReviewDto
    {
        [StringLength(1000, ErrorMessage = "Comment can't be longer than 1000 characters")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "RestaurantId is required")]
        public Guid RestaurantId { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }
    }
}
