using System;

namespace Softwriters.RestaurantReviews.Models
{
    public class Review
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int Stars { get; set; }

        public int UserId { get; set; }

        public int RestaurantId { get; set; }

        public User User { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
