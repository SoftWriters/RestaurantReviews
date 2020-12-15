using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.Logic.Model.Review.Create
{
    public class CreateReviewRequest
    {
        /// <summary>
        /// WARNING!  This is a bad idea!  The userId
        /// should be INFERRED based on the current auth credentials,
        /// but since this app has no auth we must use this bad practice
        /// of passing the userId in the request
        /// </summary>
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RestaurantId { get; set; }
        // TODO: Validate string length
        [Required]
        public string ReviewText { get; set; }
    }
}
