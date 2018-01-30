using RestaurantReview.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.BusinessLogic.Models
{
    public class Restaurant : RestaurantContext
    {
        public Restaurant(RestaurantContext restaurantContext)
        {
            id = restaurantContext.id;
            name = restaurantContext.name;
            streetAddress = restaurantContext.streetAddress;
            zipcode = restaurantContext.zipcode;
            city = restaurantContext.city;
            state = restaurantContext.state;
            country = restaurantContext.country;
            thumbnailBase64 = restaurantContext.thumbnailBase64;
        }

        public decimal rating { get { return GetRestauranteRatingAverage(id); } }

        public decimal GetRestauranteRatingAverage(int restaurantID)
        {
            RestaurantDBContext restaurantDBContext = new RestaurantDBContext();
            ReviewDBContext reviewDBContext = new ReviewDBContext();

            List<ReviewContext> reviews = reviewDBContext.Reviews.Where(x => x.restaurantID == restaurantID).ToList();

            int reviewCount = reviews.Count;

            if (reviewCount <= 0)
                return 0;

            decimal totalRating = 0;

            foreach (ReviewContext review in reviews)
            {
                totalRating += review.rating;
            }

            return totalRating / reviews.Count;
        }
    }
}
