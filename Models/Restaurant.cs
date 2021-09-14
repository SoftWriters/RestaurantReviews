
namespace RestaurantReviews
{
    public class RestaurantDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
    }

    public class Restaurant : RestaurantDTO
    {
        public long RestaurantID { get; set; }
    }
}
