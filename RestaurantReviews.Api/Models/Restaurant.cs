
namespace RestaurantReviews.Api.Models
{
    public class Restaurant
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set;}

        public string City { get; set; }

        public string State { get; set; }
    }
}