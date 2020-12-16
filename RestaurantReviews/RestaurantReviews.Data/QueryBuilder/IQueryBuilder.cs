using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Data.QueryBuilder
{
    /// <summary>
    /// Builds a query that will potentially return multiple results
    /// </summary>
    public interface IQueryBuilderSearch<TEntity, TRequest, TResponse>
    {
        /// <summary>
        /// Builds a query that will find the existing entities
        /// </summary>
        IQueryable<TEntity> BuildSearchQuery(IQueryable<TEntity> dbSet, TRequest request);
        TResponse BuildSearchResponse(TEntity entity);
    }

    /// <summary>
    /// Builds a query that will return zero or one result
    /// </summary>
    public interface IQueryBuilderSingle<TEntity, TRequest>
    {
        /// <summary>
        /// Builds a query that will find a single entity
        /// </summary>
        IQueryable<TEntity> BuildQuerySingle(IQueryable<TEntity> dbSet, TRequest request);
    }

    /// <summary>
    /// Builds a query that can be used for Insert or Update operations on a single entity
    /// </summary>
    public interface IQueryBuilderUpsert<TEntity, TRequest> : IQueryBuilderSingle<TEntity, TRequest>
    {
        TEntity BuildEntityUpsert(TRequest request);
    }
}
