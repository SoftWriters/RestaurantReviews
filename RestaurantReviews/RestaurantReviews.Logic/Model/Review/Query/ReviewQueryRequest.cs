using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Logic.Model.Review.Query
{
    public class ReviewQueryRequest : IQueryBuilder<Data.Review>
    {
        public IEnumerable<string> UserIds { get; set; }        

        public IQueryable<Data.Review> BuildQuery(IQueryable<Data.Review> query)
        {
            query = query.Include(p => p.User);

            if (UserIds != null)
            {
                query = query.Where(p => UserIds.Contains(p.UserId.ToString()));
            }
            
            return query
                .OrderByDescending(p => p.DateCreated);
        }
    }
}
