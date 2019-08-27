using System.Collections.Generic;

namespace RestaurantReviews.API.Repository
{
    public interface IDataSet<T>
    {
        ICollection<T> GetAll();
        void Save(ICollection<T> contents);
    }
}