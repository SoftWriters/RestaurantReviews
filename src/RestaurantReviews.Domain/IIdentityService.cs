using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Domain
{
    public interface IIdentityService
    {
        Guid GetUserIdentity();
    }
}
