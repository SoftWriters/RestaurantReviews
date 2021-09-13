using Softwriters.RestaurantReviews.Models.PrivateModels;
using System;
using System.Text.Json.Serialization;

namespace Softwriters.RestaurantReviews.Models
{
    public class Review : EntityBase
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int RestaurantId { get; set; }

        public double Stars { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public Critic Critic { get; set; }

        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
    }
}
