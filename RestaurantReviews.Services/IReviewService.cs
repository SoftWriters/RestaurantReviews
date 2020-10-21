using RestaurantReviews.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviews();
        Task<IEnumerable<Review>> GetAllByUserId(string userId);
        Task<IEnumerable<Review>> GetAllByRestaurant(int restaurantId);
        Task<Review> GetReview(int id);
        void UpdateReview(int id, Review review);
        void CreateReview(Review review);
        void DeleteReview(Review review);
        bool ReviewExists(int id);
        Task<int> SaveChanges();
    }
}
