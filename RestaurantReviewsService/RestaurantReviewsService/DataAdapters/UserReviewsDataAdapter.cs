using RestaurantReviewService.Entities;
using RestaurantReviewService.Operations;
using RestaurantReviewServiceRepository.Operations;
using RestaurantReviewsService.DomainModels;
using RestaurantReviewsService.ModelBuilders;
using RestaurantReviewsService.ModelBuilders.DataEntityModelBuilders;
using RestaurantReviewsService.ModelBuilders.DomainModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviewsService.DataAdapters
{
    public sealed class UserReviewsDataAdapter : IUserReviewsDataAdapter
    {
        int IUserReviewsDataAdapter.AddNewUserReview(UserReviewDM userReviewDM)
        {
            IModelBuilder<UserReview, UserReviewDM> domainToEntityModelBuilder = new UserReviewDomainEntityModelBuilder();

            AddNewUserReviewOperation addNewUserReviewOperation = new AddNewUserReviewOperation();

            UserReview userReviewDataEntity = domainToEntityModelBuilder.Build(userReviewDM);

            return addNewUserReviewOperation.AddNewUserReview(userReviewDataEntity);            
        }

        int IUserReviewsDataAdapter.DeleteExistingUserReview(UserReviewDM userReviewDM)
        {
            throw new NotImplementedException();
        }

        IList<UserReviewDM> IUserReviewsDataAdapter.GetUserReviewsForRestaurant(RestaurantDM restaurantDm)
        {
            ISelectUserReviewsOperation selectUserReviewsOperation = new SelectUserReviewsOperation();

            IList<UserReview> operationResults = selectUserReviewsOperation.SelectByRestaurant(
                new Restaurant() { Id = restaurantDm.Id });

            return BuildUserReviewDomainModelList(operationResults);
        }

        IList<UserReviewDM> IUserReviewsDataAdapter.GetUserReviewsForUser(UserDM userDm)
        {            
            ISelectUserReviewsOperation selectUserReviewsOperation = new SelectUserReviewsOperation();            

            IList<UserReview> operationResults = selectUserReviewsOperation.SelectByUser(
                new User() { Id = userDm.Id });

            return BuildUserReviewDomainModelList(operationResults);           
        }


        private IList<UserReviewDM> BuildUserReviewDomainModelList(IList<UserReview> operationResults)
        {
            IList<UserReviewDM> results = new List<UserReviewDM>();
            IModelBuilder<UserReviewDM, UserReview> modelBuilder = new UserReviewDomainModelBuilder();

            if (operationResults != null && operationResults.Count > 0)
            {
                foreach (UserReview userReview in operationResults)
                {
                    results.Add(modelBuilder.Build(userReview));
                }
            }

            return results;
        }
    }
}