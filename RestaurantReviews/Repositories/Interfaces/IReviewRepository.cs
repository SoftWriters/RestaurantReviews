using Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<IReviewModel> AddReview(IReviewModel review);

        IEnumerable<IReviewModel> DeleteReview(IReviewModel review);

        IEnumerable<IReviewModel> GetReviews();

        IReviewModel GetReviewById(int id);

        IEnumerable<IReviewModel> GetReviewsByUser(IUserModel user);

        bool HasData();
    }
}
