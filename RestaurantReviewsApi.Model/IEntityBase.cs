using System;

namespace RestaurantReviews.Model
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}