using RestaurantReviews.Core;
using RestaurantReviews.Database.Sqlite;
using SQLite.Net.Platform.Win32;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Controller
{
    public class RestaurantReviewsController : IRestaurantReviewController
    {
        /* Using separate interfaces for reading and writing for a bit of self-sanity checking,
         * and in theory each interface could have enforced permissions (e.g. query interface attempting a write operation would throw) */        private readonly IRestaurantReviewMutableDatabase _mutableDb;
        private readonly IRestaurantReviewQueryDatabase _queryDb;

        public RestaurantReviewsController(string dbFilePath)
        {
            var db = new SqliteRestaurantReviewDatabase(new SQLitePlatformWin32(), dbFilePath);
            _mutableDb = db;
            _queryDb = db;
        }

        public void AddRestaurant(IRestaurant restaurant)
        {
            _mutableDb.AddRestaurant(restaurant);
        }

        public void DeleteRestaurant(Guid restaurantId)
        {
            _mutableDb.DeleteRestaurant(restaurantId);
        }

        public void AddReview(IRestaurantReview review)
        {
            _mutableDb.AddReview(review);
        }

        public void DeleteReview(Guid reviewId)
        {
            _mutableDb.DeleteReview(reviewId);
        }

        public IReadOnlyList<IRestaurant> FindRestaurants(RestaurantsQuery query)
        {
            return _queryDb.FindRestaurants(query.Name, query.City, query.StateOrProvince, query.PostalCode);
        }

        public IReadOnlyList<IRestaurantReview> GetReviewsForRestaurant(Guid restaurantId)
        {
            return _queryDb.GetReviewsForRestaurant(restaurantId);
        }

        public IReadOnlyList<IRestaurantReview> GetReviewsForUser(Guid reviewerId)
        {
            return _queryDb.GetReviewsForReviewer(reviewerId);
        }
    }
}
