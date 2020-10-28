using RestaurantReviews.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.DataAccess
{
    public class SqlServerRestaurantReviewRepository : IRestaurantReviewRepository
    {
        public void AddRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public Task AddRestaurantAsync(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public void AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task AddReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Restaurant> GetRestaurantsByCity(string city)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Restaurant>> GetRestaurantsByCityAsync(string city)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetReviewsByUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetReviewsByUsersAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
