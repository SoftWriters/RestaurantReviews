using RestaurantReviews.Data;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Service.Interfaces
{
    public interface IReviewService
    {
        void Delete(Guid id);

        List<Review> GetAll();

        Review GetByID(Guid id);

        List<Review> GetByUserID(Guid userID);

        void Save(Review t);
    }
}