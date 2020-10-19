using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.ApiModels.ApiModels
{
    public class AccessTokenApiModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
