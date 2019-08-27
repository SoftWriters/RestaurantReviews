namespace RestaurantReviews.API.Models
{
    public class Restaurant : IRestaurant
    {
        public long Id { get; set; }
        public string City { get; set; }
    }
}
