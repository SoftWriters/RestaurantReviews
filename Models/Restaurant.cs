using System.Collections.Generic;

namespace RestaurantReviews.Models
{
    public class Restaurant
    {
        public long RestaurantID {get; set;}
        public string Name {get; set;}
        public string Street {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Zip {get; set;}
        public string Country {get; set;}
        public virtual ICollection<Review> Reviews {get; set;}
    }
}