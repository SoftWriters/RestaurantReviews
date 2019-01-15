using Dapper;
using RestaurantReviews.Common;
using RestaurantReviews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Data
{
    public interface IRestaurantDataManager
    {
        Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantAsync(int id);
        Task<Restaurant> GetRestaurantAsync(int id);
        Task<IEnumerable<Restaurant>> GetRestaurantsAsync(int page, int pagesize, DbFilter<Restaurant> filter);
    }

    public class RestaurantDataManager : DataManagerBase, IRestaurantDataManager
    {
        public RestaurantDataManager(IDbContext context) : base(context) { }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync(int page, int pagesize, DbFilter<Restaurant> filter)
        {
            using (var db = GetConnection())
            {
                var query = @"SELECT  id, name, address, city
                                FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY Name ) AS RowNum, id, name, address, city
                                          FROM      Restaurant
                                          <<whereclause>>
                                        ) AS RowConstrainedResult
                                WHERE   RowNum >= @start
                                    AND RowNum < @end
                                ORDER BY RowNum";
                Task<IEnumerable<Restaurant>> result;
                var start = ((page - 1) * pagesize) + 1;
                var end = start + pagesize;
                if(filter != null)
                {
                    query = query.Replace("<<whereclause>>", string.Format("where {0}", filter.GetFilterSql("filterparam")));
                    result = db.QueryAsync<Restaurant>(query, new { start, end, filterparam = filter.Value });
                } else
                {
                    query = query.Replace("<<whereclause>>", "");
                    result = db.QueryAsync<Restaurant>(query, new { start, end });
                }
                
                return await result;
            }
        }

        public async Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant)
        {
            using (var db = GetConnection())
            {
                string insertQuery = @"INSERT INTO [dbo].[Restaurant]([Name], [Address], [City]) VALUES (@Name, @Address, @City); select @@IDENTITY as Id, @Name as Name, @Address as Address, @City as City";

                var result = db.QueryFirstOrDefaultAsync<Restaurant>(insertQuery, new
                {
                    restaurant.Name,
                    restaurant.Address,
                    restaurant.City
                });
                return await result;
            }
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            using (var db = GetConnection())
            {
                string query = @"DELETE from [dbo].[Restaurant] where id = @id";

                await db.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Restaurant> GetRestaurantAsync(int id)
        {
            using (var db = GetConnection())
            {
                var query = @"Select * from Restaurant where Id = @id";

                return await db.QueryFirstOrDefaultAsync<Restaurant>(query, new { id });
            }
        }
    }
}
