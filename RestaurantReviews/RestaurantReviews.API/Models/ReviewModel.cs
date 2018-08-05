using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.WebServices.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }

        [Range(0, 5)]
        public int FoodGrade { get; set; }

        [Range(0, 5)]
        public int ServiceGrade { get; set; }

        [Range(0, 5)]
        public int LookFeelGrade { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }

        public RestaurantModel Restaurant { get; set; }
        public UserModel User { get; set; }
    }
}