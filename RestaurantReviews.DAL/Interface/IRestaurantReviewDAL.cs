using RestaurantReviews.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.DAL.Interface
{
    interface IRestaurantReviewDAL
    {
        void AddReview(Restaurant restaurant);
        public IEnumerable<Restaurant> GetRestaurants(string city);
        public IEnumerable<Restaurant> GetRestaurants();
    }
}
