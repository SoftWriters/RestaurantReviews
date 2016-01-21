using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities.Data
{
    /// <summary>
    /// The exception that is thrown when an entity cannot be persisted to storage.
    /// </summary>
    public class PersistanceException : Exception
    {
        public PersistanceException(string message) : base(message) { }
    }
}
