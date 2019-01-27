using System.Threading.Tasks;

namespace RestaurantReviews.Api.DataAccess
{
    public interface IDeleteReview
    {
        Task<int> Delete(long id);
    }
}