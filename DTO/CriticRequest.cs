using Softwriters.RestaurantReviews.PrivateModels;

namespace Softwriters.RestaurantReviews.Dto
{
    public class CriticRequest : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
