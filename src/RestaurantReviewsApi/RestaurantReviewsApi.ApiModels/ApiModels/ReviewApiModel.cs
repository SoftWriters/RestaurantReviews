using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.ApiModels
{
    public class ReviewApiModel
    {
        [JsonProperty("restaurant_id")]
        public Guid? RestaurantId { get; set; }
        [JsonProperty("review_id")]
        public Guid? ReviewId { get; set; }
        [JsonProperty("user_name")]
        public string UserName { get; set; }
        [JsonProperty("rating")]
        public int Rating { get; set; }
        [JsonProperty("details")]
        public string Details { get; set; }
    }
}
