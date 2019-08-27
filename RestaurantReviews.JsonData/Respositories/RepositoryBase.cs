using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Interfaces.Models;

namespace RestaurantReviews.JsonData.Repositories
{
    public abstract class RepositoryBase<T> where T : IModel
    {
        internal readonly Context context;
        private readonly DataSet<T> dataSet;

        internal RepositoryBase(Context context)
        {
            this.context = context;
            dataSet = GetDataSet();
        }

        internal abstract DataSet<T> GetDataSet();
        public abstract void Update(long id, T t);

        public ICollection<T> GetAll()
        {
            return dataSet.GetAll();
        }

        public T GetById(long id)
        {
            return dataSet.GetAll().FirstOrDefault(x => x.Id == id);
        }

        public long Create(T q)
        {
            var contents = dataSet.GetAll();
            q.Id = contents.Any() ? contents.Select(x => x.Id).Max() + 1 : 0;
            contents.Add(q);
            dataSet.Save(contents);
            return q.Id;
        }

        public void Delete(long id)
        {
            var contents = dataSet.GetAll().Where(x => x.Id != id);
            dataSet.Save(contents.ToArray());
        }
    }
}