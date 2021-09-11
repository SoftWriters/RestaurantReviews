using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Softwriters.RestaurantReviews.Models.Entities
{
    public class Critic
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; }
    }
}
