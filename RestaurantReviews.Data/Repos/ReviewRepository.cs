using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data.Interfaces;
using RestaurantReviews.Model;

namespace RestaurantReviews.Data.Repos
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary> ReviewRepository Constructor. The ReviewRepository does all of the work when saving 
        ///           reviews to our data store.</summary>
        ///
        /// <param name="dbContext"> Context for the database. </param>
        ///-------------------------------------------------------------------------------------------------

        public ReviewRepository(RestaurantReviewContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly RestaurantReviewContext _dbContext;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the reviews by users in this collection. </summary>
        ///
        /// <param name="username"> The username to filter by. </param>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the reviews by users in this
        ///     collection.  Even though we ultimately use a list, I left this as an IEnumberable for
        ///     maximum flexibility.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public IEnumerable<Review> GetReviewsByUser(string username)
        {
            return _dbContext.Reviews.Where(x => x.User == username); 
        }
    }
}