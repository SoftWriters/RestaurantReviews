using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Framework.UnitOfWorkContracts;

namespace RestaurantReviews.Domain.Service
{
    public class UserAuthenticationService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public UserAuthenticationService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            var unitOfWork = _unitOfWorkFactory.Get();

            var usersQueryResults = await unitOfWork
                .UserRepo
                .Query(username);

            if (!usersQueryResults.Any())
                return false;

            return usersQueryResults
                .Any(user => user.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && user.ValidatePassword(password));
        }
    }
}
