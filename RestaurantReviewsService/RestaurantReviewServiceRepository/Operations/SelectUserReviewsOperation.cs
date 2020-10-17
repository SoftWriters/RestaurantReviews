using RestaurantReviewService.Abstract;
using RestaurantReviewService.Concrete;
using RestaurantReviewService.Entities;
using System.Collections.Generic;

namespace RestaurantReviewService.Operations
{
    public interface ISelectUserReviewsOperation
    {
        IList<UserReview> SelectByUser(User user);

        IList<UserReview> SelectByRestaurant(Restaurant restuarant);
    }

    public sealed class SelectUserReviewsOperation : ISelectUserReviewsOperation
    {
        public IList<UserReview> SelectByRestaurant(Restaurant restaurant)
        {
            ISqlLiteDbRepository usersReviewRepository = new UserReviewsRepository<Restaurant>(
                new UserReviewsFilterParams<Restaurant>
                {
                    Criteria = restaurant,
                    FilterType = UserReviewsRepository<Restaurant>.SelectAllFilterType.ByRestaurantId
                });


            return (IList<UserReview>)usersReviewRepository.SelectAll();
        }

        public IList<UserReview> SelectByUser(User user)
        {
            ISqlLiteDbRepository usersReviewRepository = new UserReviewsRepository<User>(
                new UserReviewsFilterParams<User>
                {
                     Criteria = user,
                     FilterType = UserReviewsRepository<User>.SelectAllFilterType.ByUserId
                });


            return (IList<UserReview>)usersReviewRepository.SelectAll();
   
        }
      
    }
}
