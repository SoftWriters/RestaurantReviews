using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Entities
{
    public class Review
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

        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}