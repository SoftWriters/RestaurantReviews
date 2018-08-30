using System.Collections.Generic;

namespace Restaurant_Review.Data.Repository
{
    interface IRepository<T> where T: class
    {
        List<T> GetAll();
        int Add(T employee);
        T GetById(int id);
        bool Update(T employee);
        bool Delete(int id);
    }
}
