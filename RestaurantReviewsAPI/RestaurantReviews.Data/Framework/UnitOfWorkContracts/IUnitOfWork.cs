using System.Threading.Tasks;
using RestaurantReviews.Data.Framework.RepoContracts;

namespace RestaurantReviews.Data.Framework.UnitOfWorkContracts
{
    public interface IUnitOfWork
    {
        IUserRepo UserRepo { get; }
        IStateRepo StateRepo { get; }
        IRestaurantRepo RestaurantRepo { get; }
        IReviewRepo ReviewRepo { get; }
        Task<int> CommitAsync();
    }
}
