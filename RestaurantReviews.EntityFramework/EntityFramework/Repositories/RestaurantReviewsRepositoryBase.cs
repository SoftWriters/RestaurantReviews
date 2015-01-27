using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace RestaurantReviews.EntityFramework
{
    public abstract class RestaurantReviewsRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<RestaurantReviewsDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected RestaurantReviewsRepositoryBase(IDbContextProvider<RestaurantReviewsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class RestaurantReviewsRepositoryBase<TEntity> : RestaurantReviewsRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected RestaurantReviewsRepositoryBase(IDbContextProvider<RestaurantReviewsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
