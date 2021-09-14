
using System;
using System.Threading.Tasks;

namespace RestaurantReviews
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRestaurantRepository Restaurants { get; }
        IReviewRepository Reviews { get; }
        Task CompleteAsync();
    }
}
