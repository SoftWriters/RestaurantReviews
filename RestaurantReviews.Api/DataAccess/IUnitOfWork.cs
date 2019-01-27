using System.Data;

namespace RestaurantReviews.Api.DataAccess
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
    }
}