using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using SoftWriters.RestaurantReviews.DataLibrary;

namespace SoftWriters.RestaurantReviews.WebApi
{
    public class ReviewApi : IReviewApi
    {
        private readonly IDataStore<Restaurant> _restaurantDataStore;
        private readonly IDataStore<Review> _reviewDataStore;
        private readonly IDataStore<User> _userDataStore;

        public ReviewApi()
        {
            _restaurantDataStore = new XmlDataStore<Restaurant>();
            _reviewDataStore = new XmlDataStore<Review>();
            _userDataStore = new XmlDataStore<User>();
        }

        public ReviewApi(IDataStore<Restaurant> restaurantDataStore, IDataStore<Review> reviewDataStore, IDataStore<User> userDataStore)
        {
            _restaurantDataStore = restaurantDataStore;
            _reviewDataStore = reviewDataStore;
            _userDataStore = userDataStore;

        }

        public IEnumerable<Restaurant> GetRestaurants(string street, string city, string stateCode, string postalCode, string country)
        {
            var matches = _restaurantDataStore.GetItems(item =>
                (string.IsNullOrEmpty(street) || item.Address.StreetAddress == street) &&
                (string.IsNullOrEmpty(city) || item.Address.City == city) &&
                (string.IsNullOrEmpty(stateCode) || item.Address.StateCode == stateCode) &&
                (string.IsNullOrEmpty(postalCode) || item.Address.PostalCode == postalCode) &&
                (string.IsNullOrEmpty(country) || item.Address.Country == country));

            return matches;
        }

        public IEnumerable<Review> GetReviews(Guid userId, Guid restaurantId)
        {
            var matches = _reviewDataStore.GetItems(item =>
                (userId == Guid.Empty || item.UserId == userId) &&
                (restaurantId == Guid.Empty || item.RestaurantId == restaurantId));

            return matches;
        }
        
        public bool AddRestaurant(string name, string street, string city, string stateCode, string postalCode, string country)
        {
            var matches = _restaurantDataStore.GetItems(item => item.Name == name
                                                                && item.Address.StreetAddress == street
                                                                && item.Address.City == city
                                                                && item.Address.StateCode == stateCode
                                                                && item.Address.PostalCode == postalCode
                                                                && item.Address.Country == country);

            if (matches.Any())
                return false;

            var address = new Address(street, postalCode, city, stateCode, country);
            var id = Guid.NewGuid();
            var restaurant = new Restaurant(id, name, address);
            _restaurantDataStore.AddItem(restaurant);

            return true;
        }

        public bool AddReview(Guid userId, Guid restaurantId, int overallRating, int foodRating, int serviceRating,
            int costRating, string comments)
        {
            var matchingReviews = _reviewDataStore.GetItems(item => item.UserId == userId && item.RestaurantId == restaurantId);
            if (matchingReviews.Any())
                return false;

            var matchingUser = _userDataStore.GetItems(item => item.Id == userId).SingleOrDefault();
            if (matchingUser == null)
                return false;

            var id = Guid.NewGuid();
            var review = new Review(id, userId, restaurantId, overallRating, foodRating, serviceRating, costRating, comments);
            _reviewDataStore.AddItem(review);
            return true;
        }

        public bool DeleteReview(Guid id)
        {
            var review = _reviewDataStore.GetItems(item => item.Id == id).SingleOrDefault();
            if(review != null)
                _reviewDataStore.DeleteItem(review);

            return true;
        }
    }

    [ServiceContract]
    public interface IReviewApi
    {
        [WebInvoke(UriTemplate = "/GetRestaurants?street={street}&city={city}&stateCode={stateCode}&postalCode={postalCode}&country={country}", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        IEnumerable<Restaurant> GetRestaurants(string street, string city, string stateCode, string postalCode, string country);

        [WebInvoke(UriTemplate = "/GetReviews?userId={userId}&restaurantId={restaurantId}", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        IEnumerable<Review> GetReviews(Guid userId, Guid restaurantId);

        [WebInvoke(UriTemplate = "/AddRestaurant?name={name}&street={street}&city={city}&stateCode={stateCode}&postalCode={postalCode}&country={country}", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool AddRestaurant(string name, string street, string city, string stateCode, string postalCode, string country);
        
        [WebInvoke(UriTemplate = "/AddReview?userId={userId}&restaurantId={restaurantId}&overallRating={overallRating}&foodRating={foodRating}&serviceRating={serviceRating}&costRating={costRating}&comments={comments}", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool AddReview(Guid userId, Guid restaurantId, int overallRating, int foodRating, int serviceRating, int costRating, string comments);

        [WebInvoke(UriTemplate = "/DeleteReview?id={id}", Method = "DELETE", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool DeleteReview(Guid id);
    }
}
