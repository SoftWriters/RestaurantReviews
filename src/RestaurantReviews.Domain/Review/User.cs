using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Domain
{
    public class User
    {
        public Guid UserId;
        public bool IsAdmin;
        public string FullName;
    }
}
