using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Framework.UnitOfWorkContracts;
using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Domain.Service
{
    public class UserAuthenticationService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public UserAuthenticationService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            var unitOfWork = _unitOfWorkFactory.Get();

            var usersQueryResults = await unitOfWork
                .UserRepo
                .Query(username);

            var exactMatches =  usersQueryResults
                .Where(user => user.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && user.ValidatePassword(password))
                .ToList();

            if (!exactMatches.Any())
                return null;

            if(exactMatches.Count() > 1)
                throw new Exception($"Encountered username {username} with duplicate accounts.");

            return exactMatches
                .First();
        }
    }
}
