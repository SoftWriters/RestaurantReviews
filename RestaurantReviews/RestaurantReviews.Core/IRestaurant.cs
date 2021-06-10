using System;

namespace RestaurantReviews.Core
{
    public interface IRestaurant
    {
        Guid UniqueId { get; }

        string Name { get; }

        string Description { get; }

        IAddress Address { get; }
    }
}
