using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Model;

namespace RestaurantReviews.Data.Interfaces
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interfaces for restaurant repositories. The problem domain requires an endpoint that
    ///     returns restaurants by city so we'll add this to the common functionality specified by the
    ///     IRepository interface.
    /// </summary>
    ///
    /// <remarks>
    ///     If I had more robust interfaces fro the two services, I'd break these out into their own
    ///     files.
    /// </remarks>
    ///-------------------------------------------------------------------------------------------------

    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        IEnumerable<Restaurant> GetRestaurantsByCity(string city);
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interfaces for review repositories. The problem domain requires an endpoint that returns 
    ///     reviews by the user that wrote them so we'll add this to the common functionality specified
    ///     by the IRepository interface.
    /// </summary>
    ///
    /// <remarks>
    ///     If I had more robust interfaces fro the two services, I'd break these out into their own
    ///     files.
    /// </remarks>
    ///-------------------------------------------------------------------------------------------------

    public interface IReviewRepository : IRepository<Review>
    {
        IEnumerable<Review> GetReviewsByUser(string username);
    }
}