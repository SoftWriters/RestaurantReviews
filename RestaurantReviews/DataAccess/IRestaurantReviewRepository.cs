using RestaurantReviews.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.DataAccess
{
    public interface IRestaurantReviewRepository
    {
        IEnumerable<Restaurant> GetRestaurantsByCity(string city);

        void AddRestaurant(Restaurant restaurant);

        void AddReview(Review review);

        IEnumerable<Review> GetReviewsByUser(User user);

        void DeleteReview(Review review);

        void AddUser(User user);
    }
}
