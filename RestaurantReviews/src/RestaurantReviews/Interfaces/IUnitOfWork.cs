using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReviews.Repositories.Interfaces;

namespace RestaurantReviews.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRestaurantRepository Restaurants { get; }
        IReviewRepository Reviews { get; }
        IUserRepository Users { get; }
        int Save();
    }
}