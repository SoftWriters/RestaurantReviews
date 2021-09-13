namespace Softwriters.RestaurantReviews.Dto
{
    public class RestaurantRequest
    {
        public int CityId { get; set; }

        public int MenuId { get; set; }

        public string Name { get; set; }

        public int RestaurantTypeId { get; set; }
    }
}
