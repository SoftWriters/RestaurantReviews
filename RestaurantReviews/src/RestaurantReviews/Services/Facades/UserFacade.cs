using RestaurantReviews.DataAccess;
using RestaurantReviews.Interfaces;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Services
{
    public class UserFacade
    {
        internal void AddUser(User user)
        {
            using (var context = new RestaurantReviewContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                unitOfWork.Users.Add(user);
                unitOfWork.Save();
            }
        }
    }
}
