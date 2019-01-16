using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Common.Exceptions
{
    [Serializable]
    public class InvalidFilterFieldException : Exception
    {
		public InvalidFilterFieldException(string message) : base(message) { }

    }
}
