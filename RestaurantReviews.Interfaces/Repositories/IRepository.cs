using System.Collections.Generic;

namespace RestaurantReviews.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        ICollection<T> GetAll();
        T GetById(long id);
        long Create(T item);
    }
}
