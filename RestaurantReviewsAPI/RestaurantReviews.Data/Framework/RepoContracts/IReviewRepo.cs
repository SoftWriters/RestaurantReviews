using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data.Framework.RepoContracts
{
    public interface IReviewRepo
    {
        Review Get(long reviewId);
        void Add(long restaurantId, long authorId, Review review);
        void Remove(long reviewId);
        Task<List<Review>> FindMatchingResults(long restaurantId = -1, long authorId = -1);
    }
}
