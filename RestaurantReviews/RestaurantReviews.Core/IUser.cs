using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Core
{
    public interface IUser
    {
        Guid UniqueId { get; }

        string DisplayName { get; }
    }
}
