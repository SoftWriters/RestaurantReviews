﻿namespace Softwriters.RestaurantReviews.PrivateModels
{
    public interface IEntityBase
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
    }
}
