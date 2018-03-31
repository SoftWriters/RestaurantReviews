using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities.Data
{
    /// <summary>
    /// The exception that is thrown when an entity cannot be retrieved from storage.
    /// </summary>
    public class RetrievalException : Exception
    {
        public RetrievalException(string message) : base(message) { }
    }
}
