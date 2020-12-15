using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Logic.Model.User.Search
{
    public class SearchUserRequest
    {
        public IEnumerable<string> UserIds { get; set; }
        public string Name { get; set; }
    }
}
