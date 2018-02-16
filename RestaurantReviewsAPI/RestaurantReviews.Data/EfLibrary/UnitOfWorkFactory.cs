using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.EfLibrary.Context;
using RestaurantReviews.Data.Framework.UnitOfWorkContracts;

namespace RestaurantReviews.Data.EfLibrary
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string _connectionStringName;

        public UnitOfWorkFactory(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public IUnitOfWork Get()
        {
            return new UnitOfWork(new RestaurantReviewsContext(_connectionStringName));
        }
    }
}
