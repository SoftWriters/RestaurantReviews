using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using SoftWriters.RestaurantReviews.DataLibrary;
using SoftWriters.RestaurantReviews.WebApi;

namespace ClientConsole
{
    public class ReviewClient : ClientBase<IReviewApi>, IReviewApi
    {
        public ReviewClient()
        { }

        public ReviewClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        { }

        public IEnumerable<Restaurant> GetRestaurants(string street, string city, string stateCode, string postalCode, string country)
        {
            return Channel.GetRestaurants(street, city, stateCode, postalCode, country);
        }

        public IEnumerable<Review> GetReviews(Guid userId, Guid restaurantId)
        {
            return Channel.GetReviews(userId, restaurantId);
        }

        public bool AddRestaurant(string name, string street, string city, string stateCode, string postalCode, string country)
        {
            return Channel.AddRestaurant(name, street, city, stateCode, postalCode, country);
        }

        public bool AddReview(Guid userId, Guid restaurantId, int overallRating, int foodRating, int serviceRating, int costRating,
            string comments)
        {
            return Channel.AddReview(userId, restaurantId, overallRating, foodRating, serviceRating, costRating,
                comments);
        }

        public bool DeleteReview(Guid id)
        {
            return Channel.DeleteReview(id);
        }
    }
}
