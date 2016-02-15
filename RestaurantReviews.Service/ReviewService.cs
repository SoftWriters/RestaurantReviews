using RestaurantReviews.Data;
using RestaurantReviews.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Service
{
    public class ReviewService : IReviewService
    {
        private readonly Data.Services.ReviewService _ReviewService;

        public ReviewService()
        {
            _ReviewService = new Data.Services.ReviewService();
        }

        public void Delete(Guid id)
        {
            _ReviewService.Delete(id);
        }

        public List<Review> GetAll()
        {
            return _ReviewService.GetAll();
        }

        public Review GetByID(Guid id)
        {
            return _ReviewService.GetByID(id);
        }

        public List<Review> GetByUserID(Guid userID)
        {
            return _ReviewService.GetByUserID(userID);
        }

        public void Save(Review t)
        {
            _ReviewService.Save(t);
        }
    }
}