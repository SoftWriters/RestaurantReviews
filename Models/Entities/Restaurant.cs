using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Softwriters.RestaurantReviews.Models.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RestaurantTypeId { get; set; }

        [JsonIgnore]
        public RestaurantType RestaurantType { get; set; }

        public int CityId { get; set; }

        [JsonIgnore]
        public City City { get; set; }

        public int MenuId { get; set; }

        [JsonIgnore]
        public Menu Menu { get; set; }

        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; }
    }
}
