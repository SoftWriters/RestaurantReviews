using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.ExtendedModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Contracts.Repositories.Entities
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<IEnumerable<User>> GetUsersByCondition(Expression<Func<User, bool>> expression);

        Task<User> GetUserById(Guid userId);

        Task<UserExtended> GetUserWithDetails(Guid userId);

        Task CreateUser(User user);

        Task UpdateUser(User dbUser, User user);

        Task DeleteUser(User user);
    }
}
