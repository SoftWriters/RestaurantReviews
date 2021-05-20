using RestaurantReviews.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntReviews.Service.Interface
{
    public interface IRestaurantReviewService
    {
        public IEnumerable<Restaurant> GetRestaraunts(string city);
        public IEnumerable<Review> GetReviews(string username);
        public int AddReview(Review review);
        public int DeleteReview(int ReviewId);
        public int AddRestaurant(Restaurant restaraunt);
    }
}
