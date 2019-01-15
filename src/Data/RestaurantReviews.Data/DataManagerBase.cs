using RestaurantReviews.Common;
using System.Data;

namespace RestaurantReviews.Data
{
    public interface IDataManager
    {

    }
    public class DataManagerBase : IDataManager
    {
        private IDbContext _context;

        public DataManagerBase(IDbContext context)
        {
            this._context = context;
        }

        protected IDbConnection GetConnection()
        {
            return new System.Data.SqlClient.SqlConnection(_context.ConnnectionString);
        }
    }
}
