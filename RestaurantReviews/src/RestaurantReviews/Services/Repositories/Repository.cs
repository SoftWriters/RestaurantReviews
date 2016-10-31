using Microsoft.EntityFrameworkCore;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Services.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly RestaurantReviewContext context;

        public Repository(RestaurantReviewContext context)
        {
            this.context = context;
        }

        public virtual void Add(TEntity value)
        {
            context.Set<TEntity>().Add(value);
        }
        
        public virtual void Delete(TEntity value)
        {
            context.Set<TEntity>().Remove(value);
        }

        public void Update(TEntity value)
        {
            context.Set<TEntity>().Update(value);
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}