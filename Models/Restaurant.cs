using Softwriters.RestaurantReviews.Models.PrivateModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Softwriters.RestaurantReviews.Models
{
    public class Restaurant : EntityBase
    {
        public int CityId { get; set; }

        public int MenuId { get; set; }

        public string Name { get; set; }

        public int RestaurantTypeId { get; set; }

        [JsonIgnore]
        public City City { get; set; }

        [JsonIgnore]
        public Menu Menu { get; set; }

        [JsonIgnore]
        public RestaurantType RestaurantType { get; set; }

        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; }
    }
}