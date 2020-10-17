using RestaurantReviewsService.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsService.DataAdapters
{
    public interface IUserReviewsDataAdapter
    {
        IList<UserReviewDM> GetUserReviewsForUser(UserDM userDm);

        IList<UserReviewDM> GetUserReviewsForRestaurant(RestaurantDM restaurantDm);

        int AddNewUserReview(UserReviewDM userReviewDM);

        int DeleteExistingUserReview(UserReviewDM userReviewDM);
    }
}
