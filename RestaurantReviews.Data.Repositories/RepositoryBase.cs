using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RestaurantReviewsContext RepositoryContext { get; set; }

        public RepositoryBase() { }

        public RepositoryBase(RestaurantReviewsContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<T>> FindAll()
        {
            return await this.RepositoryContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await this.RepositoryContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task Create(T entity)
        {
            await this.RepositoryContext.Set<T>().AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public async Task Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public async Task Save()
        {
            await this.RepositoryContext.SaveChangesAsync();
        }
    }
}
