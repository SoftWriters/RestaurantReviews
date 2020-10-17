using RestaurantReviewService.Abstract;
using RestaurantReviewService.Concrete;
using RestaurantReviewService.Entities;
using RestaurantReviewService.Operations;
using System;

namespace RestaurantReviewServiceRepository.Operations
{
    public interface IAddNewUserReviewOperation : IRepositoryOperation<int, NewUserReviewAddRepositoryOperationParams> { }

    public struct NewUserReviewAddRepositoryOperationParams
    {
        public UserReviewsRepository<object> UserReviewsRepository { get; set; }

        public UserReview UserReviewAddCandidate { get; set; }
    }

    public sealed class AddNewUserReviewOperation : IAddNewUserReviewOperation
    {
        public int AddNewUserReview(UserReview userReview)
        {
            try
            {
              
                return ((IAddNewUserReviewOperation)this).Execute(new NewUserReviewAddRepositoryOperationParams
                {
                     UserReviewAddCandidate = userReview,
                     UserReviewsRepository = new UserReviewsRepository<object>()
                });
              
            }
            catch (Exception)
            {
                throw;
            }
        }
        int IRepositoryOperation<int, NewUserReviewAddRepositoryOperationParams>.Execute(NewUserReviewAddRepositoryOperationParams p)
        {
            object result = ((ISqlLiteDbRepository)p.UserReviewsRepository).Insert(p.UserReviewAddCandidate);

            if (int.TryParse(result.ToString(), out int operationResult))
            {
                return operationResult;
            }
            else
            {
                throw new Exception("AddNewUserReviewOperation::Execute received an unexpected result");
            }
        }
    }
}
