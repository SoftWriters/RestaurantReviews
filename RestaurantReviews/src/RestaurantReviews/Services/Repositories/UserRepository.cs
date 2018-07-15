using System;
using System.Collections.Generic;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Models;
using RestaurantReviews.Repositories.Interfaces;
using System.Linq;

namespace RestaurantReviews.Services.Repositories
{
    /// <summary>
    /// User repository
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(RestaurantReviewContext context) : base(context)
        {
        }

        public IEnumerable<User> GetUsers()
        {
            return context.User.ToList();
        }

        public User GetUser(long userId)
        {
            return context.User.Where(user => user.UserId == userId).FirstOrDefault();
        }
    }
}