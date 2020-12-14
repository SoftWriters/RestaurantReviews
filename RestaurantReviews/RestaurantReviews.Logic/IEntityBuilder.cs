using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Logic
{
    public interface IEntityBuilder<T>
    {
        T Build();
    }
}
