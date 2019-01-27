using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RestaurantReviews.Api.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;
        
        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection GetOpenConnection()
        {
            return _connection ?? (_connection = new SqlConnection(_configuration.GetConnectionString("RestaurantDb")));
        }

        public IDbConnection Connection => GetOpenConnection();

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}