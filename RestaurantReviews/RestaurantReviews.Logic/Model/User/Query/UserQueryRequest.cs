using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Logic.Model.User.Query
{
    public class UserQueryRequest
    {
        public IEnumerable<string> UserIds { get; set; }
    }
}
