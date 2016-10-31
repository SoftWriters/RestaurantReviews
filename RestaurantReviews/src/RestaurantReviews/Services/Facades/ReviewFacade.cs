using System;
using System.Collections.Generic;
using RestaurantReviews.Models;
using RestaurantReviews.Interfaces;
using RestaurantReviews.DataAccess;

namespace RestaurantReviews.Services
{
    public class ReviewFacade
    {
        internal IEnumerable<Review> GetByUser(UserRequest user)
        {
            using (var context = new RestaurantReviewContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                return unitOfWork.Reviews.GetReviewsByUser(user);
            }
        }

        internal IEnumerable<Review> GetByRestaurant(RestaurantRequest restaurant)
        {
            using (var context = new RestaurantReviewContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                return unitOfWork.Reviews.GetReviewsByRestaurant(restaurant);
            }
        }

        internal void AddReviewForRestaurant(ReviewRequest review)
        {
            using (var context = new RestaurantReviewContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                unitOfWork.Reviews.Add(review);
                unitOfWork.Save();
            }
        }

        internal void RemoveRestaurantReview(long reviewId)
        {
            using (var context = new RestaurantReviewContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                unitOfWork.Reviews.Delete(reviewId);
                unitOfWork.Save();
            }
        }
    }
}