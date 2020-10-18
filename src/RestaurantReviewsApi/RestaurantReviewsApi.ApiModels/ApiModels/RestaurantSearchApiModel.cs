using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.ApiModels
{
    public class RestaurantSearchApiModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address_line_1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }
    }
}
