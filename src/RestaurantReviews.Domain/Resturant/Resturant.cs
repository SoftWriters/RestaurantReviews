using System;

namespace RestaurantReviews.Domain
{
    public class Resturant
    {
        public Guid ResturantId { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public string FullAddress { get; set; }
        public Guid CreatedUserId { get; set; }
    }
}
