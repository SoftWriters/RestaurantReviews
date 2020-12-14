using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Logic.Model.Restaurant.Query
{
    public class RestaurantQueryRequest : IQueryBuilder<Data.Restaurant>
    {
        public string State { get; set; }
        public IEnumerable<string> Cities { get; set; }

        public IQueryable<Data.Restaurant> BuildQuery(IQueryable<Data.Restaurant> query)
        {
            if (State != null)
            {
                query = query.Where(p => p.State.ToLower() == State.ToLower());
            }

            if (Cities != null)
            {
                query = query.Where(p => Cities.Select(p => p.ToLower()).Contains(p.City.ToLower()));
            }

            return query;
        }
    }
}
