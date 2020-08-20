using RestaurantReviews.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Api.Model
{
    public class Review
    {
        public DateTime ReviewDate { get; set; }

        public Guid RestaurantId { get; set; }

        public Guid UserId { get; set; }

        public Rating Rating { get; set; }

        public string Comments { get; set; }

        public Guid ReviewId { get; set; }

        public Review() { }

        public Review(DateTime reviewDate, Guid restaurantId, Guid userId, Rating rating, string comments)
        {
            ReviewDate = reviewDate;
            RestaurantId = restaurantId;
            UserId = userId;
            Rating = rating;
            Comments = comments;
        }

        public string GetKey()
        {
            return RestaurantId + ":" + UserId + ":" + ReviewDate;
        }
    }
}
