using System;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsers();
        User GetUser(long userId);
    }
}