using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReviews.Data.Repos
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private readonly DbContext _dataContext;
        private DbSet<T> _dbSet;

        protected DbContext DbContext()
        {
            return _dataContext; //?? (_dataContext = DbFactory.Init);
        }

            #endregion

        protected RepositoryBase(DbContext context)
        {
            _dataContext = context;
            _dbSet = _dataContext.Set<T>();
        }

        #region Implementation
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        public virtual T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual void Save()
        {
            _dataContext.SaveChanges();
        }

        #endregion

    }

}