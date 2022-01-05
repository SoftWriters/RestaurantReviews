using System.Collections.Generic;

namespace RestaurantReviews.Interfaces.Repositories
{
    public interface IDataSet<T>
    {
        ICollection<T> GetAll();
        void Save(ICollection<T> contents);
    }
}