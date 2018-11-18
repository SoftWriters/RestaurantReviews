using System;

namespace RestaurantReviews.Model
{
    public class Review : IEntityBase
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public DateTime WhenCreated { get; set; } = DateTime.Now;
        public string Url { get; set; }
        public int Rating { get; set; }
    }
}