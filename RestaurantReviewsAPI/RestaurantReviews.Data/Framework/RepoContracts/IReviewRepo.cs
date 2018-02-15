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
        void Add(long restaurantId, long authorId, Review review);
        Task<List<Review>> FindMatchingResults(long restaurantId = -1, long authorId = -1);
    }
}
