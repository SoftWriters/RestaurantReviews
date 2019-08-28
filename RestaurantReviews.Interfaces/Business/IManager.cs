using RestaurantReviews.Interfaces.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Interfaces.Business
{
    public interface IManager<T> where T: IModel
    {
        ICollection<T> GetAll();
        T GetById(long id);
        void Create(T item);
    }
}
