using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Common
{
    public interface IDbContext {
        string ConnnectionString { get; set; }
    }
    public class DbContext : IDbContext
    {
        public string ConnnectionString { get; set; }
    }
}
