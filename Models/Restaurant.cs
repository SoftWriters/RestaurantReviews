namespace Softwriters.RestaurantReviews.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ReviewId { get; set; }

        public Review Review { get; set; }

        public int RestaurantTypeId { get; set; }

        public RestaurantType RestaurantType { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int MenuId { get; set; }

        public Menu Menu { get; set; }
    }
}
