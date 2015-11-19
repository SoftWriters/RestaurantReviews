using RestaurantReview.BL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RestaurantReview.BL.Base;

namespace RestaurantReview.BL.Model
{
    public partial class Review : ModelBase.Model<Review>
    {

        public int RestaurantID { get; set; }

        public int UserID { get; set; }

        private int _rating;

        [Range(1,5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating {
            get
            {
                return _rating;
            }
             set
            {
                if (value < 1 || value > 5)
                {
                    throw new Exception("Rating must be between 1 and 5");
                } else
                {
                    _rating = value;
                }
            }
        }

        [Required]
        public string Comments { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
