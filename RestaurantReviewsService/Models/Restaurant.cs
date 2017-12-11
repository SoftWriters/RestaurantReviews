using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RestaurantReviews.Models
{
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? RestaurantId {get; set;}
        public string Name {get; set;}
        public string Street {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Zip {get; set;}
        public string Country {get; set;}

        [JsonIgnore] 
        public virtual ICollection<Review> Reviews {get; set;}
    }
}