using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.ApiModels
{
    public class RestaurantApiModel
    {
        [JsonProperty("restaurant_id")]
        public Guid? RestaurantId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address_line_1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("address_line_2")]
        public string AddressLine2 { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("average_rating")]
        public float? AverageRating { get; set; }
    }
}
