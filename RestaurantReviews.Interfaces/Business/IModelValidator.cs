using System.Collections.Generic;

namespace RestaurantReviews.Interfaces.Business
{
    public interface IModelValidator <T>
    {
        ICollection<string> Validate(T model);
    }
}
