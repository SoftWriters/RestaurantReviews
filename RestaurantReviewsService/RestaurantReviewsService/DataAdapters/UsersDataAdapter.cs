using RestaurantReviewService.Entities;
using RestaurantReviewService.Operations;
using RestaurantReviewsService.DomainModels;
using RestaurantReviewsService.ModelBuilders;
using RestaurantReviewsService.ModelBuilders.DataEntityModelBuilders;
using RestaurantReviewsService.ModelBuilders.DomainModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviewsService.DataAdapters
{
    public sealed class UsersDataAdapter : IUsersDataAdapter
    {    
        IList<UserDM> IUsersDataAdapter.GetAllUsers()
        {
            IModelBuilder<UserDM, User> modelBuilder = new UserDomainModelBuilder();
            SelectAllUsersOperation selectAllUsersOperation = new SelectAllUsersOperation();
            IList<UserDM> results = new List<UserDM>();

            IList<User> operationResults = selectAllUsersOperation.SelectAll();

            if (operationResults != null && operationResults.Count > 0)
            {
                foreach (User user in operationResults)
                {
                    results.Add(modelBuilder.Build(user));
                }
            }

            return results;
        }

        UserDM IUsersDataAdapter.GetUserById(int userId)
        {
            UserDM results = null;

            var filteredResults = ((IUsersDataAdapter)this).GetAllUsers().Where(x => x.Id == userId).ToList<UserDM>();

            if (filteredResults != null && filteredResults.Count > 0)
            {
                results = filteredResults.SingleOrDefault<UserDM>();
            }

            return results;
        }

        UserDM IUsersDataAdapter.GetUserByName(string firstName, string lastName)
        {
            UserDM results = null;

            var filteredResults = ((IUsersDataAdapter)this).GetAllUsers().Where(x => x.FirstName == firstName
            && x.LastName == lastName).ToList<UserDM>();

            if (filteredResults != null && filteredResults.Count > 0)
            {
                results = filteredResults.SingleOrDefault<UserDM>();
            }

            return results;
        }
    }
}