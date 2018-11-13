using Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IChainRepository
    {
        IEnumerable<IChainModel> AddChain(IChainModel chain);

        IChainModel GetChainById(int id);

        bool HasData();
    }
}
