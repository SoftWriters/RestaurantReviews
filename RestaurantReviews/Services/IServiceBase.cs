using System;
using System.Collections.Generic;

namespace RestaurantReviews.Data.Services
{
    public interface IServiceBase<T>
    {
        T GetByID(Guid id);
        List<T> GetAll();
        void Delete(Guid id);
        void Save(T t);
    }
}