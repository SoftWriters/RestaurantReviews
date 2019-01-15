using Dapper;
using RestaurantReviews.Common;
using RestaurantReviews.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace RestaurantReviews.Data
{
    public interface IUserDataManager : IDataManager
    {
        Task<User> CreateUserAsync(string username);
    }
    public class UserDataManager : DataManagerBase, IUserDataManager
    {
        
        public UserDataManager(IDbContext context) : base(context){}
       
        public async Task<User> CreateUserAsync(string username)
        {

            using (var db = GetConnection())
            {
                string insertQuery = @"INSERT INTO [dbo].[Users]([Username]) VALUES (@Username); select @@IDENTITY as Id, @Username as Username";

                var result = await db.QueryAsync<User>(insertQuery, new
                {
                    Username = username
                });
                return result.FirstOrDefault();   
            }

        }
    }
}
