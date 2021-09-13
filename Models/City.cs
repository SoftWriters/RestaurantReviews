using Softwriters.RestaurantReviews.Models.PrivateModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Softwriters.RestaurantReviews.Models
{
    public class City : EntityBase
    {
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
