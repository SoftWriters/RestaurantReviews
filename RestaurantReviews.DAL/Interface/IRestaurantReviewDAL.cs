using RestarauntReviews.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.DAL.Interface
{
    interface IRestaurantReviewDAL
    {
        public IEnumerable<Restaraunt> GetRestaurants(string city);
        public IEnumerable<Restaraunt> GetRestaurants();
    }
}
