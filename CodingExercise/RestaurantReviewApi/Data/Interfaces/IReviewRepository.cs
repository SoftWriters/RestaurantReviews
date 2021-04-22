using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IReviewRepository
    {
        void CreateReview(Review review);
        void DeleteReview(int id);
        IList<Review> ReadReviewsByUser(int userId);
        IList<Review> ReadAllReviews();
    }
}
