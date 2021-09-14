using Softwriters.RestaurantReviews.PrivateModels;

namespace Softwriters.RestaurantReviews.Dto
{
    public class MenuRequest : EntityBase
    {
        public string Name { get; set; }
        public string Items { get; set; }
    }
}
