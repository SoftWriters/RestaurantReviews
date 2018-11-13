using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    // There's no DB backing this up, so I'm skipping any Unit of Work calls such as '_context.Commit();'
    public class ChainRepository : IChainRepository
    {
        IEnumerable<IChainModel> _chains = new List<IChainModel>();

        int _maxId;

        public IEnumerable<IChainModel> AddChain(IChainModel chain)
        {
            List<IChainModel> chains = _chains.ToList();
            chain.Id = ++_maxId;
            chains.Add(chain);
            _chains = chains;
            return _chains;
        }

        public IChainModel GetChainById(int id)
        {
            return _chains.FirstOrDefault(r => r.Id == id);
        }

        public bool HasData()
        {
            return _chains.Any();
        }
        // TODO: Read, Update, Delete
    }
}