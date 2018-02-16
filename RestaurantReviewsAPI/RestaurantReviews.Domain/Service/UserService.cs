using RestaurantReviews.Data.Framework.UnitOfWorkContracts;
using RestaurantReviews.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain.Service
{
    public class UserService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly long _activeUserId;

        public UserService(IUnitOfWorkFactory unitOfWorkFactory, long activeUserId)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _activeUserId = activeUserId;
        }

        public async Task<List<Review>> GetReviewsBy(long userId)
        {
            var unitOfWork = _unitOfWorkFactory
                .Get();

            if (userId < 0)
                return new List<Review>();

            var results = await unitOfWork
                .ReviewRepo
                .FindMatchingResults(authorId: userId);

            return results;
        }
    }
}
