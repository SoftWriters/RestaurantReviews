using RestarauntReviews.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntReviews.Service.Interface
{
    public interface IRestaurantReviewService
    {
        public IEnumerable<Restaraunt> GetRestaraunts(string city);
        public IEnumerable<Review> GetReviews(string username);
        public void AddReview(Review review);
        public void DeleteReview(int ReviewId);
        public void AddRestaurant(Restaraunt restaraunt);
    }
}
