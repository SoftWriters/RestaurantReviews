using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Framework.RepoContracts;

namespace RestaurantReviews.Data.Framework.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepo UserRepo { get; }
        IStateRepo StateRepo { get; }
        IRestaurantRepo RestaurantRepo { get; }
        Task<int> CommitAsync();
    }
}
