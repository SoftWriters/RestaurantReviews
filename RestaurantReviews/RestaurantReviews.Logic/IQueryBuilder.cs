using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Logic
{
    public interface IQueryBuilder<T>
    {
        IQueryable<T> BuildQuery(IQueryable<T> query);
    }
}
