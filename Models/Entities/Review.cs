using System;
using System.Text.Json.Serialization;

namespace Softwriters.RestaurantReviews.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public double Stars { get; set; }

        public int UserId { get; set; }

        public int RestaurantId { get; set; }

        [JsonIgnore]
        public Critic Critic { get; set; }

        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
    }
}
