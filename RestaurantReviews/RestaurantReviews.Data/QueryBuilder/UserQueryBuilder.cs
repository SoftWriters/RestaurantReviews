using RestaurantReviews.Logic.Model.User.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Data.QueryBuilder
{
    public interface IUserQueryBuilder : IQueryBuilderSearch<User, SearchUserRequest, SearchUser>
    { }

    public class UserQueryBuilder : IUserQueryBuilder
    {
        public SearchUser BuildSearchEntity(User entity)
        {
            return new SearchUser
            {
                Id = entity.Id.ToString(),
                Name = entity.ToString()
            };
        }

        public IQueryable<User> BuildSearchQuery(IQueryable<User> dbSet, SearchUserRequest request)
        {
            IQueryable<User> query = dbSet.OrderByDescending(p => p.DateCreated);

            if (request?.UserIds != null)
            {
                query = query.Where(p => request.UserIds.Contains(p.Id.ToString()));
            }

            if (request?.Name != null)
            {
                query = query.Where(p => p.First.ToLower().Contains(request.Name.ToLower()) || p.Last.ToLower().Contains(request.Name.ToLower()));
            }

            return query;
        }
    }
}
