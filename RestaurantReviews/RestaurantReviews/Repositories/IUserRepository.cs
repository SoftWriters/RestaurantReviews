using RestaurantReviews.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task<IEnumerable<Review>> GetUserReviews(int id);
    }
}
