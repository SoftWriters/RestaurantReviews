using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
    // There's no DB backing this up, so I'm skipping any Unit of Work calls such as '_context.Commit();'
    public class ChainRepository : IChainRepository
    {
        IEnumerable<IChainModel> _chains = new List<IChainModel>();

        public bool HasData()
        {
            return _chains.Count() > 0;
        }

        public IEnumerable<IChainModel> AddChain(IChainModel chain)
        {
            List<IChainModel> chains = _chains.ToList();
            chains.Add(chain);
            _chains = chains;
            return _chains;
        }

        // TODO: Read, Update, Delete
    }
}