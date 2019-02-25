using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data.Contracts.Repositories.Entities;
using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.ExtendedModels;
using RestaurantReviews.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Repositories.Entities
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RestaurantReviewsContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await this.RepositoryContext.Set<User>().OrderBy(ow => ow.EmailAddress).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByCondition(Expression<Func<User, bool>> expression)
        {
            return await this.RepositoryContext.Set<User>().Where(expression).ToListAsync();
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return this.RepositoryContext.Set<User>().Where(user => user.Id.Equals(userId))
                    .DefaultIfEmpty(new User())
                    .FirstOrDefault();
        }

        public async Task<UserExtended> GetUserWithDetails(Guid userId)
        {
            return new UserExtended(await GetUserById(userId))
            {
                Reviews = RepositoryContext.Reviews.Where(a => a.UserId == userId)
            };
        }

        public async Task CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            await Create(user);
            await Save();
        }

        public async Task UpdateUser(User dbUser, User user)
        {
            dbUser.Map(user);
            await Update(dbUser);
            await Save();
        }

        public async Task DeleteUser(User user)
        {
            await Delete(user);
            await Save();
        }
    }
}
