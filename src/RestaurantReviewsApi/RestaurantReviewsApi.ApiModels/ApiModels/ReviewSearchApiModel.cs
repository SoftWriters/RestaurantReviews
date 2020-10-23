using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.ApiModels
{
    public class ReviewSearchApiModel
    {
        [JsonProperty("user_name")]
        public string UserName { get; set; }
        [JsonProperty("restaurant_id")]
        public Guid? RestaurantId { get; set; }
    }
}
