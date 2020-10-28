using RestaurantReviews.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.DataAccess
{
    public interface IRestaurantReviewRepository
    {
        IEnumerable<Restaurant> GetRestaurantsByCity(string city);
        Task<IEnumerable<Restaurant>> GetRestaurantsByCityAsync(string city);

        void AddRestaurant(Restaurant restaurant);
        Task AddRestaurantAsync(Restaurant restaurant);

        void AddReview(Review review);
        Task AddReviewAsync(Review review);

        IEnumerable<Review> GetReviewsByUser(User user);
        Task<IEnumerable<Review>> GetReviewsByUsersAsync(User user);

        void DeleteReview(Review review);
        Task DeleteReviewAsync(Review review);

        void AddUser(User user);
        Task AddUserAsync(User user);
    }
}
