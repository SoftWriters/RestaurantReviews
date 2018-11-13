using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        IEnumerable<IReviewModel> _reviews = new List<IReviewModel>();

        int _maxId = 0;

        public IEnumerable<IReviewModel> AddReview(IReviewModel review)
        {
            List<IReviewModel> reviews = _reviews.ToList();
            review.Id = ++_maxId;
            reviews.Add(review);
            _reviews = reviews;
            return _reviews;
        }

        public IEnumerable<IReviewModel> DeleteReview(IReviewModel review)
        {
            List<IReviewModel> reviews = _reviews.Where(r => r != review).ToList();
            _reviews = reviews;
            return _reviews;
        }

        public IEnumerable<IReviewModel> GetReviews()
        {
            return _reviews;
        }

        public IReviewModel GetReviewById(int id)
        {
            return _reviews.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<IReviewModel> GetReviewsByUser(IUserModel user)
        {
            return _reviews.Where(r => r.SubmittingUser == user);
        }

        public bool HasData()
        {
            return _reviews.Any();
        }
    }
}