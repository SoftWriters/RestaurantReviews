using System;

namespace RestaurantReviews.Web.Data.Entities
{
    public class Review
    {
        public int Id { get; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; }

        public int UserId { get; set; }

        public string Content { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
