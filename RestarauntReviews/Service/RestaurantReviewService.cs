using RestarauntReviews.Service.Interface;
using RestaurantReviews.DAL;
using RestaurantReviews.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReviews.DAL;

namespace RestarauntReviews.Service
{
    public class RestaurantReviewService : IRestaurantReviewService
    {
        private RestaurantReviewDAL dal;

        public RestaurantReviewService()
        {
            dal = new RestaurantReviewDAL();
        }

        public IEnumerable<Restaurant> GetRestaraunts(string city)
        {
            return((IEnumerable<Restaurant>)dal.GetRestaurants(city));
        }

        public IEnumerable<Review> GetReviews(string username)
        {
            return ((IEnumerable<Review>)dal.GetReviews(username));
        }

        public void AddReview(Review review)
        {
            dal.AddReview(review);
        }

        public void AddRestaurant(Restaurant restaraunt)
        {
            dal.AddRestaurant(restaraunt);
        }
        
        public void DeleteReview(int ReviewId)
        {
            dal.DeleteReview(ReviewId);
        }
    }
}
