using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace RestaurantReviewService.Abstract
{
    public interface IDbRepository<T1, T2> where T1 : class
    {

        IEnumerable<T1> SelectAll();

        T1 SelectById(T2 id);

        void Save(T1 entity);

        T2 Insert(T1 entity);

        void Delete(T1 entity);
    }
}
