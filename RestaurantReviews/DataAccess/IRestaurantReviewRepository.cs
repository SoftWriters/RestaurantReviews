using RestaurantReviews.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.DataAccess
{
    public interface IRestaurantReviewRepository
    {
        IEnumerable<Restaurant> GetRestaurantsByCity(NonEmptyString city);

        void AddRestaurant(Restaurant restaurant);

        void AddReview(Review review);

        IEnumerable<Review> GetReviewsByUser(User user);

        Restaurant GetRestaurant(Id id);

        User GetUser(Id id);

        void DeleteReview(Review review);
    }
}
