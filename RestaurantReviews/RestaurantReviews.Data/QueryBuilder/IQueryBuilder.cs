using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Data.QueryBuilder
{
    public interface IQueryBuilderSearch<TEntity, TRequest, TResponse>
    {
        /// <summary>
        /// Builds a query that will find the existing entity or entities
        /// </summary>
        IQueryable<TEntity> BuildSearchQuery(IQueryable<TEntity> dbSet, TRequest request);
        TResponse BuildSearchEntity(TEntity entity);
    }

    public interface IQueryBuilderUpsert<TEntity, TRequest>
    {
        TEntity BuildUpsertEntity(TRequest request);
        IQueryable<TEntity> BuildUpsertQuery(IQueryable<TEntity> dbSet, TRequest request);
    }
}
