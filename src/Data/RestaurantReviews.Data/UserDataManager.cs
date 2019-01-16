using Dapper;
using RestaurantReviews.Common;
using RestaurantReviews.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace RestaurantReviews.Data
{
    public interface IUserDataManager : IDataManager
    {
        Task<User> CreateUserAsync(string username);
        Task<bool> UserExistAsync(int userId);
        Task<IEnumerable<User>> GetUsersAsync(int page, int pagesize);
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

        public async Task<IEnumerable<User>> GetUsersAsync(int page, int pagesize)
        {
            using (var db = GetConnection())
            {
                var query = @"SELECT  id, username
                                FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY username ) AS RowNum, id, username
                                          FROM      users
                                        ) AS RowConstrainedResult
                                WHERE   RowNum >= @start
                                    AND RowNum < @end
                                ORDER BY RowNum";
                var start = ((page - 1) * pagesize) + 1;
                var end = start + pagesize;
                
                var result = await db.QueryAsync<User>(query, new { start, end });
                
                return result;
            }
        }

        public async Task<bool> UserExistAsync(int userId)
        {
            using (var db = GetConnection())
            {
                string selectQuery = @"if exists (select 1 from dbo.users where id=@UserId) select 1 AS userexists else select 0 as userexists";

                var result = await db.QueryAsync<int>(selectQuery, new
                {
                    UserId = userId
                }); //strange glitch causes hang if await is used.
                return result.Single() == 1;
            }
        }
    }
}
