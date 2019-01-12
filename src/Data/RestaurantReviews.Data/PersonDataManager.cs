using Dapper;
using RestaurantReviews.Common;
using RestaurantReviews.Entity;
using System.Data;
using System.Linq;

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
    public interface IPersonDataManager
    {
        Person CreatePerson(string username);
    }
    public class PersonDataManager : DataManagerBase, IPersonDataManager
    {
        
        public PersonDataManager(IDbContext context) : base(context){}
       
        public Person CreatePerson(string username)
        {

            using (var db = GetConnection())
            {
                string insertQuery = @"INSERT INTO [dbo].[Person]([Username]) VALUES (@Username); select @@IDENTITY as Id, @Username as Username";

                var result = db.Query<Person>(insertQuery, new
                {
                    Username = username
                });
                return result.FirstOrDefault();   
            }

        }
    }
}
