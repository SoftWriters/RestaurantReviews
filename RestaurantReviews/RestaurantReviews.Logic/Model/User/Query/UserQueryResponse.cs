using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Logic.Model.User.Query
{
    public class UserQueryResponse
    {
        public IEnumerable<UserQueryResponseUser> Users { get; set; }
    }

    public class UserQueryResponseUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
