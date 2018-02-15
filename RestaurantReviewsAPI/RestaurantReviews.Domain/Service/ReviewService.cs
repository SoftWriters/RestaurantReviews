using RestaurantReviews.Data.Framework.UnitOfWorkContracts;
using RestaurantReviews.Domain.Codes;
using RestaurantReviews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain.Service
{
    public class ReviewService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly long _activeUserId;

        public ReviewService(IUnitOfWorkFactory unitOfWorkFactory, long activeUserId)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _activeUserId = activeUserId;
        }

        public async Task<OperationResponse> DeleteReview(long reviewId)
        {
            var unitOfWork = _unitOfWorkFactory
                .Get();

            var review = unitOfWork
                .ReviewRepo
                .Get(reviewId);

            if (review == null)
                return new OperationResponse { OpCode = OpCodes.ResourceNotFound };

            var activeUser = unitOfWork
                .UserRepo
                .Get(_activeUserId);

            if (activeUser == null)
                return new OperationResponse { OpCode = OpCodes.UnauthorizedOperation };

            if (!review.AuthorUsername.Equals(activeUser.Username))
                return new OperationResponse { OpCode = OpCodes.InsufficientPermissions };

            unitOfWork
                .ReviewRepo
                .Remove(review.Id);

            await unitOfWork
                .CommitAsync();

            return new OperationResponse { OpCode = OpCodes.Success };
        }
    }
}
