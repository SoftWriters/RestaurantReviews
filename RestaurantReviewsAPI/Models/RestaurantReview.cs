using System;

namespace RestaurantReviewsAPI.Models
{
    public class RestaurantReview
    {
        public Guid Id { get; set; }

        public DateTime DatePosted { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Review { get; set; }

        public string PostedByUser { get; set; }
        
        [Flags]
        public enum Rating {
            Terrible = 1, 
            Poor = 2, 
            Acceptable = 3, 
            Good = 4, 
            Great = 5
        }
    }
}
