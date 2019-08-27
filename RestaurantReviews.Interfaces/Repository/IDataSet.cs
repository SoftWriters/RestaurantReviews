using System.Collections.Generic;

namespace RestaurantReviews.Interfaces.Repository
{
    public interface IDataSet<T>
    {
        ICollection<T> GetAll();
        void Save(ICollection<T> contents);
    }
}