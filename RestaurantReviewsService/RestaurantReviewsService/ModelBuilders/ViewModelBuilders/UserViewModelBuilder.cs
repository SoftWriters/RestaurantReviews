using RestaurantReviewsService.DomainModels;
using RestaurantReviewsService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviewsService.ModelBuilders.ViewModelBuilders
{
    public sealed class UserViewModelBuilder : IModelBuilder<UserViewModel, UserDM>
    {
       
        UserViewModel IModelBuilder<UserViewModel, UserDM>.Build(UserDM p)
        {
            return new UserViewModel
            {
                Id = p.Id,
                Alias = p.Alias,
                City = p.City,
                FirstName = p.FirstName,
                LastName = p.LastName,
                State = p.State,
                StateIdRef = p.StateIdRef,
                FullName = GetUserFullName(p.FirstName,p.LastName)
            };
        }


        private string GetUserFullName(string firstName, string lastName)
        {
            string buffer = string.Format("{0} {1}", firstName.Trim(), lastName.Trim());

            return buffer.Trim();
        }
    }
}