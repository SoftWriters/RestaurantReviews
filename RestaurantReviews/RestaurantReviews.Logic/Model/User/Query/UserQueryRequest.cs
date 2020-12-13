using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Logic.Model.User.Query
{
    public class UserQueryRequest : IQueryBuilder<Data.User>
    {
        public IEnumerable<string> UserIds { get; set; }
        public string Name { get; set; }

        public IQueryable<Data.User> BuildQuery(IQueryable<Data.User> query)
        {
            if (UserIds != null)
            {
                query = query.Where(p => UserIds.Contains(p.Id.ToString()));
            }

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(p => p.First.Contains(Name) || p.Last.Contains(Name));
            }

            return query;
        }
    }
}
