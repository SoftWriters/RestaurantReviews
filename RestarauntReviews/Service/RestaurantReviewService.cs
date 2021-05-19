using RestarauntReviews.DTO;
using RestarauntReviews.Service.Interface;
using RestaurantReviews.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntReviews.Service
{
    public class RestaurantReviewService : IRestaurantReviewService
    {
        private RestaurantReviewDAL dal;

        public RestaurantReviewService()
        {
            dal = new RestaurantReviewDAL();
        }

        public IEnumerable<Restaraunt> GetRestaraunts(string city)
        {
            return((IEnumerable<Restaraunt>)dal.GetRestaurants(city));
        }
    }
}
