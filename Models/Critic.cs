using Softwriters.RestaurantReviews.Models.PrivateModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Softwriters.RestaurantReviews.Models
{
    public class Critic : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; }
    }
}
