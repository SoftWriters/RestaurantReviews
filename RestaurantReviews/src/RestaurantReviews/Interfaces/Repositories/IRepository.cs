namespace RestaurantReviews.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity value);
        void Delete(TEntity value);
        void Update(TEntity value);
        int Save();
    }
}