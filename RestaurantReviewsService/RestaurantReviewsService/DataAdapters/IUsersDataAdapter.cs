using RestaurantReviewsService.DomainModels;
using System.Collections.Generic;

namespace RestaurantReviewsService.DataAdapters
{
    public interface IUsersDataAdapter
    {
        IList<UserDM> GetAllUsers();

        UserDM GetUserByName(string firstName, string lastName);

        UserDM GetUserById(int userId);
    }
}
