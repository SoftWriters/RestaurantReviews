using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Framework.UnitOfWorkContracts
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Get();
    }
}
