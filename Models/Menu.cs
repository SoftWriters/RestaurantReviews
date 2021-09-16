using Softwriters.RestaurantReviews.PrivateModels;

namespace Softwriters.RestaurantReviews.Models
{
    public class Menu : EntityBase
    {
        public string Name { get; set; }
        public string Items { get; set; }
    }
}
